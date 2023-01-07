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

using System.Text.Json.Serialization;

namespace Reth.Wwks2.Infrastructure.Serialization.Standard.Json.DataContracts.Output
{
    public class OutputCriteriaDataContract
    {
        public string Quantity{ get; init; } = string.Empty;

        public string? SubItemQuantity{ get; init; }

        public string? ArticleId{ get; init; }

        public string? PackId{ get; init; }

        public string? MinimumExpiryDate{ get; init; }

        public string? BatchNumber{ get; init; }

        public string? ExternalId{ get; init; }

        public string? SerialNumber{ get; init; }

        public string? MachineLocation{ get; init; }

        public string? StockLocationId{ get; init; }

        public string? SingleBatchNumber{ get; init; }
        
        [JsonPropertyName( "Label" )]
        public OutputLabelDataContract[]? Labels{ get; init; }
    }
}
