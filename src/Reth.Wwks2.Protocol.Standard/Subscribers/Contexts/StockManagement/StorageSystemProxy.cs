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
    public class StorageSystemProxy:SubscriberEndpoint, IStorageSystemProxy
    {
        public StorageSystemProxy( IMessageEndpoint messageEndpoint )
        :
            base( messageEndpoint )
        {
        }

        public IDisposable Subscribe( IObserver<InitiateInputMessage> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public IDisposable Subscribe( IObserver<InputMessage> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public IDisposable Subscribe( IObserver<InputRequest> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public IDisposable Subscribe( IObserver<OutputMessage> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public IDisposable Subscribe( IObserver<StockInfoMessage> observer )
        {
            return this.MessageEndpoint.Subscribe( observer );
        }

        public ArticleMasterSetResponse SendRequest( ArticleMasterSetRequest request )
        {
            return this.MessageEndpoint.SendRequest<ArticleMasterSetRequest, ArticleMasterSetResponse>( request );
        }

        public Task<ArticleMasterSetResponse> SendRequestAsync( ArticleMasterSetRequest request, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendRequestAsync<ArticleMasterSetRequest, ArticleMasterSetResponse>( request, cancellationToken );
        }

        public InitiateInputResponse SendRequest( InitiateInputRequest request )
        {
            return this.MessageEndpoint.SendRequest<InitiateInputRequest, InitiateInputResponse>( request );
        }

        public Task<InitiateInputResponse> SendRequestAsync( InitiateInputRequest request, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendRequestAsync<InitiateInputRequest, InitiateInputResponse>( request, cancellationToken );
        }

        public ConfigurationGetResponse SendRequest( ConfigurationGetRequest request )
        {
            return this.MessageEndpoint.SendRequest<ConfigurationGetRequest, ConfigurationGetResponse>( request );
        }

        public Task<ConfigurationGetResponse> SendRequestAsync( ConfigurationGetRequest request, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendRequestAsync<ConfigurationGetRequest, ConfigurationGetResponse>( request, cancellationToken );
        }

        public OutputResponse SendRequest( OutputRequest request )
        {
            return this.MessageEndpoint.SendRequest<OutputRequest, OutputResponse>( request );
        }

        public Task<OutputResponse> SendRequestAsync( OutputRequest request, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendRequestAsync<OutputRequest, OutputResponse>( request, cancellationToken );
        }

        public StatusResponse SendRequest( StatusRequest request )
        {
            return this.MessageEndpoint.SendRequest<StatusRequest, StatusResponse>( request );
        }

        public Task<StatusResponse> SendRequestAsync( StatusRequest request, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendRequestAsync<StatusRequest, StatusResponse>( request, cancellationToken );
        }

        public StockDeliverySetResponse SendRequest( StockDeliverySetRequest request )
        {
            return this.MessageEndpoint.SendRequest<StockDeliverySetRequest, StockDeliverySetResponse>( request );
        }

        public Task<StockDeliverySetResponse> SendRequestAsync( StockDeliverySetRequest request, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendRequestAsync<StockDeliverySetRequest, StockDeliverySetResponse>( request, cancellationToken );
        }

        public StockInfoResponse SendRequest( StockInfoRequest request )
        {
            return this.MessageEndpoint.SendRequest<StockInfoRequest, StockInfoResponse>( request );
        }

        public Task<StockInfoResponse> SendRequestAsync( StockInfoRequest request, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendRequestAsync<StockInfoRequest, StockInfoResponse>( request, cancellationToken );
        }

        public StockLocationInfoResponse SendRequest( StockLocationInfoRequest request )
        {
            return this.MessageEndpoint.SendRequest<StockLocationInfoRequest, StockLocationInfoResponse>( request );
        }

        public Task<StockLocationInfoResponse> SendRequestAsync( StockLocationInfoRequest request, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendRequestAsync<StockLocationInfoRequest, StockLocationInfoResponse>( request, cancellationToken );
        }

        public TaskCancelResponse SendRequest( TaskCancelRequest request )
        {
            return this.MessageEndpoint.SendRequest<TaskCancelRequest, TaskCancelResponse>( request );
        }

        public Task<TaskCancelResponse> SendRequestAsync( TaskCancelRequest request, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendRequestAsync<TaskCancelRequest, TaskCancelResponse>( request, cancellationToken );
        }

        public TaskInfoResponse SendRequest( TaskInfoRequest request )
        {
            return this.MessageEndpoint.SendRequest<TaskInfoRequest, TaskInfoResponse>( request );
        }

        public Task<TaskInfoResponse> SendRequestAsync( TaskInfoRequest request, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendRequestAsync<TaskInfoRequest, TaskInfoResponse>( request, cancellationToken );
        }

        public void SendResponse( InputResponse response )
        {
            this.MessageEndpoint.SendMessage( response );
        }

        public Task SendResponseAsync( InputResponse response, CancellationToken cancellationToken = default )
        {
            return this.MessageEndpoint.SendMessageAsync( response, cancellationToken );
        }
    }
}
