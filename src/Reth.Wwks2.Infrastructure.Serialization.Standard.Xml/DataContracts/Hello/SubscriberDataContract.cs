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
using System.Xml;
using System.Xml.Serialization;

namespace Reth.Wwks2.Infrastructure.Serialization.Standard.Xml.DataContracts.Hello
{
    public class SubscriberDataContract
    {
        [XmlAttribute]
        public string Id{ get; init; } = string.Empty;

        [XmlAttribute]
        public string Type{ get; init; } = string.Empty;

        [XmlAttribute]
        public string? TenantId{ get; init; }

        [XmlAttribute]
        public string Manufacturer{ get; init; } = string.Empty;

        [XmlAttribute]
        public string ProductInfo{ get; init; } = string.Empty;

        [XmlAttribute]
        public string VersionInfo{ get; init; } = string.Empty;

        [XmlElement( nameof( Capability ) )]
        public CapabilityDataContract[] Capabilities{ get; init; } = Array.Empty<CapabilityDataContract>();
    }
}
