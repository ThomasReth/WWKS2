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
using System.Text;

namespace Reth.Wwks2.Infrastructure.Tokenization
{
    public abstract class TokenTransition<TState>:ITokenTransition<TState>
        where TState:Enum
    {
        public static bool EmbracesMessage( ITokenTransition<TState>? first,
                                            ITokenTransition<TState>? second )
        {
            bool isBegin = first?.IsMessageBegin() ?? false;
            bool isEnd = second?.IsMessageEnd() ?? false;

            return ( isBegin == true ) && ( isEnd == true );
        }

        public static ( long StartIndex, long Length ) GetMessageBoundaries(    ITokenTransition<TState> first,
                                                                                ITokenTransition<TState> second )
        {
            long startIndex = first.Match.StartIndex;
            long length = second.Match.EndIndex - startIndex;

            return ( startIndex, length );
        }

        protected TokenTransition(  TState from,
                                    TState to,
                                    ITokenPatternMatch match    )
        {
            this.From = from;
            this.To = to;
            this.Match = match;
        }

        public TState From
        {
            get;
        }

        public TState To
        {
            get;
        }

        public ITokenPatternMatch Match
        {
            get;
        }

        public abstract bool IsMessageBegin();
        public abstract bool IsMessageEnd();

        public override string ToString()
        {
            StringBuilder result = new();

            result.Append( this.From.ToString() );
            result.Append( " -> " );
            result.Append( this.To.ToString() );
            result.Append( ", by " );
            result.Append( this.Match.ToString() );

            return result.ToString();
        }
    }
}
