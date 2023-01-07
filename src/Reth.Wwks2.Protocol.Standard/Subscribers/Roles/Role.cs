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

using System;

namespace Reth.Wwks2.Protocol.Standard.Subscribers.Roles
{
    public abstract class Role<TProxy>:IRole<TProxy>
        where TProxy:notnull, ISubscriberEndpoint
    {
        private bool isDisposed;

        protected Role( TProxy proxy )
        {
            this.Proxy = proxy;
        }

        public TProxy Proxy
        {
            get;
        }

        protected IMessageEndpoint MessageEndpoint
        {
            get{ return this.Proxy.MessageEndpoint; }
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
                    this.Proxy.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
