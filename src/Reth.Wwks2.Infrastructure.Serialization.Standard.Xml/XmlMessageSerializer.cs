// Implementation of the WWKS2 protocol.
// Copyright (C) 2022  Thomas Reth

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using AutoMapper;

using Reth.Wwks2.Diagnostics;
using Reth.Wwks2.Infrastructure.Serialization.DataContracts;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts;
using Reth.Wwks2.Protocol.Messages;

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Reth.Wwks2.Infrastructure.Serialization.Standard.Xml
{
    public class XmlMessageSerializer:IMessageSerializer
    {
        public XmlMessageSerializer( Encoding encoding )
        {
            this.Settings = new( encoding );

            MapperConfiguration mapperConfiguration = new(  ( IMapperConfigurationExpression configuration ) =>
                                                            {
                                                                configuration.AddProfile( new XmlMappingProfile() );
                                                            }   );

            this.Mapper = mapperConfiguration.CreateMapper();
            this.DataContractResolver = new XmlDataContractResolver();
            this.SerializationManager = new SerializationManager();
        }

        public XmlMessageSerializer(    Encoding encoding,
                                        IMapper mapper,
                                        IDataContractResolver dataContractResolver,
                                        ISerializationManager serializationManager  )
        {
            this.Settings = new( encoding );

            this.Mapper = mapper;
            this.DataContractResolver = dataContractResolver;
            this.SerializationManager = serializationManager;
        }

        private XmlSerializationSettings Settings
        {
            get;
        }

        private IMapper Mapper
        {
            get;
        }
        
        private IDataContractResolver DataContractResolver
        {
            get;
        }

        private ISerializationManager SerializationManager
        {
            get;
        }

        private XmlSerializerNamespaces Namespaces
        {
            get;
        } = new XmlSerializerNamespaces( new[]{ XmlQualifiedName.Empty } );

        public IMessageParser MessageParser
        {
            get;
        } = new XmlMessageParser();

        public string Serialize( IMessageEnvelope messageEnvelope )
        {
            string messageName = messageEnvelope.Message.Name;

            StringBuilder result = new();

            DataContractMapping mapping = this.DataContractResolver.Resolve( messageName );

            using( XmlWriter writer = XmlWriter.Create( result, this.Settings.WriterSettings ) )
            {
                try
                {
                    XmlSerializer serializer = this.SerializationManager.GetSerializer( mapping.MessageEnvelopeDataContract );
                    
                    object? messageEnvelopeDataContract = this.Mapper.Map( messageEnvelope, messageEnvelope.GetType(), mapping.MessageEnvelopeDataContract );
                    
                    serializer.Serialize( writer, messageEnvelopeDataContract, this.Namespaces );
                }catch( Exception ex )
                {
                    throw new MessageSerializationException( $"Serialization of message '{ messageEnvelope } ({ messageEnvelope.Timestamp })' failed.", ex );
                }
            }

            return result.ToString();
        }

        public Task<string> SerializeAsync( IMessageEnvelope messageEnvelope, CancellationToken cancellationToken = default )
        {
            return Task.Run(    () =>
                                {
                                    return this.Serialize( messageEnvelope );
                                },
                                cancellationToken   );
        }

        public IMessageEnvelope Deserialize( string messageEnvelope )
        {
            try
            {
                string messageName = this.MessageParser.GetName( messageEnvelope );

                DataContractMapping mapping = this.DataContractResolver.Resolve( messageName );

                using( StringReader input = new( messageEnvelope ) )
                {
                    using( XmlReader reader = XmlReader.Create( input, this.Settings.ReaderSettings ) )
                    {
                        XmlSerializer serializer = this.SerializationManager.GetSerializer( mapping.MessageEnvelopeDataContract );

                        object? messageEnvelopeDataContract = serializer.Deserialize( reader );

                        object result = this.Mapper.Map( messageEnvelopeDataContract!, mapping.MessageEnvelopeDataContract, mapping.MessageEnvelope );

                        return ( IMessageEnvelope )result;
                    }
                }
            }catch( Exception ex )
            {
                throw new MessageSerializationException( $"Deserialization of message (truncated) '{ messageEnvelope.Truncate() }' failed.", ex );
            }
        }

        public Task<IMessageEnvelope> DeserializeAsync( string messageEnvelope, CancellationToken cancellationToken = default )
        {
            return Task.Run(    () =>
                                {
                                    return this.Deserialize( messageEnvelope );
                                },
                                cancellationToken   );
        }
    }
}
