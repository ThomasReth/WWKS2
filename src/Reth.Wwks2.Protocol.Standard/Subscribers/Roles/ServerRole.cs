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

using Reth.Wwks2.Protocol.Standard.Messages.Hello;

using System;

namespace Reth.Wwks2.Protocol.Standard.Subscribers.Roles
{
    public class ServerRole<TProxy>:Role<TProxy>, IServerRole<TProxy>
        where TProxy:notnull, ISubscriberEndpoint
    {
        public ServerRole( TProxy proxy )
        :
            base( proxy )
        {
        }

        public IDisposable Subscribe( IObserver<HelloRequest> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }
    }
}
