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

using Reth.Wwks2.Infrastructure.Serialization;
using Reth.Wwks2.Infrastructure.Tokenization;
using Reth.Wwks2.Protocol.Messages;

using System;
using System.IO;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Wwks2.Infrastructure.Messaging.Transport.StreamBased
{
    public class MessageChannel:MessageChannelBase
    {
        private bool isDisposed;

        public MessageChannel(  IMessageSerializer messageSerializer,
                                ITokenReader tokenReader,
                                Stream stream   )
        :
            this(   messageSerializer,
                    tokenReader,
                    stream,
                    tokenReader.Select( ( string messageEnvelope ) =>
                                        {
                                            return messageSerializer.Deserialize( messageEnvelope );
                                        }   )   )
        {
        }

        public MessageChannel(  IMessageSerializer messageSerializer,
                                ITokenReader tokenReader,
                                Stream stream,
                                IObservable<IMessageEnvelope> source )
        :
            base( messageSerializer )
        {
            this.TokenReader = tokenReader;
            
            this.Stream = stream;

            this.Source = source;
        }

        private ITokenReader TokenReader
        {
            get;
        }

        private Encoding Encoding
        {
            get{ return this.TokenReader.Encoding; }
        }

        private Stream Stream
        {
            get;
        }

        private IObservable<IMessageEnvelope> Source
        {
            get;
        }

        public override IDisposable Subscribe( IObserver<IMessageEnvelope> observer )
        {
            return this.Source.Subscribe( observer );
        }
        
        public override void SendMessage( IMessageEnvelope messageEnvelope )
        {
            string serializedMessage = this.MessageSerializer.Serialize( messageEnvelope );
            
            using( StreamWriter streamWriter = new( this.Stream, this.Encoding, leaveOpen:true ) )
            {
                streamWriter.Write( serializedMessage );
                streamWriter.Flush();
            }
        }

        public override async Task SendMessageAsync( IMessageEnvelope messageEnvelope, CancellationToken cancellationToken = default )
        {
            string serializedMessage = await this.MessageSerializer.SerializeAsync( messageEnvelope, cancellationToken ).ConfigureAwait( continueOnCapturedContext:false );
            
            using( StreamWriter streamWriter = new( this.Stream, this.Encoding, leaveOpen:true ) )
            {
                await streamWriter.WriteAsync( serializedMessage ).ConfigureAwait( continueOnCapturedContext:false );
                await streamWriter.FlushAsync().ConfigureAwait( continueOnCapturedContext:false );
            }
        }

        public override void SendMessage( string messageEnvelope )
        {            
            using( StreamWriter streamWriter = new( this.Stream, this.Encoding, leaveOpen:true ) )
            {
                streamWriter.Write( messageEnvelope );
                streamWriter.Flush();
            }
        }

        public override async Task SendMessageAsync( string messageEnvelope, CancellationToken cancellationToken = default )
        {            
            using( StreamWriter streamWriter = new( this.Stream, this.Encoding, leaveOpen:true ) )
            {
                await streamWriter.WriteAsync( messageEnvelope ).ConfigureAwait( continueOnCapturedContext:false );
                await streamWriter.FlushAsync().ConfigureAwait( continueOnCapturedContext:false );
            }
        }

        protected override void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.TokenReader.Dispose();
                    this.Stream.Dispose();
                }

                this.isDisposed = true;
            }

            base.Dispose( disposing );
        }
    }
}
