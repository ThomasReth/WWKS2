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

using Reth.Wwks2.Protocol.Standard.Messages;

using System.Xml;
using System.Xml.Serialization;

namespace Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.ArticleMasterSet
{
    public class ArticleMasterSetArticleDataContract
    {
        [XmlAttribute]
        public string Id{ get; init; } = string.Empty;

        [XmlAttribute]
        public string? Name{ get; init; }

        [XmlAttribute]
        public string? DosageForm{ get; init; }

        [XmlAttribute]
        public string? PackagingUnit{ get; init; }

        [XmlAttribute]
        public string? MachineLocation{ get; init; }

        [XmlAttribute]
        public string? StockLocationId{ get; init; }

        [XmlAttribute]
        public string? RequiresFridge{ get; init; }
        
        [XmlAttribute]
        public string? MaxSubItemQuantity{ get; init; }

        [XmlAttribute]
        public string? Depth{ get; init; }

        [XmlAttribute]
        public string? Width{ get; init; }

        [XmlAttribute]
        public string? Height{ get; init; }

        [XmlAttribute]
        public string? Weight{ get; init; }

        [XmlAttribute]
        public string? SerialNumberSinceExpiryDate{ get; init; }

        [XmlElement( nameof( ProductCode ) )]
        public ProductCodeDataContract[]? ProductCodes{ get; init; }
    }
}
