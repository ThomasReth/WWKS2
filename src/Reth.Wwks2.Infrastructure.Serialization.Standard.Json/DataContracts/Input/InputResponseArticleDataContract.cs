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

using System.Text.Json.Serialization;

namespace Reth.Wwks2.Infrastructure.Serialization.Standard.Json.DataContracts.Input
{
    public class InputResponseArticleDataContract
    {
        public string? Id{ get; init; }

        public string? Name{ get; init; }

        public string? DosageForm{ get; init; }

        public string? PackagingUnit{ get; init; }

        public string? RequiresFridge{ get; init; }
        
        public string? MaxSubItemQuantity{ get; init; }

        public string? SerialNumberSinceExpiryDate{ get; init; }

        [JsonPropertyName( nameof( ProductCode ) )]
        public ProductCodeDataContract[]? ProductCodes{ get; init; }

        [JsonPropertyName( "Pack" )]
        public InputResponsePackDataContract[]? Packs{ get; init; }
    }
}
