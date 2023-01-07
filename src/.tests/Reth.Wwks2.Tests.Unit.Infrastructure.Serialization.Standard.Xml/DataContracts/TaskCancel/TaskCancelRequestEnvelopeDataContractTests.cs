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

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Xml.DataContracts.TaskCancel
{
    public class TaskCancelRequestEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( string Xml, IMessageEnvelope Object ) Request
        {
            get
            {
                ( string Id, TaskCancelType Type ) taskCancelError = ( "4711", TaskCancelType.Output );
                ( string Id, TaskCancelType Type ) taskCancelled = ( "4712", TaskCancelType.Output );
                ( string Id, TaskCancelType Type ) taskUnknown = ( "4713", TaskCancelType.Output );

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <TaskCancelRequest Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"">
                                        <Task Id=""{ taskCancelError.Id }"" Type=""{ taskCancelError.Type }"" />
                                        <Task Id=""{ taskCancelled.Id }"" Type=""{ taskCancelled.Type }"" />
                                        <Task Id=""{ taskUnknown.Id }"" Type=""{ taskUnknown.Type }"" />
                                    </TaskCancelRequest>
                                </WWKS>",
                            new MessageEnvelope<TaskCancelRequest>( new TaskCancelRequest(  XmlMessageTests.Source,
                                                                                            XmlMessageTests.Destination,
                                                                                            new TaskCancelRequestTask[]
                                                                                            {
                                                                                                new TaskCancelRequestTask( taskCancelError.Id, taskCancelError.Type ),
                                                                                                new TaskCancelRequestTask( taskCancelled.Id, taskCancelError.Type ),
                                                                                                new TaskCancelRequestTask( taskUnknown.Id, taskCancelError.Type ),
                                                                                            },
                                                                                            XmlMessageTests.MessageId   ),
                                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( TaskCancelRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( TaskCancelRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }
    }
}
