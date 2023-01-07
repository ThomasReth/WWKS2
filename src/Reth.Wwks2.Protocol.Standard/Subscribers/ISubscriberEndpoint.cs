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
    public interface ISubscriberEndpoint
    :
        IDisposable,
        IObservable<KeepAliveRequest>
    {
        IMessageEndpoint MessageEndpoint{ get; }

        KeepAliveResponse SendRequest( KeepAliveRequest request );
        Task<KeepAliveResponse> SendRequestAsync( KeepAliveRequest request, CancellationToken cancellationToken = default );

        void SendResponse( KeepAliveResponse response );
        Task SendResponseAsync( KeepAliveResponse response, CancellationToken cancellationToken = default );
    }
}
