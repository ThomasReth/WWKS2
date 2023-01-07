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

using Reth.Wwks2.Protocol.Messages;

using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Wwks2.Infrastructure.Messaging
{
    public class MessageEndpoint:IMessageEndpoint
    {
        private bool isDisposed;

        public MessageEndpoint( IMessageChannel messageChannel, TimeSpan responseTimeout )
        {
            this.MessageChannel = messageChannel;
            this.ResponseTimeout = responseTimeout;

            this.Source = messageChannel.Publish();
        }

        private IMessageChannel MessageChannel
        {
            get;
        }

        private TimeSpan ResponseTimeout
        {
            get;
        }

        private IConnectableObservable<IMessageEnvelope> Source
        {
            get;
        }

        public IDisposable Subscribe( IObserver<IMessageEnvelope> observer )
        {
            return this.Source.Subscribe( observer );
        }

        public IDisposable Subscribe<TMessage>( IObserver<TMessage> observer )where TMessage:class, IMessage
        {
            return (    from IMessageEnvelope messageEnvelope in this.Source
                        where messageEnvelope.Message is TMessage
                        select messageEnvelope.Message as TMessage  ).Subscribe( observer );
        }

        public IDisposable Connect()
        {
            return this.Source.Connect();
        }

        public TResponse SendRequest<TRequest, TResponse>( TRequest request )
            where TRequest:IMessage
            where TResponse:IMessage
        {
            return ( TResponse )this.SendRequest<TResponse>( new MessageEnvelope<TRequest>( request ) ).Message;
        }

        public async Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request, CancellationToken cancellationToken = default )
            where TRequest:IMessage
            where TResponse:IMessage
        {
            IMessageEnvelope response = await this.SendRequestAsync<TResponse>( new MessageEnvelope<TRequest>( request ), cancellationToken ).ConfigureAwait( continueOnCapturedContext:false );

            return ( TResponse )( response.Message );
        }

        public IMessageEnvelope SendRequest<TResponse>( IMessageEnvelope requestEnvelope )
            where TResponse:IMessage
        {
            return this.SendRequest<TResponse>( requestEnvelope, CancellationToken.None );
        }

        public Task<IMessageEnvelope> SendRequestAsync<TResponse>( IMessageEnvelope requestEnvelope, CancellationToken cancellationToken = default )
            where TResponse:IMessage
        {
            return Task.Run(    () =>
                                {
                                    return this.SendRequest<TResponse>( requestEnvelope, cancellationToken );
                                },
                                cancellationToken   );
        }

        private IMessageEnvelope SendRequest<TResponse>( IMessageEnvelope requestEnvelope, CancellationToken cancellationToken )
            where TResponse:IMessage
        {
            IMessageEnvelope? result = null;

            using( ManualResetEventSlim syncEvent = new() )
            {
                IObservable<IMessageEnvelope> source = this.Source.Where(   ( IMessageEnvelope messageEnvelope ) =>
                                                                            {
                                                                                IMessage message = messageEnvelope.Message;

                                                                                return  message.Id == requestEnvelope.Message.Id &&
                                                                                        message.GetType().Equals( typeof( TResponse ) );
                                                                            }   );

                using(  source.Subscribe(   ( IMessageEnvelope messageEnvelope ) =>
                                            {
                                                result = messageEnvelope;

                                                syncEvent.Set();
                                            }   )   )
                {               
                    using( source.Publish().Connect() )
                    {
                        this.MessageChannel.SendMessage( requestEnvelope );

                        if( syncEvent.Wait( this.ResponseTimeout, cancellationToken ) == false )
                        {
                            throw new TimeoutException( $"Waiting for message '{ typeof( TResponse ) }' with ID '{ requestEnvelope.Message.Id }' failed after { this.ResponseTimeout.TotalSeconds } seconds." );
                        }
                    }

                    return result!;
                }
            }
        }

        public void SendMessage<TMessage>( TMessage message )
            where TMessage:IMessage
        {
            this.SendMessage( new MessageEnvelope<TMessage>( message ) );
        }

        public Task SendMessageAsync<TMessage>( TMessage message, CancellationToken cancellationToken = default )
            where TMessage:IMessage
        {
            return this.SendMessageAsync( message, cancellationToken );
        }

        public void SendMessage( IMessageEnvelope messageEnvelope )
        {
            this.MessageChannel.SendMessage( messageEnvelope );
        }

        public Task SendMessageAsync( IMessageEnvelope messageEnvelope, CancellationToken cancellationToken = default )
        {
            return this.MessageChannel.SendMessageAsync( messageEnvelope, cancellationToken );
        }

        public void SendMessage( string messageEnvelope )
        {
            this.MessageChannel.SendMessage( messageEnvelope );
        }

        public Task SendMessageAsync( string messageEnvelope, CancellationToken cancellationToken = default )
        {
            return this.MessageChannel.SendMessageAsync( messageEnvelope, cancellationToken );
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.MessageChannel.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
