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
using Reth.Wwks2.Protocol.Standard.Messages.StockDeliverySet;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Xml.DataContracts.StockDeliverySet
{
    public class StockDeliverySetRequestEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( string Xml, IMessageEnvelope Object ) Request
        {
            get
            {
                string deliveryNumber = "K90";

                (   ArticleId Id,
                    string BatchNumber,
                    string ExternalId,
                    string SerialNumber,
                    string MachineLocation,
                    StockLocationId StockLocationId,
                    PackDate ExpiryDate,
                    int Quantity   ) line = (   new ArticleId( "56" ),
                                                "BAT-4",
                                                "EXT-2",
                                                "SER-A",
                                                "default",
                                                new StockLocationId( "main" ),
                                                new PackDate( 2024, 5, 10 ),
                                                30  );

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <StockDeliverySetRequest Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"">
                                        <StockDelivery  DeliveryNumber=""{ deliveryNumber }"">
                                            <Line   Id=""{ line.Id }""
                                                    BatchNumber=""{ line.BatchNumber }""
                                                    ExternalId=""{ line.ExternalId }""
                                                    SerialNumber=""{ line.SerialNumber }""
                                                    MachineLocation=""{ line.MachineLocation }""
                                                    StockLocationId=""{ line.StockLocationId }""
                                                    ExpiryDate=""{ line.ExpiryDate }""
                                                    Quantity=""{ line.Quantity }"" />
                                        </StockDelivery>
                                    </StockDeliverySetRequest>
                                </WWKS>",
                            new MessageEnvelope<StockDeliverySetRequest>(   new StockDeliverySetRequest(    XmlMessageTests.Source,
                                                                                                            XmlMessageTests.Destination,
                                                                                                            new StockDelivery[]
                                                                                                            {
                                                                                                                new(    deliveryNumber,
                                                                                                                        new StockDeliveryLine[]
                                                                                                                        {
                                                                                                                            new(    line.Id,
                                                                                                                                    line.BatchNumber,
                                                                                                                                    line.ExternalId,
                                                                                                                                    line.SerialNumber,
                                                                                                                                    line.MachineLocation,
                                                                                                                                    line.StockLocationId,
                                                                                                                                    line.ExpiryDate,
                                                                                                                                    line.Quantity   )
                                                                                                                        }   )
                                                                                                            },
                                                                                                            XmlMessageTests.MessageId   ),
                                                                            XmlMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( StockDeliverySetRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( StockDeliverySetRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }
    }
}
