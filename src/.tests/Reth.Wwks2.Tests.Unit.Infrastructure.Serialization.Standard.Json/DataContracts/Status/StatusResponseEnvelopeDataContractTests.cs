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
    public class StatusResponseEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( string Json, IMessageEnvelope Object ) Response
        {
            get
            {
                ( ComponentType Type, ComponentState State, string Description, string StateText ) storageSystem = ( ComponentType.StorageSystem, ComponentState.Ready, "Vmax1", "Door is open." );
                ( ComponentType Type, ComponentState State, string Description, string StateText ) boxSystem = ( ComponentType.BoxSystem, ComponentState.NotReady, "Box System", "Switched off." );

                ComponentState overallState = ComponentState.NotReady;
                string overallStateText = "Out of order.";

                return (    $@" {{
                                    ""StatusResponse"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""State"": ""{ overallState }"",
                                        ""StateText"": ""{ overallStateText }"",
                                        ""Component"":
                                        [
                                            {{
                                                ""Type"": ""{ storageSystem.Type }"",
                                                ""State"": ""{ storageSystem.State }"",
                                                ""Description"": ""{ storageSystem.Description }"",
                                                ""StateText"": ""{ storageSystem.StateText }""
                                            }},
                                            {{
                                                ""Type"": ""{ boxSystem.Type }"",
                                                ""State"": ""{ boxSystem.State }"",
                                                ""Description"": ""{ boxSystem.Description }"",
                                                ""StateText"": ""{ boxSystem.StateText }""
                                            }}
                                        ]
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope<StatusResponse>(    new StatusResponse( JsonMessageTests.Source,
                                                                                        JsonMessageTests.Destination,
                                                                                        JsonMessageTests.MessageId,
                                                                                        overallState,
                                                                                        overallStateText,
                                                                                        new Component[]
                                                                                        {
                                                                                            new Component(  storageSystem.Type,
                                                                                                            storageSystem.State,
                                                                                                            storageSystem.Description,
                                                                                                            storageSystem.StateText ),
                                                                                            new Component(  boxSystem.Type,
                                                                                                            boxSystem.State,
                                                                                                            boxSystem.Description,
                                                                                                            boxSystem.StateText )
                                                                                        } ),
                                                                JsonMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( StatusResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( StatusResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }
    }
}
