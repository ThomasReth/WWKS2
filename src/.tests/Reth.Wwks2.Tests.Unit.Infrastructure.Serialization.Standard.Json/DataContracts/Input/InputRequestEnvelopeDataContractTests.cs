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
using Reth.Wwks2.Protocol.Standard.Messages.Input;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Json.DataContracts.Input
{
    public class InputRequestEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( string Json, IMessageEnvelope Object ) Request
        {
            get
            {
                (   ArticleId Id,
                    string FMDId    ) article = ( new ArticleId( "1985" ), "FMD-23" );

                (   int Index,
                    string ScanCode,
                    string DeliveryNumber,
                    string BatchNumber,
                    string ExternalId,
                    string SerialNumber,
                    PackDate ExpiryDate,
                    int SubItemQuantity,
                    string MachineLocation,
                    StockLocationId StockLocationId ) pack = (  42,
                                                                "0101010",
                                                                "4711",
                                                                "0815",
                                                                "EXT-1",
                                                                "SER-3",
                                                                new PackDate( 2021, 5, 5 ),
                                                                30,
                                                                "main",
                                                                new StockLocationId( "default" )    );

                bool isNewDelivery = false;
                bool setPickingIndicator = true;

                return (    $@" {{
                                    ""InputRequest"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""IsNewDelivery"": ""{ isNewDelivery }"",
                                        ""SetPickingIndicator"": ""{ setPickingIndicator }"",
                                        ""Article"":
                                        [
                                            {{
                                                ""Id"": ""{ article.Id }"",
                                                ""FMDId"": ""{ article.FMDId }"",
                                                ""Pack"":
                                                [
                                                    {{
                                                        ""ScanCode"": ""{ pack.ScanCode }"",
                                                        ""DeliveryNumber"": ""{ pack.DeliveryNumber }"",
                                                        ""BatchNumber"": ""{ pack.BatchNumber }"",
                                                        ""ExternalId"": ""{ pack.ExternalId }"",
                                                        ""SerialNumber"": ""{ pack.SerialNumber }"",
                                                        ""MachineLocation"": ""{ pack.MachineLocation }"",
                                                        ""StockLocationId"": ""{ pack.StockLocationId }"",
                                                        ""ExpiryDate"": ""{ pack.ExpiryDate }"",
                                                        ""Index"": ""{ pack.Index }"",
                                                        ""SubItemQuantity"": ""{ pack.SubItemQuantity }""
                                                    }}
                                                ]
                                            }}
                                        ]
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope<InputRequest>(  new InputRequest(   JsonMessageTests.Source,
                                                                                    JsonMessageTests.Destination,
                                                                                    new InputRequestArticle[]
                                                                                    {
                                                                                        new InputRequestArticle(    article.Id,
                                                                                                                    article.FMDId,
                                                                                                                    new InputRequestPack[]
                                                                                                                    {
                                                                                                                        new InputRequestPack(   pack.ScanCode,
                                                                                                                                                pack.DeliveryNumber,
                                                                                                                                                pack.BatchNumber,
                                                                                                                                                pack.ExternalId,
                                                                                                                                                pack.SerialNumber,
                                                                                                                                                pack.MachineLocation,
                                                                                                                                                pack.StockLocationId,
                                                                                                                                                pack.ExpiryDate,
                                                                                                                                                pack.Index,
                                                                                                                                                pack.SubItemQuantity    )
                                                                                                                    }   )
                                                                                    },
                                                                                    isNewDelivery,
                                                                                    setPickingIndicator,
                                                                                    JsonMessageTests.MessageId  ),
                                                                JsonMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( InputRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( InputRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }
    }
}
