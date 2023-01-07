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

namespace Reth.Wwks2.Protocol.Standard.Messages.StockLocationInfo
{
    public class StockLocation:IEquatable<StockLocation>
    {
        public static bool operator==( StockLocation? left, StockLocation? right )
		{
            return StockLocation.Equals( left, right );
		}
		
		public static bool operator!=( StockLocation? left, StockLocation? right )
		{
			return !( StockLocation.Equals( left, right ) );
		}

        public static bool Equals( StockLocation? left, StockLocation? right )
		{
            bool result = StockLocationId.Equals( left?.Id, right?.Id );

            result &= ( result ? string.Equals( left?.Description, right?.Description, StringComparison.OrdinalIgnoreCase ) : false );
            
            return result;
		}

        public StockLocation( StockLocationId id )
        {
            this.Id = id;
        }

        public StockLocation(   StockLocationId id,
                                string? description )
        {
            this.Id = id;
            this.Description = description;
        }

        public StockLocationId Id
        {
            get;
        }

        public string? Description
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as StockLocation );
		}
		
		public bool Equals( StockLocation? other )
		{
            return StockLocation.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
