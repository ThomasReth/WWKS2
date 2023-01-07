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
using Reth.Wwks2.Protocol.Standard.Messages.InitiateInput;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Json.DataContracts.InitiateInput
{
    public class InitiateInputMessageEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( string Json, IMessageEnvelope Object ) Message
        {
            get
            {
                ( int InputSource, int InputPoint, InitiateInputMessageStatus Status ) details = ( 1, 2, InitiateInputMessageStatus.Completed );

                (   ArticleId Id,
                    string Name,
                    string DosageForm,
                    string PackagingUnit,
                    int MaxSubItemQuantity  ) article = ( new ArticleId( "1985" ), "Whole Device", "Flux Capacitor", "Box", 100 );

                (   int Index,
                    string ScanCode,
                    string DeliveryNumber,
                    string BatchNumber,
                    string ExternalId,
                    string SerialNumber,
                    string MachineLocation,
                    PackDate ExpiryDate,
                    PackDate StockInDate,
                    int SubItemQuantity,
                    int Depth,
                    int Width,
                    int Height,
                    int Weight,
                    StockLocationId StockLocationId,
                    PackId Id,
                    PackShape Shape,
                    PackState State,
                    bool IsInFridge ) pack = (  42,
                                                "1100101",
                                                "4711",
                                                "0815",
                                                "EXT-1",
                                                "SER-3",
                                                "main",
                                                new PackDate( 2021, 5, 5 ),
                                                new PackDate( 1999, 6, 28 ),
                                                30,
                                                100,
                                                50,
                                                24,
                                                153,
                                                new StockLocationId( "default" ),
                                                new PackId( "21449" ),
                                                PackShape.Cuboid,
                                                PackState.Available,
                                                true    );

                ( InitiateInputErrorType Type, string Text ) error = ( InitiateInputErrorType.InputBroken, "Error." );

                return (    $@" {{
                                    ""InitiateInputMessage"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""Details"":
                                        {{
                                            ""Status"": ""{ details.Status }"",
                                            ""InputSource"": ""{ details.InputSource }"",
                                            ""InputPoint"": ""{ details.InputPoint }""
                                        }},
                                        ""Article"":
                                        [
                                            {{
                                                ""Id"": ""{ article.Id }"",
                                                ""Name"": ""{ article.Name }"",
                                                ""DosageForm"": ""{ article.DosageForm }"",
                                                ""PackagingUnit"": ""{ article.PackagingUnit }"",
                                                ""MaxSubItemQuantity"": ""{ article.MaxSubItemQuantity }"",
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
                                                        ""Id"": ""{ pack.Id }"",
                                                        ""StockInDate"": ""{ pack.StockInDate }"",
                                                        ""ExpiryDate"": ""{ pack.ExpiryDate }"",
                                                        ""Index"": ""{ pack.Index }"",
                                                        ""SubItemQuantity"": ""{ pack.SubItemQuantity }"",
                                                        ""Depth"": ""{ pack.Depth }"",
                                                        ""Width"": ""{ pack.Width }"",
                                                        ""Height"": ""{ pack.Height }"",
                                                        ""Weight"": ""{ pack.Weight }"",
                                                        ""Shape"": ""{ pack.Shape }"",
                                                        ""State"": ""{ pack.State }"",
                                                        ""IsInFridge"": ""{ pack.IsInFridge }"",
                                                        ""Error"":
                                                        {{
                                                            ""Type"": ""{ error.Type }"",
                                                            ""Text"": ""{ error.Text }""
                                                        }}
                                                    }}
                                                ]
                                            }}
                                        ]
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope<InitiateInputMessage>(  new InitiateInputMessage(   JsonMessageTests.Source,
                                                                                                    JsonMessageTests.Destination,
                                                                                                    JsonMessageTests.MessageId,
                                                                                                    new(    details.Status,
                                                                                                            details.InputSource,
                                                                                                            details.InputPoint  ),
                                                                                                    new InitiateInputMessageArticle[]
                                                                                                    {
                                                                                                        new(    article.Id,
                                                                                                                article.Name,
                                                                                                                article.DosageForm,
                                                                                                                article.PackagingUnit,
                                                                                                                article.MaxSubItemQuantity,
                                                                                                                new InitiateInputMessagePack[]
                                                                                                                {
                                                                                                                    new(    pack.ScanCode,
                                                                                                                            pack.DeliveryNumber,
                                                                                                                            pack.BatchNumber,
                                                                                                                            pack.ExternalId,
                                                                                                                            pack.SerialNumber,
                                                                                                                            pack.MachineLocation,
                                                                                                                            pack.StockLocationId,
                                                                                                                            pack.Id,
                                                                                                                            pack.ExpiryDate,
                                                                                                                            pack.StockInDate,
                                                                                                                            pack.Index,
                                                                                                                            pack.SubItemQuantity,
                                                                                                                            pack.Depth,
                                                                                                                            pack.Width,
                                                                                                                            pack.Height,
                                                                                                                            pack.Weight,
                                                                                                                            pack.Shape,
                                                                                                                            pack.State,
                                                                                                                            pack.IsInFridge,
                                                                                                                            new( error.Type, error.Text )   )
                                                                                                                }   )
                                                                                                    }   ),
                                                                        JsonMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Message_Succeeds()
        {
            bool result = base.SerializeMessage( InitiateInputMessageEnvelopeDataContractTests.Message );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Message_Succeeds()
        {
            bool result = base.DeserializeMessage( InitiateInputMessageEnvelopeDataContractTests.Message );

            result.Should().BeTrue();
        }
    }
}
