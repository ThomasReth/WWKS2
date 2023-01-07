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
using Reth.Wwks2.Protocol.Standard.Messages.ConfigurationGet;

using System;
using System.Text;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Json.DataContracts.ConfigurationGet
{
    public class ConfigurationGetResponseEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( string Json, IMessageEnvelope Object ) Response
        {
            get
            {
                string configuration = Convert.ToBase64String(  Encoding.UTF8.GetBytes( $@" [
                                                                                                {{
                                                                                                    ""Name"": ""Autumn"",
                                                                                                    ""Value"": ""brown""
                                                                                                }},
                                                                                                {{
                                                                                                    ""Name"": ""Summer"",
                                                                                                    ""Value"": ""green""
                                                                                                }}
                                                                                            ]" )    );

                return (    $@" {{
                                    ""ConfigurationGetResponse"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""Configuration"": ""{ configuration }""
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope<ConfigurationGetResponse>(  new ConfigurationGetResponse(   JsonMessageTests.Source,
                                                                                                            JsonMessageTests.Destination,
                                                                                                            JsonMessageTests.MessageId,
                                                                                                            configuration  ),
                                                                            JsonMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( ConfigurationGetResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( ConfigurationGetResponseEnvelopeDataContractTests.Response );

            result.Should().BeTrue();
        }
    }
}
