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
using Reth.Wwks2.Protocol.Standard.Messages.StockDeliverySet;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Xml.DataContracts.StockDeliverySet
{
    public class StockDeliverySetResponseEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( string Xml, IMessageEnvelope Object ) Response
        {
            get
            {
                StockDeliverySetResult result = new( StockDeliverySetResultValue.Accepted, "All articles accepted." );

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <StockDeliverySetResponse Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"">
                                        <SetResult Value=""{ result.Value }"" Text=""{ result.Text }"" />
                                    </StockDeliverySetResponse>
                                </WWKS>",
                            new MessageEnvelope<StockDeliverySetResponse>(  new StockDeliverySetResponse(   XmlMessageTests.Source,
                                                                                                            XmlMessageTests.Destination,
                                                                                                            XmlMessageTests.MessageId,
                                                                                                            result  ),
                                                                            XmlMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( StockDeliverySetResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( StockDeliverySetResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }
    }
}
