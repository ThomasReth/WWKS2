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
using Reth.Wwks2.Protocol.Standard.Messages.StockInfo;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Json.DataContracts.StockInfo
{
    public class StockInfoRequestEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( string Json, IMessageEnvelope Object ) Request
        {
            get
            {
                bool includePacks = true;
                bool includeArticleDetails = true;

                ArticleId articleId = new ArticleId( "1999" );

                string batchNumber = "5483";
                string externalId = "asdf";
                string serialNumber = "X-11";
                string machineLocation = "default";

                StockLocationId stockLocationId = new StockLocationId( "main" );

                return (    $@" {{
                                    ""StockInfoRequest"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""IncludePacks"": ""{ includePacks }"",
                                        ""IncludeArticleDetails"": ""{ includeArticleDetails }"",
                                        ""Criteria"":
                                        [
                                            {{
                                                ""ArticleId"": ""{ articleId }"",
                                                ""BatchNumber"": ""{ batchNumber }"",
                                                ""ExternalId"": ""{ externalId }"",
                                                ""SerialNumber"": ""{ serialNumber }"",
                                                ""MachineLocation"": ""{ machineLocation }"",
                                                ""StockLocationId"": ""{ stockLocationId }""
                                            }}
                                        ]
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope<StockInfoRequest>(    new StockInfoRequest( JsonMessageTests.Source,
                                                                                            JsonMessageTests.Destination,
                                                                                            includePacks,
                                                                                            includeArticleDetails,
                                                                                            new StockInfoCriteria[]
                                                                                            {
                                                                                                new StockInfoCriteria(  articleId,
                                                                                                                        batchNumber,
                                                                                                                        externalId,
                                                                                                                        serialNumber,
                                                                                                                        machineLocation,
                                                                                                                        stockLocationId )
                                                                                            },
                                                                                            JsonMessageTests.MessageId  ),
                                                                        JsonMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( StockInfoRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( StockInfoRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }
    }
}
