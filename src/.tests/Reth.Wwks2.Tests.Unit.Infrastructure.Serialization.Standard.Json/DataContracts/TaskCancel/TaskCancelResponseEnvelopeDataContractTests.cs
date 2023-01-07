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
using Reth.Wwks2.Protocol.Standard.Messages.TaskCancel;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Json.DataContracts.TaskCancel
{
    public class TaskCancelResponseEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( string Json, IMessageEnvelope Object ) Response
        {
            get
            {
                ( string Id, TaskCancelType Type, TaskCancelStatus Status ) taskCancelError = ( "4711", TaskCancelType.Output, TaskCancelStatus.CancelError );
                ( string Id, TaskCancelType Type, TaskCancelStatus Status ) taskCancelled = ( "4712", TaskCancelType.Output, TaskCancelStatus.Cancelled );
                ( string Id, TaskCancelType Type, TaskCancelStatus Status ) taskUnknown = ( "4713", TaskCancelType.Output, TaskCancelStatus.Unknown );

                return (    $@" {{
                                    ""TaskCancelResponse"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""Task"":
                                        [
                                            {{
                                                ""Id"": ""{ taskCancelError.Id }"",
                                                ""Type"": ""{ taskCancelError.Type }"",
                                                ""Status"": ""{ taskCancelError.Status }""
                                            }},
                                            {{
                                                ""Id"": ""{ taskCancelled.Id }"",
                                                ""Type"": ""{ taskCancelled.Type }"",
                                                ""Status"": ""{ taskCancelled.Status }""
                                            }},
                                            {{
                                                ""Id"": ""{ taskUnknown.Id }"",
                                                ""Type"": ""{ taskUnknown.Type }"",
                                                ""Status"": ""{ taskUnknown.Status }""
                                            }}
                                        ]
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope<TaskCancelResponse>(    new TaskCancelResponse( JsonMessageTests.Source,
                                                                                                JsonMessageTests.Destination,
                                                                                                JsonMessageTests.MessageId,
                                                                                                new TaskCancelResponseTask[]
                                                                                                {
                                                                                                    new TaskCancelResponseTask( taskCancelError.Id, taskCancelError.Type, taskCancelError.Status ),
                                                                                                    new TaskCancelResponseTask( taskCancelled.Id, taskCancelError.Type, taskCancelled.Status ),
                                                                                                    new TaskCancelResponseTask( taskUnknown.Id, taskCancelError.Type, taskUnknown.Status ),
                                                                                                }   ),
                                                                JsonMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( TaskCancelResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( TaskCancelResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }
    }
}
