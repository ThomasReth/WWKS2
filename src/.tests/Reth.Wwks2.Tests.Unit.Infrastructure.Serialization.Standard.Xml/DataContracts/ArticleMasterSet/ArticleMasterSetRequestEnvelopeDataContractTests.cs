// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

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

using FluentAssertions;

using Reth.Wwks2.Protocol.Messages;
using Reth.Wwks2.Protocol.Standard.Messages;
using Reth.Wwks2.Protocol.Standard.Messages.ArticleMasterSet;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Serialization.Standard.Xml.DataContracts.ArticleMasterSet
{
    public class ArticleMasterSetRequestEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( string Xml, IMessageEnvelope Object ) Request
        {
            get
            {
                string name = "Aspirin";
                string dosageForm = "pills";
                string packagingUnit = "1x50 pills";
                string machineLocation = "main";
                StockLocationId stockLocationId = new StockLocationId( "default" );
                bool requiresFridge = true;
                int maxSubItemQuantity = 50;
                int depth = 20;
                int width = 30;
                int height = 15;
                int weight = 67;
                PackDate serialNumberSinceExpiryDate = new PackDate( 2024, 12, 2 );

                ArticleId articleId = new ArticleId( "4711" );

                ProductCode productCode = new( new ProductCodeId( "5783" ) );

                ArticleMasterSetArticle article = new(  articleId,
                                                        name,
                                                        dosageForm,
                                                        packagingUnit,
                                                        machineLocation,
                                                        stockLocationId,
                                                        requiresFridge,
                                                        maxSubItemQuantity,
                                                        depth,
                                                        width,
                                                        height,
                                                        weight,
                                                        serialNumberSinceExpiryDate,
                                                        new ProductCode[]
                                                        {
                                                            productCode
                                                        }   );

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <ArticleMasterSetRequest Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"">
                                        <Article    Id=""{ articleId }""
                                                    Name=""{ name }""
                                                    DosageForm=""{ dosageForm }""
                                                    PackagingUnit=""{ packagingUnit }""
                                                    MachineLocation=""{ machineLocation }""
                                                    StockLocationId=""{ stockLocationId }""
                                                    RequiresFridge=""{ requiresFridge }""
                                                    MaxSubItemQuantity=""{ maxSubItemQuantity }""
                                                    Depth=""{ depth }""
                                                    Width=""{ width }""
                                                    Height=""{ height }""
                                                    Weight=""{ weight }""
                                                    SerialNumberSinceExpiryDate=""{ serialNumberSinceExpiryDate }"">
                                            <ProductCode Code=""{ productCode.Code }"" />
                                        </Article>
                                    </ArticleMasterSetRequest>
                                </WWKS>",
                            new MessageEnvelope<ArticleMasterSetRequest>(   new ArticleMasterSetRequest(    XmlMessageTests.Source,
                                                                                                            XmlMessageTests.Destination,
                                                                                                            new ArticleMasterSetArticle[]
                                                                                                            {
                                                                                                                article
                                                                                                            },
                                                                                                            XmlMessageTests.MessageId   ),
                                                                            XmlMessageTests.Timestamp    ) );
            }
        }

        [Fact]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( ArticleMasterSetRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( ArticleMasterSetRequestEnvelopeDataContractTests.Request );

            result.Should().BeTrue();
        }
    }
}
