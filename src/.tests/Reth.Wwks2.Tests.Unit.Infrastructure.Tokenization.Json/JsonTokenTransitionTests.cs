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

using Moq;

using Reth.Wwks2.Infrastructure.Tokenization;
using Reth.Wwks2.Infrastructure.Tokenization.Json;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Tokenization.Json
{
    public class JsonTokenTransitionTests:TokenTransitionTestBase<JsonTokenState>
    {
        [InlineData( JsonTokenState.OutOfMessage, JsonTokenState.WithinObject, true )]
        [InlineData( JsonTokenState.OutOfMessage, JsonTokenState.WithinString, false )]
        [InlineData( JsonTokenState.OutOfMessage, JsonTokenState.OutOfMessage, false )]

        [InlineData( JsonTokenState.WithinObject, JsonTokenState.OutOfMessage, false )]
        [InlineData( JsonTokenState.WithinObject, JsonTokenState.WithinString, false )]
        [InlineData( JsonTokenState.WithinObject, JsonTokenState.WithinObject, false )]

        [InlineData( JsonTokenState.WithinString, JsonTokenState.OutOfMessage, false )]
        [InlineData( JsonTokenState.WithinString, JsonTokenState.WithinObject, false )]
        [InlineData( JsonTokenState.WithinString, JsonTokenState.WithinString, false )]
        [Theory]
        public void IsMessageBegin_WithProvidedStates_ReturnsExpectedResult( JsonTokenState from, JsonTokenState to, bool expectedResult )
        {
            Mock<ITokenPatternMatch> matchStub = new();

            JsonTokenTransition transition = new( from, to, matchStub.Object );
            
            bool actualResult = transition.IsMessageBegin();

            actualResult.Should().Be( expectedResult );
        }

        [InlineData( JsonTokenState.OutOfMessage, JsonTokenState.WithinObject, false )]
        [InlineData( JsonTokenState.OutOfMessage, JsonTokenState.WithinString, false )]
        [InlineData( JsonTokenState.OutOfMessage, JsonTokenState.OutOfMessage, false )]

        [InlineData( JsonTokenState.WithinObject, JsonTokenState.OutOfMessage, true )]
        [InlineData( JsonTokenState.WithinObject, JsonTokenState.WithinString, false )]
        [InlineData( JsonTokenState.WithinObject, JsonTokenState.WithinObject, false )]

        [InlineData( JsonTokenState.WithinString, JsonTokenState.OutOfMessage, false )]
        [InlineData( JsonTokenState.WithinString, JsonTokenState.WithinObject, false )]
        [InlineData( JsonTokenState.WithinString, JsonTokenState.WithinString, false )]
        [Theory]
        public void IsMessageEnd_WithProvidedStates_ReturnsExpectedResult( JsonTokenState from, JsonTokenState to, bool expectedResult )
        {
            Mock<ITokenPatternMatch> matchStub = new();

            JsonTokenTransition transition = new( from, to, matchStub.Object );
            
            bool actualResult = transition.IsMessageEnd();

            actualResult.Should().Be( expectedResult );
        }
    }
}
