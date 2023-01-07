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

using System;
using System.Buffers;
using System.Collections.Generic;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Tokenization
{
    public abstract class TokenFinderTestBase<TState>:TokenizationTestBase
        where TState:Enum
    {
        protected abstract ITokenTransition<TState>? CreateTransition(  IEnumerable<ITokenTransition<TState>> transitions,
                                                                        ITokenPatternMatch nextMatch    );

        protected abstract ITokenPatternMatch? FindNextMatch(   TState currentState,
                                                                ref SequenceReader<byte> sequenceReader );

        protected Mock<ITokenTransition<TState>> CreateTransitionStub(  ITokenPattern tokenPattern,
                                                                        TState expectedFromState,
                                                                        TState expectedToState  )
        {
            Mock<ITokenPatternMatch> match = new();

            match.Setup( x => x.Pattern ).Returns( tokenPattern );

            Mock<ITokenTransition<TState>> result = new Mock<ITokenTransition<TState>>();

            result.Setup( x => x.From ).Returns( expectedFromState );
            result.Setup( x => x.To ).Returns( expectedToState );
            result.Setup( x => x.Match ).Returns( match.Object );

            return result;
        }

        protected void CreateTransition_WithGivenContext_Succeeds(  ITokenPattern tokenPattern,
                                                                    ITokenTransition<TState>? previousTransition,
                                                                    TState expectedFromState,
                                                                    TState expectedToState  )
        {
            Mock<ITokenPatternMatch> nextMatch = new();

            nextMatch.Setup( x => x.Pattern ).Returns( tokenPattern );

            List<ITokenTransition<TState>> transitions = new List<ITokenTransition<TState>>{};

            if( previousTransition is not null )
            {
                transitions.Add( previousTransition );
            }

            ITokenTransition<TState>? actualResult = this.CreateTransition( transitions, nextMatch.Object );

            actualResult.Should().NotBeNull();
            
            actualResult!.From.Should().Be( expectedFromState );
            actualResult!.To.Should().Be( expectedToState );
            
            actualResult.Match.Should().BeSameAs( nextMatch.Object );
        }

        protected void FindNextMatch_WithGivenContext_Succeeds( TState currentState,
                                                                String content,
                                                                ITokenPattern expectedPattern   )
        {
            this.GetSequenceReader( content, out SequenceReader<byte> sequenceReader );

            ITokenPatternMatch? actualMatch = this.FindNextMatch( currentState, ref sequenceReader );

            actualMatch.Should().NotBeNull();
            
            actualMatch!.Pattern.Should().Be( expectedPattern );
        }
    }
}
