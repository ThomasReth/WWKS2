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
    public class OutputRequestEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( string Xml, IMessageEnvelope Object ) Request
        {
            get
            {
                (   int OutputDestination,
                    int OutputPoint,
                    OutputPriority Priority ) details = ( 14, 0, OutputPriority.Normal );

                (   int Quantity,
                    int SubItemQuantity,
                    ArticleId ArticleId,
                    PackId PackId,
                    PackDate MinimumExpiryDate,
                    string BatchNumber,
                    string ExternalId,
                    string SerialNumber,
                    string MachineLocation,
                    StockLocationId StockLocationId,
                    bool SingleBatchNumber ) criteria = (   23,
                                                            45,
                                                            new ArticleId( "4711" ),
                                                            new PackId( "1998" ),
                                                            new PackDate( 1999, 7, 23 ),
                                                            "BATCH-1",
                                                            "EXT-3",
                                                            "SER-5",
                                                            "default",
                                                            new StockLocationId( "main" ),
                                                            true    );

                ( string TemplateId, string Content ) label = ( "TEMPLATE-DEFAULT", "<LABEL>Print me.</LABEL>" );

                string boxNumber = "DE2-115";

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <OutputRequest  Id=""{ XmlMessageTests.MessageId }""
                                                    Source=""{ XmlMessageTests.Source }""
                                                    Destination=""{ XmlMessageTests.Destination }""
                                                    BoxNumber=""{ boxNumber }"">
                                        <Details    Priority=""{ details.Priority }""
                                                    OutputDestination=""{ details.OutputDestination }""
                                                    OutputPoint=""{ details.OutputPoint }"" />
                                        <Criteria   ArticleId=""{ criteria.ArticleId }""
                                                    PackId=""{ criteria.PackId }""
                                                    MinimumExpiryDate=""{ criteria.MinimumExpiryDate }""
                                                    BatchNumber=""{ criteria.BatchNumber }""
                                                    ExternalId=""{ criteria.ExternalId }""
                                                    SerialNumber=""{ criteria.SerialNumber }""
                                                    MachineLocation=""{ criteria.MachineLocation }""
                                                    StockLocationId=""{ criteria.StockLocationId }""
                                                    Quantity=""{ criteria.Quantity }""
                                                    SubItemQuantity=""{ criteria.SubItemQuantity }""
                                                    SingleBatchNumber=""{ criteria.SingleBatchNumber }"">
                                            <Label TemplateId=""{ label.TemplateId }"">
                                                <Content>
                                                    <![CDATA[{ label.Content }]]>
                                                </Content>
                                            </Label>
                                        </Criteria>
                                    </OutputRequest>
                                </WWKS>",
                            new MessageEnvelope<OutputRequest>( new OutputRequest(  XmlMessageTests.Source,
                                                                                    XmlMessageTests.Destination,
                                                                                    XmlMessageTests.MessageId,
                                                                                    new OutputRequestDetails(   details.OutputDestination,
                                                                                                                details.Priority,
                                                                                                                details.OutputPoint ),
                                                                                    new OutputCriteria[]
                                                                                    {
                                                                                        new OutputCriteria( criteria.Quantity,
                                                                                                            criteria.SubItemQuantity,
                                                                                                            criteria.ArticleId,
                                                                                                            criteria.PackId,
                                                                                                            criteria.MinimumExpiryDate,
                                                                                                            criteria.BatchNumber,
                                                                                                            criteria.ExternalId,
                                                                                                            criteria.SerialNumber,
                                                                                                            criteria.MachineLocation,
                                                                                                            criteria.StockLocationId,
                                                                                                            criteria.SingleBatchNumber,
                                                                                                            new OutputLabel[]
                                                                                                            {
                                                                                                                new( label.TemplateId, label.Content )
                                                                                                            }   )
                                                                                    },
                                                                                    boxNumber   ),
                                                                XmlMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( OutputRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( OutputRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }
    }
}
