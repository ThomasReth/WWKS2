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

using Reth.Wwks2.Protocol.Standard.Messages.Status;

using System.Text.Json.Serialization;

namespace Reth.Wwks2.Infrastructure.Serialization.Standard.Json.DataContracts.Status
{
    public class StatusResponseDataContract:SubscriberMessageDataContract
    {
        public string State{ get; init; } = string.Empty;

        public string? StateText{ get; init; }

        [JsonPropertyName( nameof( Component ) )]
        public ComponentDataContract[]? Components{ get; init; }
    }
}