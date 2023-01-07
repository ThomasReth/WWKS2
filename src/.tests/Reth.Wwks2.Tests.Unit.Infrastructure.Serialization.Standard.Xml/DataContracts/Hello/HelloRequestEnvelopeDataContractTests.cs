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

using FluentAssertions;

using Reth.Wwks2.Protocol.Messages;
using Reth.Wwks2.Protocol.Standard.Messages;
using Reth.Wwks2.Protocol.Standard.Messages.Hello;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Xml.DataContracts.Hello
{
    public class HelloRequestEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( string Xml, IMessageEnvelope Object ) Request
        {
            get
            {
                (   SubscriberId Id,
                    SubscriberType Type,
                    string TenantId,
                    string Manufacturer,
                    string ProductInfo,
                    string VersionInfo  ) subscriber = (    SubscriberId.DefaultIMS,
                                                            SubscriberType.IMS,
                                                            "Polly's Pharmacy",
                                                            "4711",
                                                            "Digital Pharmacy",
                                                            "1.3"   );

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <HelloRequest Id=""{ XmlMessageTests.MessageId }"">
                                        <Subscriber Id=""{ subscriber.Id }""
                                                    Type=""{ subscriber.Type }""
                                                    Manufacturer=""{ subscriber.Manufacturer }""
                                                    ProductInfo=""{ subscriber.ProductInfo }""
                                                    VersionInfo=""{ subscriber.VersionInfo }""
                                                    TenantId=""{ subscriber.TenantId }"">
                                            <Capability Name=""{ Capabilities.KeepAlive }""/>
                                            <Capability Name=""{ Capabilities.ConfigurationGet }"" />
                                        </Subscriber>
                                    </HelloRequest>
                                </WWKS>",
                            new MessageEnvelope<HelloRequest>(  new HelloRequest(   new Subscriber(   new Capability[]{ Capabilities.KeepAlive,
                                                                                                                        Capabilities.ConfigurationGet },
                                                                                                    subscriber.Id,
                                                                                                    subscriber.Type,
                                                                                                    subscriber.TenantId,
                                                                                                    subscriber.Manufacturer,
                                                                                                    subscriber.ProductInfo,
                                                                                                    subscriber.VersionInfo  ),
                                                                                    XmlMessageTests.MessageId   ),
                                                                XmlMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( HelloRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( HelloRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }
    }
}
