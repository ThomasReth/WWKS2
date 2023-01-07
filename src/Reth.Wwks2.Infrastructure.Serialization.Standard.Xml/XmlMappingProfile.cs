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

using Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts;
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
using Reth.Wwks2.Protocol.Standard.Messages;
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

using System.Xml;

namespace Reth.Wwks2.Infrastructure.Serialization.Standard.Xml
{
    internal class XmlMappingProfile:MappingProfile
    {
        public XmlMappingProfile()
        {
            this.CreateArticleMasterSetMap();
            this.CreateConfigurationGetMap();
            this.CreateHelloMap();
            this.CreateInitiateInputMap();
            this.CreateInputMap();
            this.CreateKeepAliveMap();
            this.CreateOutputMap();
            this.CreateStatusMap();
            this.CreateStockDeliverySetMap();
            this.CreateStockInfoMap();
            this.CreateStockLocationInfoMap();
            this.CreateTaskCancelMap();
            this.CreateTaskInfoMap();

            this.CreateMap<string?, ArticleId?>().ConvertUsing( value => value == null ? null : ArticleId.Parse( value ) );
            this.CreateMap<ArticleId?, string?>().ConvertUsing( value => value == null ? null : value.ToString() );

            this.CreateMap<string?, PackDate?>().ConvertUsing( value => value == null ? null : PackDate.Parse( value ) );
            this.CreateMap<PackDate?, string?>().ConvertUsing( value => value == null ? null : value.ToString() );

            this.CreateMap<string?, PackId?>().ConvertUsing( value => value == null ? null : PackId.Parse( value ) );
            this.CreateMap<PackId?, string?>().ConvertUsing( value => value == null ? null : value.ToString() );

            this.CreateMap<string?, StockLocationId?>().ConvertUsing( value => value == null ? null : StockLocationId.Parse( value ) );
            this.CreateMap<StockLocationId?, string?>().ConvertUsing( value => value == null ? null : value.ToString() );

            this.CreateMap<string, XmlCDataSection>().ConvertUsing( value => new XmlDocument().CreateCDataSection( value ) );
            this.CreateMap<XmlCDataSection, string>().ConvertUsing( value => value.Data );

            this.CreateMap<ProductCodeDataContract, ProductCode>().ReverseMap();
        }

        private void CreateArticleMasterSetMap()
        {
            this.CreateMap<ArticleMasterSetRequestEnvelopeDataContract, MessageEnvelope<ArticleMasterSetRequest>>().ReverseMap();
            this.CreateMap<ArticleMasterSetResponseEnvelopeDataContract, MessageEnvelope<ArticleMasterSetResponse>>().ReverseMap();
            this.CreateMap<ArticleMasterSetRequestDataContract, ArticleMasterSetRequest>().ReverseMap();
            this.CreateMap<ArticleMasterSetResponseDataContract, ArticleMasterSetResponse>().ReverseMap();

            this.CreateMap<ArticleMasterSetArticleDataContract, ArticleMasterSetArticle>().ReverseMap();
            this.CreateMap<ArticleMasterSetResultDataContract, ArticleMasterSetResult>().ReverseMap();
        }

        private void CreateConfigurationGetMap()
        {
            this.CreateMap<ConfigurationGetRequestEnvelopeDataContract, MessageEnvelope<ConfigurationGetRequest>>().ReverseMap();
            this.CreateMap<ConfigurationGetResponseEnvelopeDataContract, MessageEnvelope<ConfigurationGetResponse>>().ReverseMap();
            this.CreateMap<ConfigurationGetRequestDataContract, ConfigurationGetRequest>().ReverseMap();
            this.CreateMap<ConfigurationGetResponseDataContract, ConfigurationGetResponse>().ReverseMap();
        }

        private void CreateHelloMap()
        {
            this.CreateMap<HelloRequestEnvelopeDataContract, MessageEnvelope<HelloRequest>>().ReverseMap();
            this.CreateMap<HelloResponseEnvelopeDataContract, MessageEnvelope<HelloResponse>>().ReverseMap();
            this.CreateMap<HelloRequestDataContract, HelloRequest>().ReverseMap();
            this.CreateMap<HelloResponseDataContract, HelloResponse>().ReverseMap();

            this.CreateMap<SubscriberDataContract, Subscriber>().ReverseMap();
            this.CreateMap<CapabilityDataContract, Capability>().ReverseMap();
        }

