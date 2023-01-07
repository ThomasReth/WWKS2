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

using FluentAssertions;

using Reth.Wwks2.Infrastructure.Messaging;
using Reth.Wwks2.Infrastructure.Messaging.Transport.StreamBased;
using Reth.Wwks2.Infrastructure.Serialization;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml;
using Reth.Wwks2.Infrastructure.Tokenization;
using Reth.Wwks2.Infrastructure.Tokenization.Xml;
using Reth.Wwks2.Protocol.Messages;
using Reth.Wwks2.Tests.Unit.TestData.Xml;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Messaging.Transport.StreamBased
{
    public class MessageChannelTests:TestBase
    {
        [Fact]
        public void Subscribe_WithMultipleSubscriptions_DispatchesMessageToAllSubscriptions()
        {
            List<IMessageEnvelope> actualMessages = new();
            List<IMessageEnvelope> expectedMessages = new();
            List<string> queuedMessages = new();

            expectedMessages.Add( XmlTestData.HelloRequest.Object );
            expectedMessages.Add( XmlTestData.HelloRequest.Object );
            expectedMessages.Add( XmlTestData.KeepAliveRequest.Object );
            expectedMessages.Add( XmlTestData.KeepAliveRequest.Object );
                                    
            queuedMessages.Add( XmlTestData.HelloRequest.Xml );
            queuedMessages.Add( XmlTestData.KeepAliveRequest.Xml );

            using( Stream stream = this.GetStream( queuedMessages.Aggregate(    ( string total, string message ) =>
                                                                                {
                                                                                    return total + message;
                                                                                }   )   )   )
            {
                IMessageSerializer messageSerializer = new XmlMessageSerializer( this.Encoding );
                ITokenReader tokenReader = new XmlTokenReader( stream, this.Encoding );

                using( IMessageChannel messageChannel = new MessageChannel( messageSerializer,
                                                                            tokenReader,
                                                                            stream  )   )
                {
                    IConnectableObservable<IMessageEnvelope> source = messageChannel.Publish();

                    using( ManualResetEventSlim syncEvent = new( initialState:false ) )
                    {
                        int releaseCount = expectedMessages.Count;

                        for( int i = 0; i < expectedMessages.Count / queuedMessages.Count; i++ )
                        {
                            source.Subscribe(   ( IMessageEnvelope messageEnvelope ) =>
                                                {
                                                    actualMessages.Add( messageEnvelope );

                                                    --releaseCount;

                                                    if( releaseCount == 0 )
                                                    {
                                                        syncEvent.Set();
                                                    }
                                                }   );
                        }

                        source.Connect();
                    
                        syncEvent.Wait();

                        actualMessages.Count.Should().Be( expectedMessages.Count );
                        actualMessages.Should().BeEquivalentTo( expectedMessages );
                    }
                }
            }
        }
    }
}
