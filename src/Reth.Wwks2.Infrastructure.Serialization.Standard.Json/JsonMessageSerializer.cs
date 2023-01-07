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
using Reth.Wwks2.Infrastructure.Serialization.Standard.Json.DataContracts;
using Reth.Wwks2.Protocol.Messages;

using System;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Wwks2.Infrastructure.Serialization.Standard.Json
{
    public class JsonMessageSerializer:IMessageSerializer
    {
        public JsonMessageSerializer()
        {
            MapperConfiguration mapperConfiguration = new(  ( IMapperConfigurationExpression configuration ) =>
                                                            {
                                                                configuration.AddProfile( new JsonMappingProfile() );
                                                            }   );

            this.Mapper = mapperConfiguration.CreateMapper();
            this.DataContractResolver = new JsonDataContractResolver();
        }

        public JsonMessageSerializer(   IMapper mapper,
                                        IDataContractResolver dataContractResolver  )
        {
            this.Mapper = mapper;
            this.DataContractResolver = dataContractResolver;
        }

        private JsonSerializationSettings Settings
        {
            get;
        } = new();

        private IMapper Mapper
        {
            get;
        }

        private IDataContractResolver DataContractResolver
        {
            get;
        }

        public IMessageParser MessageParser
        {
            get;
        } = new JsonMessageParser();

        public string Serialize( IMessageEnvelope messageEnvelope )
        {
            string messageName = messageEnvelope.Message.Name;

            DataContractMapping mapping = this.DataContractResolver.Resolve( messageName );

            try
            {
                object? messageEnvelopeDataContract = this.Mapper.Map( messageEnvelope, messageEnvelope.GetType(), mapping.MessageEnvelopeDataContract );
                
                return JsonSerializer.Serialize( messageEnvelopeDataContract, mapping.MessageEnvelopeDataContract, this.Settings.SerializerOptions );
            }catch( Exception ex )
            {
                throw new MessageSerializationException( $"Serialization of message '{ messageEnvelope } ({ messageEnvelope.Timestamp })' failed.", ex );
            }
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

                object? messageEnvelopeDataContract = JsonSerializer.Deserialize( messageEnvelope, mapping.MessageEnvelopeDataContract, this.Settings.DeserializerOptions );

                object result = this.Mapper.Map( messageEnvelopeDataContract!, mapping.MessageEnvelopeDataContract, mapping.MessageEnvelope );

                return ( IMessageEnvelope )result;
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
