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

using Reth.Wwks2.Infrastructure.Serialization.Standard.Json;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Json
{
    public class JsonMessageParserTests
    {
        private const string ExpectedMessageName = "KeepAliveRequest";

        [InlineData( JsonMessageParserTests.ExpectedMessageName, $@"    {{
                                                                            ""{ JsonMessageParserTests.ExpectedMessageName }"":
                                                                            {{
                                                                                ""Id"":""10"",
                                                                                ""Source"":""100"",
                                                                                ""Destination"":""999""
                                                                            }},
                                                                            ""Version"": ""2.0"",
                                                                            ""TimeStamp"": ""2021-05-02T20:58:03Z""
                                                                        }}" )]

        [InlineData( JsonMessageParserTests.ExpectedMessageName, $@"{{""{ JsonMessageParserTests.ExpectedMessageName }"":{{""Id"":""10"",""Source"":""100"",""Destination"":""999""}},""Version"": ""2.0"",""TimeStamp"": ""2021-05-02T20:58:03Z""}}" )]

        [InlineData( JsonMessageParserTests.ExpectedMessageName, $@"    {{
                                                                            ""{ JsonMessageParserTests.ExpectedMessageName }"":
                                                                            {{
                                                                                    ""Id"":""10"",
                                                                                    ""Source"":""100"",
                                                                                    ""Destination"":""999""
                                                                            }}
                                                                        }}" )]
        
        [Theory]
        public void GetMessageName_FromMessageWithOrWithoutEnvelope_Succeeds( string expectedMessageName, string message )
        {
            JsonMessageParser parser = new();

            string actualMessageName = parser.GetName( message );
             
            actualMessageName.Should().Be( expectedMessageName );
        }
    }
}
