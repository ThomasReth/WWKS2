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
using System.Text;

namespace Reth.Wwks2.Infrastructure.Tokenization.Json
{
    internal class JsonTokenFinder:TokenFinder<JsonTokenState>
    {
        public JsonTokenFinder( Encoding encoding )
        :
            base( JsonTokenState.OutOfMessage )
        {
            this.LookupPatterns = new JsonTokenPatterns( encoding );
        }

        private JsonTokenPatterns LookupPatterns
        {
            get;
        }

        public override ITokenPatternMatch? FindNextMatch(  JsonTokenState currentState,
                                                            ref SequenceReader<byte> sequenceReader )
        {
            List<ITokenPattern> patterns = new List<ITokenPattern>();

            switch( currentState )
            {
                case JsonTokenState.OutOfMessage:
                    patterns.Add( this.LookupPatterns.BeginOfObject );
                    break;

                case JsonTokenState.WithinObject:
                    patterns.Add( this.LookupPatterns.BeginOfObject );
                    patterns.Add( this.LookupPatterns.BeginOfString );
                    patterns.Add( this.LookupPatterns.EndOfObject );
                    break;

                case JsonTokenState.WithinString:
                    patterns.Add( this.LookupPatterns.EndOfString );
                    break;
            }

            return base.FindNextMatch(  patterns,
                                        ref sequenceReader  );
        }

        public override ITokenTransition<JsonTokenState>? CreateTransition( IEnumerable<ITokenTransition<JsonTokenState>> transitions,
                                                                            ITokenPatternMatch nextMatch    )
        {
            ITokenTransition<JsonTokenState>? result = null;

            ITokenPattern pattern = nextMatch.Pattern;

            ITokenTransition<JsonTokenState>? previousTransition = transitions.LastOrDefault();

            JsonTokenState currentState = this.GetCurrentState( previousTransition );

            bool endOfMessage = this.EndOfMessage( transitions );            

            if( this.LookupPatterns.BeginOfObject.Equals( pattern ) == true )
            {
                result = new JsonTokenTransition(   currentState,
                                                    JsonTokenState.WithinObject,
                                                    nextMatch   );
            }else if( this.LookupPatterns.EndOfObject.Equals( pattern ) == true )
            {
                if( endOfMessage == true )
                {
                    result = new JsonTokenTransition(   currentState,
                                                        this.DefaultState,
                                                        nextMatch   );
                }else
                {
                    result = new JsonTokenTransition(   currentState,
                                                        JsonTokenState.WithinObject,
                                                        nextMatch   );
                }
            }else if( this.LookupPatterns.BeginOfString.Equals( pattern ) == true )
            {
                result = new JsonTokenTransition(   currentState,
                                                    JsonTokenState.WithinString,
                                                    nextMatch   );
            }else if( this.LookupPatterns.EndOfString.Equals( pattern ) == true )
            {
                result = new JsonTokenTransition(   currentState,
                                                    previousTransition?.From ?? this.DefaultState,
                                                    nextMatch   );
            }else
            {
                throw new ArgumentException( $"Unknown token pattern in match '{ nextMatch.Pattern }'.", nameof( nextMatch ) );
            }

            return result;
        }

        private bool EndOfMessage( IEnumerable<ITokenTransition<JsonTokenState>> transitions )
        {
            int level = 0;

            foreach( ITokenTransition<JsonTokenState> transition in transitions )
            {
                if( transition.Match.Pattern.Equals( this.LookupPatterns.BeginOfObject ) == true )
                {
                    level++;
                }else if( transition.Match.Pattern.Equals( this.LookupPatterns.EndOfObject ) == true )
                {
                    level--;
                }
            }

            return ( level == 1 );
        }
    }
}
