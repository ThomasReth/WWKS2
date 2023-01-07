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

using System;

namespace Reth.Wwks2.Protocol.Standard.Messages.StockInfo
{
    public class StockInfoCriteria:IEquatable<StockInfoCriteria>
    {
        public static bool operator==( StockInfoCriteria? left, StockInfoCriteria? right )
		{
            return StockInfoCriteria.Equals( left, right );
		}
		
		public static bool operator!=( StockInfoCriteria? left, StockInfoCriteria? right )
		{
			return !( StockInfoCriteria.Equals( left, right ) );
		}

        public static bool Equals( StockInfoCriteria? left, StockInfoCriteria? right )
		{
            bool result = ArticleId.Equals( left?.ArticleId, right?.ArticleId );

            result &= ( result ? string.Equals( left?.BatchNumber, right?.BatchNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.ExternalId, right?.ExternalId, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.SerialNumber, right?.SerialNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.MachineLocation, right?.MachineLocation, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? StockLocationId.Equals( left?.StockLocationId, right?.StockLocationId ) : false );

            return result;
		}

        public StockInfoCriteria(   ArticleId? articleId,
                                    string? batchNumber,
                                    string? externalId,
                                    string? serialNumber,
                                    string? machineLocation,
                                    StockLocationId? stockLocationId    )
        {
            this.ArticleId = articleId;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
        }

        public ArticleId? ArticleId
        {
            get;
        }

        public string? BatchNumber
        {
            get;
        }

        public string? ExternalId
        {
            get;
        }

        public string? SerialNumber
        {
            get;
        }

        public string? MachineLocation
        {
            get;
        }

        public StockLocationId? StockLocationId
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as StockInfoCriteria );
		}
		
        public bool Equals( StockInfoCriteria? other )
		{
            return StockInfoCriteria.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return HashCode.Combine( this.ArticleId, this.BatchNumber );
		}
    }
}
