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
using Reth.Wwks2.Protocol.Standard.Messages.Status;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Json.DataContracts.Status
{
    public class StatusRequestEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( string Json, IMessageEnvelope Object ) Request
        {
            get
            {
                bool includeDetails = true;

                return (    $@" {{
                                    ""StatusRequest"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""IncludeDetails"": ""{ includeDetails }""
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope<StatusRequest>( new StatusRequest(  JsonMessageTests.Source,
                                                                                    JsonMessageTests.Destination,
                                                                                    includeDetails,
                                                                                    JsonMessageTests.MessageId  ),
                                                                JsonMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( StatusRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( StatusRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }
    }
}
