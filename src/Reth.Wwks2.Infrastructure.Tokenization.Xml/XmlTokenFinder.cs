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

namespace Reth.Wwks2.Infrastructure.Tokenization.Xml
{
    public class XmlTokenFinder:TokenFinder<XmlTokenState>
    {
        public XmlTokenFinder( Encoding encoding )
        :
            base( XmlTokenState.OutOfMessage )
        {
            this.LookupPatterns = new XmlTokenPatterns( encoding );
        }

        private XmlTokenPatterns LookupPatterns
        {
            get;
        }

        public override ITokenPatternMatch? FindNextMatch(  XmlTokenState currentState,
                                                            ref SequenceReader<byte> sequenceReader )
        {
            List<ITokenPattern> patterns = new List<ITokenPattern>();

            switch( currentState )
            {
                case XmlTokenState.OutOfMessage:
                    patterns.Add( this.LookupPatterns.BeginOfMessage );
                    break;

                case XmlTokenState.WithinMessage:
                    patterns.Add( this.LookupPatterns.BeginOfData );
                    patterns.Add( this.LookupPatterns.EndOfMessage );
                    break;

                case XmlTokenState.WithinData:
                    patterns.Add( this.LookupPatterns.EndOfData );
                    break;
            }

            return base.FindNextMatch(  patterns,
                                        ref sequenceReader  );
        }

        public override ITokenTransition<XmlTokenState>? CreateTransition(  IEnumerable<ITokenTransition<XmlTokenState>> transitions,
                                                                            ITokenPatternMatch nextMatch    )
        {
            ITokenTransition<XmlTokenState>? result = null;

            ITokenPattern pattern = nextMatch.Pattern;

            ITokenTransition<XmlTokenState>? previousTransition = transitions.LastOrDefault();

            XmlTokenState currentState = this.GetCurrentState( previousTransition );

            if( this.LookupPatterns.BeginOfMessage.Equals( pattern ) == true )
            {
                result = new XmlTokenTransition(    currentState,
                                                    XmlTokenState.WithinMessage,
                                                    nextMatch   );
            }else if( this.LookupPatterns.EndOfMessage.Equals( pattern ) == true )
            {
                result = new XmlTokenTransition(    currentState,
                                                    previousTransition?.From ?? this.DefaultState,
                                                    nextMatch   );
            }else if( this.LookupPatterns.BeginOfData.Equals( pattern ) == true )
            {
                result = new XmlTokenTransition(    currentState,
                                                    XmlTokenState.WithinData,
                                                    nextMatch   );
            }else if( this.LookupPatterns.EndOfData.Equals( pattern ) == true )
            {
                result = new XmlTokenTransition(    currentState,
                                                    previousTransition?.From ?? this.DefaultState,
                                                    nextMatch   );
            }else
            {
                throw new ArgumentException( $"Unknown token pattern in match '{ nextMatch.Pattern }'.", nameof( nextMatch ) );
            }

            return result;
        }
    }
}
