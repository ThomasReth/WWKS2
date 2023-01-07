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
using System.Collections.Generic;

namespace Reth.Wwks2.Protocol.Standard.Messages.StockDeliverySet
{
    public class StockDeliverySetResult:IEquatable<StockDeliverySetResult>
    {
        public static bool operator==( StockDeliverySetResult? left, StockDeliverySetResult? right )
		{
            return StockDeliverySetResult.Equals( left, right );
		}
		
		public static bool operator!=( StockDeliverySetResult? left, StockDeliverySetResult? right )
		{
			return !( StockDeliverySetResult.Equals( left, right ) );
		}

        public static bool Equals( StockDeliverySetResult? left, StockDeliverySetResult? right )
		{
            bool result = EqualityComparer<StockDeliverySetResultValue?>.Default.Equals( left?.Value, right?.Value );

            result &= ( result ? string.Equals( left?.Text, right?.Text, StringComparison.OrdinalIgnoreCase ) : false );
            
            return result;
		}

        public StockDeliverySetResult( StockDeliverySetResultValue value )
        :
            this( value, null )
        {
        }

        public StockDeliverySetResult( StockDeliverySetResultValue value, string? text )
        {
            this.Value = value;
            this.Text = text;
        }

        public StockDeliverySetResultValue Value
        {
            get;
        }

        public string? Text
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as StockDeliverySetResult );
		}
		
        public bool Equals( StockDeliverySetResult? other )
		{
            return StockDeliverySetResult.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
