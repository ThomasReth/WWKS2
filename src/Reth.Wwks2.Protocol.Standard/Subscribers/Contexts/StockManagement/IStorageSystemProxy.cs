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
    public interface IStorageSystemProxy
    :
        IDisposable,
        IObservable<InitiateInputMessage>,
        IObservable<InputMessage>,
        IObservable<InputRequest>,
        IObservable<OutputMessage>,
        IObservable<StockInfoMessage>,
        ISubscriberEndpoint
    {
        ArticleMasterSetResponse SendRequest( ArticleMasterSetRequest request );
        Task<ArticleMasterSetResponse> SendRequestAsync( ArticleMasterSetRequest request, CancellationToken cancellationToken = default );
        
        ConfigurationGetResponse SendRequest( ConfigurationGetRequest request );
        Task<ConfigurationGetResponse> SendRequestAsync( ConfigurationGetRequest request, CancellationToken cancellationToken = default );

        InitiateInputResponse SendRequest( InitiateInputRequest request );
        Task<InitiateInputResponse> SendRequestAsync( InitiateInputRequest request, CancellationToken cancellationToken = default );

        OutputResponse SendRequest( OutputRequest request );
        Task<OutputResponse> SendRequestAsync( OutputRequest request, CancellationToken cancellationToken = default );

        StatusResponse SendRequest( StatusRequest request );
        Task<StatusResponse> SendRequestAsync( StatusRequest request, CancellationToken cancellationToken = default );

        StockDeliverySetResponse SendRequest( StockDeliverySetRequest request );
        Task<StockDeliverySetResponse> SendRequestAsync( StockDeliverySetRequest request, CancellationToken cancellationToken = default );

        StockInfoResponse SendRequest( StockInfoRequest request );
        Task<StockInfoResponse> SendRequestAsync( StockInfoRequest request, CancellationToken cancellationToken = default );

        StockLocationInfoResponse SendRequest( StockLocationInfoRequest request );
        Task<StockLocationInfoResponse> SendRequestAsync( StockLocationInfoRequest request, CancellationToken cancellationToken = default );

        TaskCancelResponse SendRequest( TaskCancelRequest request );
        Task<TaskCancelResponse> SendRequestAsync( TaskCancelRequest request, CancellationToken cancellationToken = default );

        TaskInfoResponse SendRequest( TaskInfoRequest request );
        Task<TaskInfoResponse> SendRequestAsync( TaskInfoRequest request, CancellationToken cancellationToken = default );

        void SendResponse( InputResponse response );
        Task SendResponseAsync( InputResponse response, CancellationToken cancellationToken = default );
    }
}
