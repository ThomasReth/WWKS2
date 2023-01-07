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
using Reth.Wwks2.Protocol.Standard.Messages.StockLocationInfo;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Json.DataContracts.StockLocationInfo
{
    public class StockLocationInfoResponseEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( string Json, IMessageEnvelope Object ) Response
        {
            get
            {
                StockLocationId defaultLocationId = new StockLocationId( "4711" );
                StockLocationId specialLocationId = new StockLocationId( "4712" );

                string defaultLocationDescription = "Default";
                string specialLocationDescription = "Special";

                return (    $@" {{
                                    ""StockLocationInfoResponse"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""StockLocation"":
                                        [
                                            {{
                                                ""Id"": ""{ defaultLocationId }"",
                                                ""Description"": ""{ defaultLocationDescription }""
                                            }},
                                            {{
                                                ""Id"": ""{ specialLocationId }"",
                                                ""Description"": ""{ specialLocationDescription }""
                                            }}
                                        ]
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope<StockLocationInfoResponse>( new StockLocationInfoResponse(  JsonMessageTests.Source,
                                                                                                            JsonMessageTests.Destination,
                                                                                                            JsonMessageTests.MessageId,
                                                                                                            new StockLocation[]
                                                                                                            {
                                                                                                                new StockLocation( defaultLocationId, defaultLocationDescription ),
                                                                                                                new StockLocation( specialLocationId, specialLocationDescription )
                                                                                                            } ),
                                                                            JsonMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( StockLocationInfoResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( StockLocationInfoResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }
    }
}
