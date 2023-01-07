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
using Reth.Wwks2.Protocol.Messages;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Wwks2.Infrastructure.Messaging
{
    public abstract class MessageChannelBase:IMessageChannel
    {
        protected MessageChannelBase( IMessageSerializer messageSerializer )
        {           
            this.MessageSerializer = messageSerializer;
        }

        protected IMessageSerializer MessageSerializer
        {
            get;
        }

        public abstract IDisposable Subscribe( IObserver<IMessageEnvelope> observer );
        
        public abstract void SendMessage( IMessageEnvelope messageEnvelope );

        public abstract Task SendMessageAsync( IMessageEnvelope messageEnvelope, CancellationToken cancellationToken = default );

        public abstract void SendMessage( string messageEnvelope );

        public abstract Task SendMessageAsync( string messageEnvelope, CancellationToken cancellationToken = default );

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
    }
}
