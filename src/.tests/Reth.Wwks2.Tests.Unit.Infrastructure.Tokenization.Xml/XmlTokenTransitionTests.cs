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
using Reth.Wwks2.Infrastructure.Tokenization.Xml;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Tokenization.Xml
{
    public class XmlTokenTransitionTests:TokenTransitionTestBase<XmlTokenState>
    {
        [InlineData( XmlTokenState.OutOfMessage, XmlTokenState.WithinMessage, true )]
        [InlineData( XmlTokenState.OutOfMessage, XmlTokenState.WithinData, false )]
        [InlineData( XmlTokenState.OutOfMessage, XmlTokenState.OutOfMessage, false )]

        [InlineData( XmlTokenState.WithinMessage, XmlTokenState.OutOfMessage, false )]
        [InlineData( XmlTokenState.WithinMessage, XmlTokenState.WithinData, false )]
        [InlineData( XmlTokenState.WithinMessage, XmlTokenState.WithinMessage, false )]

        [InlineData( XmlTokenState.WithinData, XmlTokenState.OutOfMessage, false )]
        [InlineData( XmlTokenState.WithinData, XmlTokenState.WithinMessage, false )]
        [InlineData( XmlTokenState.WithinData, XmlTokenState.WithinData, false )]
        [Theory]
        public void IsMessageBegin_WithProvidedStates_ReturnsExpectedResult( XmlTokenState from, XmlTokenState to, bool expectedResult )
        {
            Mock<ITokenPatternMatch> matchStub = new();

            XmlTokenTransition transition = new( from, to, matchStub.Object );
            
            bool actualResult = transition.IsMessageBegin();

            actualResult.Should().Be( expectedResult );
        }

        [InlineData( XmlTokenState.OutOfMessage, XmlTokenState.WithinMessage, false )]
        [InlineData( XmlTokenState.OutOfMessage, XmlTokenState.WithinData, false )]
        [InlineData( XmlTokenState.OutOfMessage, XmlTokenState.OutOfMessage, false )]

        [InlineData( XmlTokenState.WithinMessage, XmlTokenState.OutOfMessage, true )]
        [InlineData( XmlTokenState.WithinMessage, XmlTokenState.WithinData, false )]
        [InlineData( XmlTokenState.WithinMessage, XmlTokenState.WithinMessage, false )]

        [InlineData( XmlTokenState.WithinData, XmlTokenState.OutOfMessage, false )]
        [InlineData( XmlTokenState.WithinData, XmlTokenState.WithinMessage, false )]
        [InlineData( XmlTokenState.WithinData, XmlTokenState.WithinData, false )]
        [Theory]
        public void IsMessageEnd_WithProvidedStates_ReturnsExpectedResult( XmlTokenState from, XmlTokenState to, bool expectedResult )
        {
            Mock<ITokenPatternMatch> matchStub = new();

            XmlTokenTransition transition = new( from, to, matchStub.Object );
            
            bool actualResult = transition.IsMessageEnd();

            actualResult.Should().Be( expectedResult );
        }
    }
}