        private void CreateInitiateInputMap()
        {
            this.CreateMap<InitiateInputMessageEnvelopeDataContract, MessageEnvelope<InitiateInputMessage>>().ReverseMap();
            this.CreateMap<InitiateInputRequestEnvelopeDataContract, MessageEnvelope<InitiateInputRequest>>().ReverseMap();
            this.CreateMap<InitiateInputResponseEnvelopeDataContract, MessageEnvelope<InitiateInputResponse>>().ReverseMap();
            this.CreateMap<InitiateInputMessageDataContract, InitiateInputMessage>().ReverseMap();
            this.CreateMap<InitiateInputRequestDataContract, InitiateInputRequest>().ReverseMap();
            this.CreateMap<InitiateInputResponseDataContract, InitiateInputResponse>().ReverseMap();

            this.CreateMap<InitiateInputErrorDataContract, InitiateInputError>().ReverseMap();
            this.CreateMap<InitiateInputMessageArticleDataContract, InitiateInputMessageArticle>().ReverseMap();
            this.CreateMap<InitiateInputMessageDetailsDataContract, InitiateInputMessageDetails>().ReverseMap();
            this.CreateMap<InitiateInputMessagePackDataContract, InitiateInputMessagePack>().ReverseMap();
            this.CreateMap<InitiateInputRequestArticleDataContract, InitiateInputRequestArticle>().ReverseMap();
            this.CreateMap<InitiateInputRequestDetailsDataContract, InitiateInputRequestDetails>().ReverseMap();
            this.CreateMap<InitiateInputRequestPackDataContract, InitiateInputRequestPack>().ReverseMap();
            this.CreateMap<InitiateInputResponseArticleDataContract, InitiateInputResponseArticle>().ReverseMap();
            this.CreateMap<InitiateInputResponseDetailsDataContract, InitiateInputResponseDetails>().ReverseMap();
            this.CreateMap<InitiateInputResponsePackDataContract, InitiateInputResponsePack>().ReverseMap();
        }

        private void CreateInputMap()
        {
            this.CreateMap<InputMessageEnvelopeDataContract, MessageEnvelope<InputMessage>>().ReverseMap();
            this.CreateMap<InputRequestEnvelopeDataContract, MessageEnvelope<InputRequest>>().ReverseMap();
            this.CreateMap<InputResponseEnvelopeDataContract, MessageEnvelope<InputResponse>>().ReverseMap();
            this.CreateMap<InputMessageDataContract, InputMessage>().ReverseMap();
            this.CreateMap<InputRequestDataContract, InputRequest>().ReverseMap();
            this.CreateMap<InputResponseDataContract, InputResponse>().ReverseMap();

            this.CreateMap<InputMessageArticleDataContract, InputMessageArticle>().ReverseMap();
            this.CreateMap<InputMessagePackDataContract, InputMessagePack>().ReverseMap();
            this.CreateMap<InputMessagePackHandlingDataContract, InputMessagePackHandling>().ReverseMap();
            this.CreateMap<InputRequestArticleDataContract, InputRequestArticle>().ReverseMap();
            this.CreateMap<InputRequestPackDataContract, InputRequestPack>().ReverseMap();
            this.CreateMap<InputResponseArticleDataContract, InputResponseArticle>().ReverseMap();
            this.CreateMap<InputResponsePackDataContract, InputResponsePack>().ReverseMap();
            this.CreateMap<InputResponsePackHandlingDataContract, InputResponsePackHandling>().ReverseMap();
        }

        private void CreateKeepAliveMap()
        {
            this.CreateMap<KeepAliveRequestEnvelopeDataContract, MessageEnvelope<KeepAliveRequest>>().ReverseMap();
            this.CreateMap<KeepAliveResponseEnvelopeDataContract, MessageEnvelope<KeepAliveResponse>>().ReverseMap();
            this.CreateMap<KeepAliveRequestDataContract, KeepAliveRequest>().ReverseMap();
            this.CreateMap<KeepAliveResponseDataContract, KeepAliveResponse>().ReverseMap();
        }

        private void CreateOutputMap()
        {
            this.CreateMap<OutputMessageEnvelopeDataContract, MessageEnvelope<OutputMessage>>().ReverseMap();
            this.CreateMap<OutputRequestEnvelopeDataContract, MessageEnvelope<OutputRequest>>().ReverseMap();
            this.CreateMap<OutputResponseEnvelopeDataContract, MessageEnvelope<OutputResponse>>().ReverseMap();
            this.CreateMap<OutputMessageDataContract, OutputMessage>().ReverseMap();
            this.CreateMap<OutputRequestDataContract, OutputRequest>().ReverseMap();
            this.CreateMap<OutputResponseDataContract, OutputResponse>().ReverseMap();

            this.CreateMap<OutputArticleDataContract, OutputArticle>().ReverseMap();
            this.CreateMap<BoxDataContract, Box>().ReverseMap();
            this.CreateMap<OutputCriteriaDataContract, OutputCriteria>().ReverseMap();
            this.CreateMap<OutputLabelDataContract, OutputLabel>().ReverseMap();
            this.CreateMap<OutputMessageDetailsDataContract, OutputMessageDetails>().ReverseMap();
            this.CreateMap<OutputPackDataContract, OutputPack>().ReverseMap();
            this.CreateMap<OutputRequestDetailsDataContract, OutputRequestDetails>().ReverseMap();
            this.CreateMap<OutputResponseDetailsDataContract, OutputResponseDetails>().ReverseMap();
        }

