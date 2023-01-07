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

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Xml.DataContracts.StockInfo
{
    public class StockInfoResponseEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( string Xml, IMessageEnvelope Object ) Response
        {
            get
            {
                (   ArticleId Id,
                    string Name,
                    string DosageForm,
                    string PackagingUnit,
                    int MaxSubItemQuantity,
                    int Quantity    ) article = ( new ArticleId( "1985" ), "Whole Device", "Flux Capacitor", "Box", 100, 20 );

                (   PackId Id,
                    string DeliveryNumber,
                    string BatchNumber,
                    string ExternalId,
                    string SerialNumber,
                    string ScanCode,
                    string MachineLocation,
                    StockLocationId StockLocationId,
                    PackDate ExpiryDate,
                    PackDate StockInDate,
                    int SubItemQuantity,
                    int Depth,
                    int Width,
                    int Height,
                    int Weight,
                    PackShape Shape,
                    PackState State,
                    bool IsInFridge  ) pack = ( new PackId( "42" ),
                                                "4711",
                                                "0815",
                                                "EXT-1",
                                                "SER-3",
                                                "0101010",
                                                "main",
                                                new StockLocationId( "default" ),
                                                new PackDate( 2021, 5, 5 ),
                                                new PackDate( 1999, 7, 23 ),
                                                30,
                                                20,
                                                40,
                                                15,
                                                68,
                                                PackShape.Cylinder,
                                                PackState.Available,
                                                true    );

                ProductCode productCode = new( new ProductCodeId( "5783" ) );

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <StockInfoResponse  Id=""{ XmlMessageTests.MessageId }""
                                                        Source=""{ XmlMessageTests.Source }""
                                                        Destination=""{ XmlMessageTests.Destination }"">
                                        <Article    Id=""{ article.Id }""
                                                    Name=""{ article.Name }""
                                                    DosageForm=""{ article.DosageForm }""
                                                    PackagingUnit=""{ article.PackagingUnit }""
                                                    MaxSubItemQuantity=""{ article.MaxSubItemQuantity }""
                                                    Quantity=""{ article.Quantity }"">
                                            <ProductCode Code=""{ productCode.Code }"" />
                                            <Pack   Id=""{ pack.Id }""
                                                    DeliveryNumber=""{ pack.DeliveryNumber }""
                                                    BatchNumber=""{ pack.BatchNumber }""
                                                    ExternalId=""{ pack.ExternalId }""
                                                    SerialNumber=""{ pack.SerialNumber }""
                                                    ScanCode=""{ pack.ScanCode }""
                                                    MachineLocation=""{ pack.MachineLocation }""
                                                    StockLocationId=""{ pack.StockLocationId }""
                                                    ExpiryDate=""{ pack.ExpiryDate }""
                                                    StockInDate=""{ pack.StockInDate }""
                                                    SubItemQuantity=""{ pack.SubItemQuantity }""
                                                    Depth=""{ pack.Depth }""
                                                    Width=""{ pack.Width }""
                                                    Height=""{ pack.Height }""
                                                    Weight=""{ pack.Weight }""
                                                    Shape=""{ pack.Shape }""
                                                    State=""{ pack.State }""
                                                    IsInFridge=""{ pack.IsInFridge }"" />
                                        </Article>
                                    </StockInfoResponse>
                                </WWKS>",
                            new MessageEnvelope<StockInfoResponse>( new StockInfoResponse(  XmlMessageTests.Source,
                                                                                            XmlMessageTests.Destination,
                                                                                            XmlMessageTests.MessageId,
                                                                                            new StockInfoArticle[]
                                                                                            {
                                                                                                new StockInfoArticle(   article.Id,
                                                                                                                        article.Quantity,
                                                                                                                        article.Name,
                                                                                                                        article.DosageForm,
                                                                                                                        article.PackagingUnit,
                                                                                                                        article.MaxSubItemQuantity,
                                                                                                                        new ProductCode[]
                                                                                                                        {
                                                                                                                            productCode
                                                                                                                        },
                                                                                                                        new StockInfoPack[]
                                                                                                                        {
                                                                                                                            new StockInfoPack(  pack.Id,
                                                                                                                                                pack.DeliveryNumber,
                                                                                                                                                pack.BatchNumber,
                                                                                                                                                pack.ExternalId,
                                                                                                                                                pack.SerialNumber,
                                                                                                                                                pack.ScanCode,
                                                                                                                                                pack.MachineLocation,
                                                                                                                                                pack.StockLocationId,
                                                                                                                                                pack.ExpiryDate,
                                                                                                                                                pack.StockInDate,
                                                                                                                                                pack.SubItemQuantity,
                                                                                                                                                pack.Depth,
                                                                                                                                                pack.Width,
                                                                                                                                                pack.Height,
                                                                                                                                                pack.Weight,
                                                                                                                                                pack.Shape,
                                                                                                                                                pack.State,
                                                                                                                                                pack.IsInFridge )
                                                                                                                        }   )
                                                                                            } ),
                                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( StockInfoResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( StockInfoResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }
    }
}
