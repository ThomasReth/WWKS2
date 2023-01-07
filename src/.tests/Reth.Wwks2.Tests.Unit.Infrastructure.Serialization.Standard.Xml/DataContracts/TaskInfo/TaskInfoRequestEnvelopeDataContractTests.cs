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
using Reth.Wwks2.Protocol.Standard.Messages.TaskInfo;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Xml.DataContracts.TaskInfo
{
    public class TaskInfoRequestEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( string Xml, IMessageEnvelope Object ) Request
        {
            get
            {
                bool includeTaskDetails = true;

                ( string Id, TaskInfoType Type ) taskInfo = ( "4711", TaskInfoType.Output );
                
                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <TaskInfoRequest    Id=""{ XmlMessageTests.MessageId }""
                                                        Source=""{ XmlMessageTests.Source }""
                                                        Destination=""{ XmlMessageTests.Destination }""
                                                        IncludeTaskDetails=""{ includeTaskDetails }"">
                                        <Task Id=""{ taskInfo.Id }"" Type=""{ taskInfo.Type }"" />
                                    </TaskInfoRequest>
                                </WWKS>",
                            new MessageEnvelope<TaskInfoRequest>(   new TaskInfoRequest(    XmlMessageTests.Source,
                                                                                            XmlMessageTests.Destination,
                                                                                            new TaskInfoRequestTask( taskInfo.Id, taskInfo.Type ),
                                                                                            includeTaskDetails,
                                                                                            XmlMessageTests.MessageId   ),
                                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( TaskInfoRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( TaskInfoRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }
    }
}
