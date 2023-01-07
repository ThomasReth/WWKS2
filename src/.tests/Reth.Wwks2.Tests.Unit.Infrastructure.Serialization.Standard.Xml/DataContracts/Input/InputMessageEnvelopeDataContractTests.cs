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

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Xml.DataContracts.Input
{
    public class InputMessageEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( string Xml, IMessageEnvelope Object ) Message
        {
            get
            {
                (   ArticleId Id,
                    string Name,
                    string DosageForm,
                    string PackagingUnit,
                    int MaxSubItemQuantity  ) article = ( new ArticleId( "1985" ), "Whole Device", "Flux Capacitor", "Box", 100 );

                (   PackId Id,
                    string DeliveryNumber,
                    string BatchNumber,
                    string ExternalId,
                    string SerialNumber,
                    string ScanCode,
                    string MachineLocation,
                    PackDate ExpiryDate,
                    PackDate StockInDate,
                    int Index,
                    int SubItemQuantity,
                    int Depth,
                    int Width,
                    int Height,
                    int Weight,
                    PackShape Shape,
                    PackState State,
                    bool IsInFridge,
                    StockLocationId StockLocationId ) pack = (  new PackId( "1984" ),
                                                                "4711",
                                                                "0815",
                                                                "EXT-1",
                                                                "SER-3",
                                                                "1001101",
                                                                "main",
                                                                new PackDate( 2021, 5, 5 ),
                                                                new PackDate( 1999, 7, 23 ),
                                                                42,
                                                                30,
                                                                100,
                                                                50,
                                                                24,
                                                                153,
                                                                PackShape.Cuboid,
                                                                PackState.Available,
                                                                true,
                                                                new StockLocationId( "default" )    );

                InputMessagePackHandling handling = new( InputMessagePackHandlingInput.Completed, "Done." );

                ProductCode productCode = new( new ProductCodeId( "5783" ) );

                bool isNewDelivery = false;

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <InputMessage   Id=""{ XmlMessageTests.MessageId }""
                                                    Source=""{ XmlMessageTests.Source }""
                                                    Destination=""{ XmlMessageTests.Destination }""
                                                    IsNewDelivery=""{ isNewDelivery }"">
                                        <Article    Id=""{ article.Id }""
                                                    Name=""{ article.Name }""
                                                    DosageForm=""{ article.DosageForm }""
                                                    PackagingUnit=""{ article.PackagingUnit }""
                                                    MaxSubItemQuantity=""{ article.MaxSubItemQuantity }"">
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
                                                    Index=""{ pack.Index }""
                                                    SubItemQuantity=""{ pack.SubItemQuantity }""
                                                    Depth=""{ pack.Depth }""
                                                    Width=""{ pack.Width }""
                                                    Height=""{ pack.Height }""
                                                    Weight=""{ pack.Weight }""
                                                    Shape=""{ pack.Shape }""
                                                    State=""{ pack.State }""
                                                    IsInFridge=""{ pack.IsInFridge }"">
                                                <Handling Input=""{ handling.Input }"" Text=""{ handling.Text }""   />
                                            </Pack>
                                        </Article>
                                    </InputMessage>
                                </WWKS>",
                            new MessageEnvelope<InputMessage>(  new InputMessage(   XmlMessageTests.Source,
                                                                                    XmlMessageTests.Destination,
                                                                                    XmlMessageTests.MessageId,
                                                                                    new InputMessageArticle[]
                                                                                    {
                                                                                        new InputMessageArticle(    article.Id,
                                                                                                                    article.Name,
                                                                                                                    article.DosageForm,
                                                                                                                    article.PackagingUnit,
                                                                                                                    article.MaxSubItemQuantity,
                                                                                                                    new ProductCode[]
                                                                                                                    {
                                                                                                                        productCode
                                                                                                                    },
                                                                                                                    new InputMessagePack[]
                                                                                                                    {
                                                                                                                        new InputMessagePack(   pack.Id,
                                                                                                                                                handling,
                                                                                                                                                pack.DeliveryNumber,
                                                                                                                                                pack.BatchNumber,
                                                                                                                                                pack.ExternalId,
                                                                                                                                                pack.SerialNumber,
                                                                                                                                                pack.ScanCode,
                                                                                                                                                pack.MachineLocation,
                                                                                                                                                pack.StockLocationId,
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
                                                                                                                                                pack.IsInFridge )
                                                                                                                    }   )
                                                                                    },
                                                                                    isNewDelivery   ),
                                                                XmlMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Message_Succeeds()
        {
            bool result = base.SerializeMessage( InputMessageEnvelopeDataContractTests.Message );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Message_Succeeds()
        {
            bool result = base.DeserializeMessage( InputMessageEnvelopeDataContractTests.Message );

            result.Should().BeTrue();
        }
    }
}
