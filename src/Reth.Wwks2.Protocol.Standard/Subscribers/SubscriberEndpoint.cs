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

using Reth.Wwks2.Infrastructure.Messaging;
using Reth.Wwks2.Protocol.Standard.Messages.KeepAlive;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Wwks2.Protocol.Standard.Subscribers
{
    public abstract class SubscriberEndpoint:ISubscriberEndpoint
    {
        private bool isDisposed;

        protected SubscriberEndpoint( IMessageEndpoint messageEndpoint )
        {
            this.MessageEndpoint = messageEndpoint;
        }

        public IMessageEndpoint MessageEndpoint
        {
            get;
        }

        public IDisposable Subscribe( IObserver<KeepAliveRequest> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public KeepAliveResponse SendRequest( KeepAliveRequest request )
        {
            return this.MessageEndpoint.SendRequest<KeepAliveRequest,KeepAliveResponse>( request );
        }

        public Task<KeepAliveResponse> SendRequestAsync( KeepAliveRequest request, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendRequestAsync<KeepAliveRequest, KeepAliveResponse>( request, cancellationToken );
        }

        public void SendResponse( KeepAliveResponse response )
        {
            this.MessageEndpoint.SendMessage( response );
        }

        public Task SendResponseAsync( KeepAliveResponse response, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( response, cancellationToken );
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
                    this.MessageEndpoint.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
