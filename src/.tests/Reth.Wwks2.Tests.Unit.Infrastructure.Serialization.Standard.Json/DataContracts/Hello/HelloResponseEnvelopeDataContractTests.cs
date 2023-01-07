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

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Json.DataContracts.Hello
{
    public class HelloResponseEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( string Json, IMessageEnvelope Object ) Response
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

                return (    $@" {{
                                    ""HelloResponse"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Subscriber"":
                                        {{
                                            ""Id"": ""{ subscriber.Id }"",
                                            ""Type"": ""{ subscriber.Type }"",
                                            ""Manufacturer"": ""{ subscriber.Manufacturer }"",
                                            ""ProductInfo"": ""{ subscriber.ProductInfo }"",
                                            ""VersionInfo"": ""{ subscriber.VersionInfo }"",
                                            ""TenantId"": ""{ subscriber.TenantId }"",
                                            ""Capability"":
                                            [
                                                {{
                                                    ""Name"": ""{ Capabilities.KeepAlive }""
                                                }},
                                                {{
                                                    ""Name"": ""{ Capabilities.ConfigurationGet }""
                                                }}
                                            ]
                                        }}
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope<HelloResponse>( new HelloResponse(  new Subscriber(   new Capability[]{ Capabilities.KeepAlive,
                                                                                                                        Capabilities.ConfigurationGet },
                                                                                                    subscriber.Id,
                                                                                                    subscriber.Type,
                                                                                                    subscriber.TenantId,
                                                                                                    subscriber.Manufacturer,
                                                                                                    subscriber.ProductInfo,
                                                                                                    subscriber.VersionInfo  ),
                                                                                    JsonMessageTests.MessageId  ),
                                                                JsonMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( HelloResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( HelloResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }
    }
}
