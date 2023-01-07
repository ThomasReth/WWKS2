// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

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

using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts;
using Reth.Wwks2.Protocol.Messages;
using Reth.Wwks2.Protocol.Standard.Messages;
using Reth.Wwks2.Tests.Unit.TestData.Xml;

using System.Text;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Xml.DataContracts
{
    public abstract class XmlMessageTests
    {
        protected static readonly MessageTimestamp Timestamp = MessageTimestamp.Parse( "2021-05-02T20:58:03Z" );
        protected static readonly MessageId MessageId = new( 10 );
        protected static readonly SubscriberId Source = SubscriberId.DefaultIMS;
        protected static readonly SubscriberId Destination = SubscriberId.DefaultRobot;

        private XmlMessageSerializer CreateSerializer()
        {
            MapperConfiguration mapperConfiguration = new(  ( IMapperConfigurationExpression configuration ) =>
                                                            {
                                                                configuration.AddProfile( new XmlMappingProfile() );
                                                            }   );

            return new( Encoding.UTF8,
                        mapperConfiguration.CreateMapper(),
                        new XmlDataContractResolver(),
                        new SerializationManager()  );
        }

        protected bool SerializeMessage( ( string Xml, IMessageEnvelope Object ) message )
        {
            XmlMessageSerializer serializer = this.CreateSerializer();

            string actualXml = serializer.Serialize( message.Object );

            return XmlComparer.Default.Equals( message.Xml, actualXml );
        }

        protected bool DeserializeMessage( ( string Xml, IMessageEnvelope Object ) message )
        {
            XmlMessageSerializer serializer = this.CreateSerializer();

            IMessageEnvelope actualObject = serializer.Deserialize( message.Xml );

            return message.Object.Equals( actualObject );
        }
    }
}
