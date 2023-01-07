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
using Reth.Wwks2.Protocol.Standard.Messages.ArticleMasterSet;
using Reth.Wwks2.Protocol.Standard.Messages.ConfigurationGet;
using Reth.Wwks2.Protocol.Standard.Messages.InitiateInput;
using Reth.Wwks2.Protocol.Standard.Messages.Input;
using Reth.Wwks2.Protocol.Standard.Messages.Output;
using Reth.Wwks2.Protocol.Standard.Messages.Status;
using Reth.Wwks2.Protocol.Standard.Messages.StockDeliverySet;
using Reth.Wwks2.Protocol.Standard.Messages.StockInfo;
using Reth.Wwks2.Protocol.Standard.Messages.StockLocationInfo;
using Reth.Wwks2.Protocol.Standard.Messages.TaskCancel;
using Reth.Wwks2.Protocol.Standard.Messages.TaskInfo;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Wwks2.Protocol.Standard.Subscribers.Contexts.StockManagement
{
    public class InventoryManagementSystemProxy:SubscriberEndpoint, IInventoryManagementSystemProxy
    {
        public InventoryManagementSystemProxy( IMessageEndpoint messageEndpoint )
        :
            base( messageEndpoint )
        {
        }

        public IDisposable Subscribe( IObserver<ArticleMasterSetRequest> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public IDisposable Subscribe( IObserver<ConfigurationGetRequest> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public IDisposable Subscribe( IObserver<InitiateInputRequest> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public IDisposable Subscribe( IObserver<OutputRequest> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public IDisposable Subscribe( IObserver<StatusRequest> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public IDisposable Subscribe( IObserver<StockDeliverySetRequest> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public IDisposable Subscribe( IObserver<StockInfoRequest> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public IDisposable Subscribe( IObserver<StockLocationInfoRequest> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public IDisposable Subscribe( IObserver<TaskCancelRequest> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public IDisposable Subscribe( IObserver<TaskInfoRequest> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public void SendMessage( InitiateInputMessage message )
        {
            this.MessageEndpoint.SendMessage( message );
        }

        public Task SendMessageAsync( InitiateInputMessage message, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( message, cancellationToken );
        }

        public void SendMessage( OutputMessage message )
        {
            this.MessageEndpoint.SendMessage( message );
        }

        public Task SendMessageAsync( OutputMessage message, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( message, cancellationToken );
        }

        public void SendMessage( StockInfoMessage message )
        {
            this.MessageEndpoint.SendMessage( message );
        }

        public Task SendMessageAsync( StockInfoMessage message, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( message, cancellationToken );
        }

        public InputResponse SendRequest( InputRequest request )
        {
            return this.MessageEndpoint.SendRequest<InputRequest, InputResponse>( request );
        }

        public Task<InputResponse> SendRequestAsync( InputRequest request, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendRequestAsync<InputRequest, InputResponse>( request, cancellationToken );
        }

        public void SendResponse( ArticleMasterSetResponse response )
        {
            this.MessageEndpoint.SendMessage( response );
        }

        public Task SendResponseAsync( ArticleMasterSetResponse response, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( response );
        }

        public void SendResponse( InitiateInputResponse response )
        {
            this.MessageEndpoint.SendMessage( response );
        }

        public Task SendResponseAsync( InitiateInputResponse response, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( response );
        }

        public void SendResponse( ConfigurationGetResponse response )
        {
            this.MessageEndpoint.SendMessage( response );
        }

        public Task SendResponseAsync( ConfigurationGetResponse response, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( response );
        }

        public void SendResponse( OutputResponse response )
        {
            this.MessageEndpoint.SendMessage( response );
        }

        public Task SendResponseAsync( OutputResponse response, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( response );
        }

        public void SendResponse( StatusResponse response )
        {
            this.MessageEndpoint.SendMessage( response );
        }

        public Task SendResponseAsync( StatusResponse response, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( response );
        }

        public void SendResponse( StockDeliverySetResponse response )
        {
            this.MessageEndpoint.SendMessage( response );
        }

        public Task SendResponseAsync( StockDeliverySetResponse response, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( response );
        }

        public void SendResponse( StockInfoResponse response )
        {
            this.MessageEndpoint.SendMessage( response );
        }

        public Task SendResponseAsync( StockInfoResponse response, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( response );
        }

        public void SendResponse( StockLocationInfoResponse response )
        {
            this.MessageEndpoint.SendMessage( response );
        }

        public Task SendResponseAsync( StockLocationInfoResponse response, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( response );
        }

        public void SendResponse( TaskCancelResponse response )
        {
            this.MessageEndpoint.SendMessage( response );
        }

        public Task SendResponseAsync( TaskCancelResponse response, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( response );
        }

        public void SendResponse( TaskInfoResponse response )
        {
            this.MessageEndpoint.SendMessage( response );
        }

        public Task SendResponseAsync( TaskInfoResponse response, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( response );
        }
    }
}
