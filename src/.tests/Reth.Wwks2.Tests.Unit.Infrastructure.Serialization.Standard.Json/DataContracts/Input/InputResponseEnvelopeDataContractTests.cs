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
    public class InputResponseEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( string Json, IMessageEnvelope Object ) Response
        {
            get
            {
                (   ArticleId Id,
                    string Name,
                    string DosageForm,
                    string PackagingUnit,
                    bool RequiresFridge,
                    int MaxSubItemQuantity,
                    PackDate SerialNumberSinceExpiryDate ) article = ( new ArticleId( "1985" ), "Whole Device", "Flux Capacitor", "Box", false, 100, new PackDate( 1999, 7, 23 ) );

                (   int Index,
                    string DeliveryNumber,
                    string BatchNumber,
                    string ExternalId,
                    string SerialNumber,
                    PackDate ExpiryDate,
                    int SubItemQuantity,
                    int Depth,
                    int Width,
                    int Height,
                    int Weight,
                    StockLocationId StockLocationId ) pack = (  42,
                                                                "4711",
                                                                "0815",
                                                                "EXT-1",
                                                                "SER-3",
                                                                new PackDate( 2021, 5, 5 ),
                                                                30,
                                                                100,
                                                                50,
                                                                24,
                                                                153,
                                                                new StockLocationId( "default" )    );

                InputResponsePackHandling handling = new( InputResponsePackHandlingInput.Allowed, "OK." );

                ProductCode productCode = new( new ProductCodeId( "5783" ) );

                bool isNewDelivery = false;

                return (    $@" {{
                                    ""InputResponse"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""IsNewDelivery"": ""{ isNewDelivery }"",
                                        ""Article"":
                                        [
                                            {{
                                                ""Id"": ""{ article.Id }"",
                                                ""Name"": ""{ article.Name }"",
                                                ""DosageForm"": ""{ article.DosageForm }"",
                                                ""PackagingUnit"": ""{ article.PackagingUnit }"",
                                                ""RequiresFridge"": ""{ article.RequiresFridge }"",
                                                ""MaxSubItemQuantity"": ""{ article.MaxSubItemQuantity }"",
                                                ""SerialNumberSinceExpiryDate"": ""{ article.SerialNumberSinceExpiryDate }"",
                                                ""ProductCode"":
                                                [
                                                    {{
                                                        ""Code"": ""{ productCode.Code }""
                                                    }}
                                                ],
                                                ""Pack"":
                                                [
                                                    {{
                                                        ""DeliveryNumber"": ""{ pack.DeliveryNumber }"",
                                                        ""BatchNumber"": ""{ pack.BatchNumber }"",
                                                        ""ExternalId"": ""{ pack.ExternalId }"",
                                                        ""SerialNumber"": ""{ pack.SerialNumber }"",
                                                        ""StockLocationId"": ""{ pack.StockLocationId }"",
                                                        ""ExpiryDate"": ""{ pack.ExpiryDate }"",
                                                        ""Index"": ""{ pack.Index }"",
                                                        ""SubItemQuantity"": ""{ pack.SubItemQuantity }"",
                                                        ""Depth"": ""{ pack.Depth }"",
                                                        ""Width"": ""{ pack.Width }"",
                                                        ""Height"": ""{ pack.Height }"",
                                                        ""Weight"": ""{ pack.Weight }"",
                                                        ""Handling"":
                                                        {{
                                                            ""Input"": ""{ handling.Input }"",
                                                            ""Text"": ""{ handling.Text }""
                                                        }}
                                                    }}
                                                ]
                                            }}
                                        ]
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope<InputResponse>( new InputResponse(  JsonMessageTests.Source,
                                                                                    JsonMessageTests.Destination,
                                                                                    JsonMessageTests.MessageId,
                                                                                    new InputResponseArticle[]
                                                                                    {
                                                                                        new InputResponseArticle(   article.Id,
                                                                                                                    article.Name,
                                                                                                                    article.DosageForm,
                                                                                                                    article.PackagingUnit,
                                                                                                                    article.RequiresFridge,
                                                                                                                    article.MaxSubItemQuantity,
                                                                                                                    article.SerialNumberSinceExpiryDate,
                                                                                                                    new ProductCode[]
                                                                                                                    {
                                                                                                                        productCode
                                                                                                                    },
                                                                                                                    new InputResponsePack[]
                                                                                                                    {
                                                                                                                        new InputResponsePack(  handling,
                                                                                                                                                pack.DeliveryNumber,
                                                                                                                                                pack.BatchNumber,
                                                                                                                                                pack.ExternalId,
                                                                                                                                                pack.SerialNumber,
                                                                                                                                                pack.StockLocationId,
                                                                                                                                                pack.ExpiryDate,
                                                                                                                                                pack.Index,
                                                                                                                                                pack.SubItemQuantity,
                                                                                                                                                pack.Depth,
                                                                                                                                                pack.Width,
                                                                                                                                                pack.Height,
                                                                                                                                                pack.Weight )
                                                                                                                    }   )
                                                                                    },
                                                                                    isNewDelivery   ),
                                                                JsonMessageTests.Timestamp    ) );
                        }
        }

        [Fact]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( InputResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( InputResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }
    }
}
