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
    public interface IInventoryManagementSystemProxy
    :
        IDisposable,
        IObservable<ArticleMasterSetRequest>,
        IObservable<ConfigurationGetRequest>,
        IObservable<InitiateInputRequest>,
        IObservable<OutputRequest>,
        IObservable<StatusRequest>,
        IObservable<StockDeliverySetRequest>,
        IObservable<StockInfoRequest>,
        IObservable<StockLocationInfoRequest>,
        IObservable<TaskCancelRequest>,
        IObservable<TaskInfoRequest>,
        ISubscriberEndpoint
    {
        void SendMessage( InitiateInputMessage message );
        Task SendMessageAsync( InitiateInputMessage message, CancellationToken cancellationToken = default );

        void SendMessage( OutputMessage message );
        Task SendMessageAsync( OutputMessage message, CancellationToken cancellationToken = default );

        void SendMessage( StockInfoMessage message );
        Task SendMessageAsync( StockInfoMessage message, CancellationToken cancellationToken = default );

        InputResponse SendRequest( InputRequest request );
        Task<InputResponse> SendRequestAsync( InputRequest request, CancellationToken cancellationToken = default );

        void SendResponse( ArticleMasterSetResponse response );
        Task SendResponseAsync( ArticleMasterSetResponse response, CancellationToken cancellationToken = default );

        void SendResponse( ConfigurationGetResponse response );
        Task SendResponseAsync( ConfigurationGetResponse response, CancellationToken cancellationToken = default );

        void SendResponse( InitiateInputResponse response );
        Task SendResponseAsync( InitiateInputResponse response, CancellationToken cancellationToken = default );

        void SendResponse( OutputResponse response );
        Task SendResponseAsync( OutputResponse response, CancellationToken cancellationToken = default );

        void SendResponse( StatusResponse response );
        Task SendResponseAsync( StatusResponse response, CancellationToken cancellationToken = default );

        void SendResponse( StockDeliverySetResponse response );
        Task SendResponseAsync( StockDeliverySetResponse response, CancellationToken cancellationToken = default );

        void SendResponse( StockInfoResponse response );
        Task SendResponseAsync( StockInfoResponse response, CancellationToken cancellationToken = default );

        void SendResponse( StockLocationInfoResponse response );
        Task SendResponseAsync( StockLocationInfoResponse response, CancellationToken cancellationToken = default );

        void SendResponse( TaskCancelResponse response );
        Task SendResponseAsync( TaskCancelResponse response, CancellationToken cancellationToken = default );

        void SendResponse( TaskInfoResponse response );
        Task SendResponseAsync( TaskInfoResponse response, CancellationToken cancellationToken = default );
    }
}
