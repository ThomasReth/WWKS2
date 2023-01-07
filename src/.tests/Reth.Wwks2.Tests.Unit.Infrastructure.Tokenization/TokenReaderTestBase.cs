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
using FluentAssertions.Equivalency;

using Reth.Wwks2.Infrastructure.Tokenization;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Tokenization
{
    public abstract class TokenReaderTestBase<TTokenState>:TokenizationTestBase
        where TTokenState:Enum
    {
        protected abstract IEqualityComparer<string?> Comparer{ get; }

        protected abstract ITokenReader CreateTokenReader( Stream stream );

        public void Wait_WithCompleteMessage_ReturnsMessage( string expectedMessage )
        {
            using( Stream stream = this.GetStream( expectedMessage ) )
            {
                using( ITokenReader tokenReader = this.CreateTokenReader( stream ) )
                {
                    string actualMessage = tokenReader.Wait();

                    this.Comparer.Equals( expectedMessage, actualMessage ).Should().BeTrue();
                }
            }
        }
        
        public void Subscribe_WithMultipleSubscriptions_DispatchesMessageToAllSubscriptions( string expectedMessage )
        {
            List<string> actualMessages = new();
            List<string> expectedMessages = new();
            List<string> queuedMessages = new();
            
            expectedMessages.Add( expectedMessage );
            expectedMessages.Add( expectedMessage );

            queuedMessages.Add( expectedMessage );

            using( Stream stream = this.GetStream( expectedMessage ) )
            {
                using( ITokenReader tokenReader = this.CreateTokenReader( stream ) )
                {
                    IConnectableObservable<string> source = tokenReader.Publish();

                    using( Semaphore semaphore = new( initialCount:0, maximumCount:expectedMessages.Count ) )
                    {
                        for( int i = 0; i < expectedMessages.Count / queuedMessages.Count; i++ )
                        {
                            source.Subscribe(   ( string token ) =>
                                                {
                                                    actualMessages.Add( token );

                                                    semaphore.Release( releaseCount:1 );
                                                }   );
                        }
                    
                        using( source.Connect() )
                        {
                            semaphore.WaitOne();

                            actualMessages.Should().BeEquivalentTo( expectedMessages,
                                                                    ( EquivalencyAssertionOptions<string> options ) =>
                                                                    {
                                                                        return options.Using( this.Comparer );
                                                                    }   );
                        }
                    }
                }
            }
        }
    }
}