        private void CreateStatusMap()
        {
            this.CreateMap<StatusRequestEnvelopeDataContract, MessageEnvelope<StatusRequest>>().ReverseMap();
            this.CreateMap<StatusResponseEnvelopeDataContract, MessageEnvelope<StatusResponse>>().ReverseMap();
            this.CreateMap<StatusRequestDataContract, StatusRequest>().ReverseMap();
            this.CreateMap<StatusResponseDataContract, StatusResponse>().ReverseMap();

            this.CreateMap<ComponentDataContract, Component>().ReverseMap();
        }

        private void CreateStockDeliverySetMap()
        {
            this.CreateMap<StockDeliverySetRequestEnvelopeDataContract, MessageEnvelope<StockDeliverySetRequest>>().ReverseMap();
            this.CreateMap<StockDeliverySetResponseEnvelopeDataContract, MessageEnvelope<StockDeliverySetResponse>>().ReverseMap();
            this.CreateMap<StockDeliverySetRequestDataContract, StockDeliverySetRequest>().ReverseMap();
            this.CreateMap<StockDeliverySetResponseDataContract, StockDeliverySetResponse>().ReverseMap();

            this.CreateMap<StockDeliveryDataContract, StockDelivery>().ReverseMap();
            this.CreateMap<StockDeliveryLineDataContract, StockDeliveryLine>().ReverseMap();
            this.CreateMap<StockDeliverySetResultDataContract, StockDeliverySetResult>().ReverseMap();
        }

        private void CreateStockInfoMap()
        {
            this.CreateMap<StockInfoMessageEnvelopeDataContract, MessageEnvelope<StockInfoMessage>>().ReverseMap();
            this.CreateMap<StockInfoRequestEnvelopeDataContract, MessageEnvelope<StockInfoRequest>>().ReverseMap();
            this.CreateMap<StockInfoResponseEnvelopeDataContract, MessageEnvelope<StockInfoResponse>>().ReverseMap();
            this.CreateMap<StockInfoMessageDataContract, StockInfoMessage>().ReverseMap();
            this.CreateMap<StockInfoRequestDataContract, StockInfoRequest>().ReverseMap();
            this.CreateMap<StockInfoResponseDataContract, StockInfoResponse>().ReverseMap();

            this.CreateMap<StockInfoArticleDataContract, StockInfoArticle>().ReverseMap();
            this.CreateMap<StockInfoCriteriaDataContract, StockInfoCriteria>().ReverseMap();
            this.CreateMap<StockInfoPackDataContract, StockInfoPack>().ReverseMap();
        }

        private void CreateStockLocationInfoMap()
        {
            this.CreateMap<StockLocationInfoRequestEnvelopeDataContract, MessageEnvelope<StockLocationInfoRequest>>().ReverseMap();
            this.CreateMap<StockLocationInfoResponseEnvelopeDataContract, MessageEnvelope<StockLocationInfoResponse>>().ReverseMap();
            this.CreateMap<StockLocationInfoRequestDataContract, StockLocationInfoRequest>().ReverseMap();
            this.CreateMap<StockLocationInfoResponseDataContract, StockLocationInfoResponse>().ReverseMap();

            this.CreateMap<StockLocationDataContract, StockLocation>().ReverseMap();
        }

        private void CreateTaskCancelMap()
        {
            this.CreateMap<TaskCancelRequestEnvelopeDataContract, MessageEnvelope<TaskCancelRequest>>().ReverseMap();
            this.CreateMap<TaskCancelResponseEnvelopeDataContract, MessageEnvelope<TaskCancelResponse>>().ReverseMap();
            this.CreateMap<TaskCancelRequestDataContract, TaskCancelRequest>().ReverseMap();
            this.CreateMap<TaskCancelResponseDataContract, TaskCancelResponse>().ReverseMap();

            this.CreateMap<TaskCancelRequestTaskDataContract, TaskCancelRequestTask>().ReverseMap();
            this.CreateMap<TaskCancelResponseTaskDataContract, TaskCancelResponseTask>().ReverseMap();
        }

        private void CreateTaskInfoMap()
        {
            this.CreateMap<TaskInfoRequestEnvelopeDataContract, MessageEnvelope<TaskInfoRequest>>().ReverseMap();
            this.CreateMap<TaskInfoResponseEnvelopeDataContract, MessageEnvelope<TaskInfoResponse>>().ReverseMap();
            this.CreateMap<TaskInfoRequestDataContract, TaskInfoRequest>().ReverseMap();
            this.CreateMap<TaskInfoResponseDataContract, TaskInfoResponse>().ReverseMap();

            this.CreateMap<TaskInfoArticleDataContract, TaskInfoArticle>().ReverseMap();
            this.CreateMap<TaskInfoPackDataContract, TaskInfoPack>().ReverseMap();
            this.CreateMap<TaskInfoRequestTaskDataContract, TaskInfoRequestTask>().ReverseMap();
            this.CreateMap<TaskInfoResponseTaskDataContract, TaskInfoResponseTask>().ReverseMap();
        }
    }
}
