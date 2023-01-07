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
using Reth.Wwks2.Protocol.Standard.Messages.Output;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Xml.DataContracts.Output
{
    public class OutputMessageEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( string Xml, IMessageEnvelope Object ) Message
        {
            get
            {
                (   int OutputDestination,
                    int OutputPoint,
                    OutputPriority Priority,
                    OutputMessageStatus Status ) details = ( 14, 0, OutputPriority.Normal, OutputMessageStatus.Completed );

                ArticleId articleId = new ArticleId( "K2" );

                string boxNumber = "1023";

                (   PackId Id,
                    int OutputDestination,
                    int OutputPoint,
                    string DeliveryNumber,
                    string BatchNumber,
                    string ExternalId,
                    string SerialNumber,
                    string ScanCode,
                    string BoxNumber,
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
                    bool IsInFridge,
                    LabelStatus LabelStatus    ) pack = (   new PackId( "1998" ),
                                                            14,
                                                            0,
                                                            "DEL-4",
                                                            "BAT-3",
                                                            "EXT-2",
                                                            "SER-A",
                                                            "11001100",
                                                            boxNumber,
                                                            "default",
                                                            new StockLocationId( "main" ),
                                                            new PackDate( 2012, 5, 6 ),
                                                            new PackDate( 1999, 7, 23 ),
                                                            50,
                                                            100,
                                                            40,
                                                            15,
                                                            765,
                                                            PackShape.Cylinder,
                                                            false,
                                                            LabelStatus.Labelled    );

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <OutputMessage  Id=""{ XmlMessageTests.MessageId }""
                                                    Source=""{ XmlMessageTests.Source }""
                                                    Destination=""{ XmlMessageTests.Destination }"">
                                        <Details    Priority=""{ details.Priority }""
                                                    OutputDestination=""{ details.OutputDestination }""
                                                    OutputPoint=""{ details.OutputPoint }""
                                                    Status=""{ details.Status }"" />
                                        <Article    Id=""{ articleId }"">
                                            <Pack   Id=""{ pack.Id }""
                                                    OutputDestination=""{ pack.OutputDestination }""
                                                    OutputPoint=""{ pack.OutputPoint }""
                                                    DeliveryNumber=""{ pack.DeliveryNumber }""
                                                    BatchNumber=""{ pack.BatchNumber }""
                                                    ExternalId=""{ pack.ExternalId }""
                                                    SerialNumber=""{ pack.SerialNumber }""
                                                    ScanCode=""{ pack.ScanCode }""
                                                    BoxNumber=""{ pack.BoxNumber }""
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
                                                    IsInFridge=""{ pack.IsInFridge }""
                                                    LabelStatus=""{ pack.LabelStatus }"" />
                                        </Article>
                                        <Box Number=""{ boxNumber }"" />
                                    </OutputMessage>
                                </WWKS>",
                            new MessageEnvelope<OutputMessage>( new OutputMessage(  XmlMessageTests.Source,
                                                                                    XmlMessageTests.Destination,
                                                                                    XmlMessageTests.MessageId,
                                                                                    new OutputMessageDetails(   details.OutputDestination,
                                                                                                                details.Status,
                                                                                                                details.Priority,
                                                                                                                details.OutputPoint ),
                                                                                    new OutputArticle[]
                                                                                    {
                                                                                        new(    articleId,
                                                                                                new OutputPack[]
                                                                                                {
                                                                                                    new(    pack.Id,
                                                                                                            pack.OutputDestination,
                                                                                                            pack.OutputPoint,
                                                                                                            pack.DeliveryNumber,
                                                                                                            pack.BatchNumber,
                                                                                                            pack.ExternalId,
                                                                                                            pack.SerialNumber,
                                                                                                            pack.ScanCode,
                                                                                                            pack.BoxNumber,
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
                                                                                                            pack.IsInFridge,
                                                                                                            pack.LabelStatus    )
                                                                                                }   )
                                                                                    },
                                                                                    new Box[]
                                                                                    {
                                                                                        new( boxNumber )
                                                                                    }   ),
                                                                XmlMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Message_Succeeds()
        {
            bool result = base.SerializeMessage( OutputMessageEnvelopeDataContractTests.Message );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Message_Succeeds()
        {
            bool result = base.DeserializeMessage( OutputMessageEnvelopeDataContractTests.Message );

            result.Should().BeTrue();
        }
    }
}
