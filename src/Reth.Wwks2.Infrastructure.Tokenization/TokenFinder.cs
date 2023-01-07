// Implementation of the WWKS2 protocol.
// Copyright (C) 2022  Thomas Reth

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

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reth.Wwks2.Infrastructure.Tokenization
{
    public abstract class TokenFinder<TState>:ITokenFinder<TState>
        where TState:Enum
    {
        protected TokenFinder( TState defaultState )
        {
            this.DefaultState = defaultState;
        }

        public TState DefaultState
        {
            get;
        }

        public virtual ITokenTransition<TState>? FindNextTransition(    IEnumerable<ITokenTransition<TState>> transitions,
                                                                        ref SequenceReader<byte> sequenceReader )
        {
            ITokenTransition<TState>? result = null;

            TState currentState = this.GetCurrentState( transitions.LastOrDefault() );
            
            ITokenPatternMatch? nextMatch = this.FindNextMatch( currentState, ref sequenceReader );

            if( nextMatch is not null )
            {
                result = this.CreateTransition( transitions, nextMatch );
            }

            return result;
        }

        public abstract ITokenTransition<TState>? CreateTransition( IEnumerable<ITokenTransition<TState>> transitions,
                                                                    ITokenPatternMatch nextMatch    );

        public abstract ITokenPatternMatch? FindNextMatch(  TState currentState,
                                                            ref SequenceReader<byte> sequenceReader );

        protected TState GetCurrentState( ITokenTransition<TState>? previousTransition )
        {
            return previousTransition is not null ? previousTransition.To : this.DefaultState;
        }

        protected ITokenPatternMatch? FindNextMatch(    IEnumerable<ITokenPattern> patterns,
                                                        ref SequenceReader<byte> sequenceReader )
        {
            ITokenPatternMatch?[] matches = new ITokenPatternMatch?[ patterns.Count() ];

            long startIndex = sequenceReader.Consumed;

            ReadOnlySequence<byte> buffer = sequenceReader.Sequence;

            Parallel.ForEach(   patterns,
                                ( ITokenPattern pattern, ParallelLoopState state, long iteration ) =>
                                {
                                    SequenceReader<byte> matchReader = new SequenceReader<byte>( buffer );

                                    matchReader.Advance( startIndex );

                                    matches[ iteration ] = this.FindNextMatch( pattern, ref matchReader );
                                }   );

            ITokenPatternMatch? result = matches.Aggregate();

            if( result is not null )
            {
                sequenceReader.Advance( result.EndIndex - startIndex );
            }

            return result;
        }

        private ITokenPatternMatch? FindNextMatch( ITokenPattern pattern, ref SequenceReader<byte> sequenceReader )
        {
            TokenPatternMatch? result = null;

            ReadOnlySpan<byte> givenPattern = pattern.Value.AsSpan();

            if( sequenceReader.TryReadTo( out ReadOnlySequence<byte> _, givenPattern, advancePastDelimiter:true ) == true )
            {
                long startIndex = sequenceReader.Consumed - givenPattern.Length;

                result = new TokenPatternMatch( pattern, startIndex );
            }

            return result;
        }
    }
}
