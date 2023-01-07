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

using Reth.Wwks2.Infrastructure.Serialization.DataContracts;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.ArticleMasterSet;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.ConfigurationGet;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.Hello;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.InitiateInput;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.Input;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.KeepAlive;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.Output;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.Status;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.StockDeliverySet;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.StockInfo;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.StockLocationInfo;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.TaskCancel;
using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.TaskInfo;
using Reth.Wwks2.Protocol.Messages;
using Reth.Wwks2.Protocol.Standard.Messages.ArticleMasterSet;
using Reth.Wwks2.Protocol.Standard.Messages.ConfigurationGet;
using Reth.Wwks2.Protocol.Standard.Messages.Hello;
using Reth.Wwks2.Protocol.Standard.Messages.InitiateInput;
using Reth.Wwks2.Protocol.Standard.Messages.Input;
using Reth.Wwks2.Protocol.Standard.Messages.KeepAlive;
using Reth.Wwks2.Protocol.Standard.Messages.Output;
using Reth.Wwks2.Protocol.Standard.Messages.Status;
using Reth.Wwks2.Protocol.Standard.Messages.StockDeliverySet;
using Reth.Wwks2.Protocol.Standard.Messages.StockInfo;
using Reth.Wwks2.Protocol.Standard.Messages.StockLocationInfo;
using Reth.Wwks2.Protocol.Standard.Messages.TaskCancel;
using Reth.Wwks2.Protocol.Standard.Messages.TaskInfo;

namespace Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts
{
    internal class XmlDataContractResolver:DataContractResolver
    {
        public XmlDataContractResolver()
        {
            this.CreateMapping( typeof( ArticleMasterSetRequestEnvelopeDataContract ), typeof( MessageEnvelope<ArticleMasterSetRequest> ) );
            this.CreateMapping( typeof( ArticleMasterSetResponseEnvelopeDataContract ), typeof( MessageEnvelope<ArticleMasterSetResponse> ) );
            this.CreateMapping( typeof( ConfigurationGetRequestEnvelopeDataContract ), typeof( MessageEnvelope<ConfigurationGetRequest> ) );
            this.CreateMapping( typeof( ConfigurationGetResponseEnvelopeDataContract ), typeof( MessageEnvelope<ConfigurationGetResponse> ) );
            this.CreateMapping( typeof( HelloRequestEnvelopeDataContract ), typeof( MessageEnvelope<HelloRequest> ) );
            this.CreateMapping( typeof( HelloResponseEnvelopeDataContract ), typeof( MessageEnvelope<HelloResponse> ) );
            this.CreateMapping( typeof( InitiateInputMessageEnvelopeDataContract ), typeof( MessageEnvelope<InitiateInputMessage> ) );
            this.CreateMapping( typeof( InitiateInputRequestEnvelopeDataContract ), typeof( MessageEnvelope<InitiateInputRequest> ) );
            this.CreateMapping( typeof( InitiateInputResponseEnvelopeDataContract ), typeof( MessageEnvelope<InitiateInputResponse> ) );
            this.CreateMapping( typeof( InputMessageEnvelopeDataContract ), typeof( MessageEnvelope<InputMessage> ) );
            this.CreateMapping( typeof( InputRequestEnvelopeDataContract ), typeof( MessageEnvelope<InputRequest> ) );
            this.CreateMapping( typeof( InputResponseEnvelopeDataContract ), typeof( MessageEnvelope<InputResponse> ) );
            this.CreateMapping( typeof( KeepAliveRequestEnvelopeDataContract ), typeof( MessageEnvelope<KeepAliveRequest> ) );
            this.CreateMapping( typeof( KeepAliveResponseEnvelopeDataContract ), typeof( MessageEnvelope<KeepAliveResponse> ) );
            this.CreateMapping( typeof( OutputMessageEnvelopeDataContract ), typeof( MessageEnvelope<OutputMessage> ) );
            this.CreateMapping( typeof( OutputRequestEnvelopeDataContract ), typeof( MessageEnvelope<OutputRequest> ) );
            this.CreateMapping( typeof( OutputResponseEnvelopeDataContract ), typeof( MessageEnvelope<OutputResponse> ) );
            this.CreateMapping( typeof( StatusRequestEnvelopeDataContract ), typeof( MessageEnvelope<StatusRequest> ) );
            this.CreateMapping( typeof( StatusResponseEnvelopeDataContract ), typeof( MessageEnvelope<StatusResponse> ) );
            this.CreateMapping( typeof( StockDeliverySetRequestEnvelopeDataContract ), typeof( MessageEnvelope<StockDeliverySetRequest> ) );
            this.CreateMapping( typeof( StockDeliverySetResponseEnvelopeDataContract ), typeof( MessageEnvelope<StockDeliverySetResponse> ) );
            this.CreateMapping( typeof( StockInfoMessageEnvelopeDataContract ), typeof( MessageEnvelope<StockInfoMessage> ) );
            this.CreateMapping( typeof( StockInfoRequestEnvelopeDataContract ), typeof( MessageEnvelope<StockInfoRequest> ) );
            this.CreateMapping( typeof( StockInfoResponseEnvelopeDataContract ), typeof( MessageEnvelope<StockInfoResponse> ) );
            this.CreateMapping( typeof( StockLocationInfoRequestEnvelopeDataContract ), typeof( MessageEnvelope<StockLocationInfoRequest> ) );
            this.CreateMapping( typeof( StockLocationInfoResponseEnvelopeDataContract ), typeof( MessageEnvelope<StockLocationInfoResponse> ) );
            this.CreateMapping( typeof( TaskCancelRequestEnvelopeDataContract ), typeof( MessageEnvelope<TaskCancelRequest> ) );
            this.CreateMapping( typeof( TaskCancelResponseEnvelopeDataContract ), typeof( MessageEnvelope<TaskCancelResponse> ) );
            this.CreateMapping( typeof( TaskInfoRequestEnvelopeDataContract ), typeof( MessageEnvelope<TaskInfoRequest> ) );
            this.CreateMapping( typeof( TaskInfoResponseEnvelopeDataContract ), typeof( MessageEnvelope<TaskInfoResponse> ) );
        }
    }
}
