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
using Reth.Wwks2.Protocol.Standard.Messages.KeepAlive;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Xml.DataContracts.KeepAlive
{
    public class KeepAliveResponseEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( string Xml, IMessageEnvelope Object ) Response
        {
            get
            {
                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <KeepAliveResponse Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"" />
                                </WWKS>",
                            new MessageEnvelope<KeepAliveResponse>( new KeepAliveResponse(  XmlMessageTests.Source,
                                                                                            XmlMessageTests.Destination,
                                                                                            XmlMessageTests.MessageId   ),
                                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( KeepAliveResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( KeepAliveResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }
    }
}
