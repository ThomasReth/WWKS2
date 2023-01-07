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

namespace Reth.Wwks2.Protocol.Standard.Messages.Input
{
    public class InputResponsePack:IEquatable<InputResponsePack>
    {
        public static bool operator==( InputResponsePack? left, InputResponsePack? right )
		{
            return InputResponsePack.Equals( left, right );
		}
		
		public static bool operator!=( InputResponsePack? left, InputResponsePack? right )
		{
			return !( InputResponsePack.Equals( left, right ) );
		}

        public static bool Equals( InputResponsePack? left, InputResponsePack? right )
		{
            bool result = EqualityComparer<InputResponsePackHandling?>.Default.Equals( left?.Handling, right?.Handling );
            
            result &= ( result ? string.Equals( left?.DeliveryNumber, right?.DeliveryNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.BatchNumber, right?.BatchNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.ExternalId, right?.ExternalId, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.SerialNumber, right?.SerialNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? StockLocationId.Equals( left?.StockLocationId, right?.StockLocationId ) : false );
            result &= ( result ? PackDate.Equals( left?.ExpiryDate, right?.ExpiryDate ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Index, right?.Index ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.SubItemQuantity, right?.SubItemQuantity ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Depth, right?.Depth ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Width, right?.Width ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Height, right?.Height ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Weight, right?.Weight ) : false );

            return result;
		}
        
        public InputResponsePack( InputResponsePackHandling handling )
        {
            this.Handling = handling;
        }

        public InputResponsePack(   InputResponsePackHandling handling,
                                    string? deliveryNumber,
                                    string? batchNumber,
                                    string? externalId,
                                    string? serialNumber,
                                    StockLocationId? stockLocationId,
                                    PackDate? expiryDate,
                                    int? index,
                                    int? subItemQuantity,
                                    int? depth,
                                    int? width,
                                    int? height,
                                    int? weight )
        {
            index?.ThrowIfNegative();
            subItemQuantity?.ThrowIfNegative();
            depth?.ThrowIfNegative();
            width?.ThrowIfNegative();
            height?.ThrowIfNegative();
            weight?.ThrowIfNegative();

            this.Handling = handling;
            this.DeliveryNumber = deliveryNumber;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.StockLocationId = stockLocationId;
            this.ExpiryDate = expiryDate;
            this.Index = index;
            this.SubItemQuantity = subItemQuantity;
            this.Depth = depth;
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
        }
        
        public InputResponsePackHandling Handling
        {
            get;
        }

        public string? DeliveryNumber
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

        public StockLocationId? StockLocationId
        {
            get;
        }

        public PackDate? ExpiryDate
        {
            get;
        }

        public int? Index
        {
            get;
        }

        public int? SubItemQuantity
        {
            get;
        }

        public int? Depth
        {
            get;
        }

        public int? Width
        {
            get;
        }

        public int? Height
        {
            get;
        }

        public int? Weight
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as InputResponsePack );
		}
		
		public bool Equals( InputResponsePack? other )
		{
            return InputResponsePack.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Handling.GetHashCode();
		}

        public override string ToString()
        {
            return this.Handling.ToString();
        }
    }
}
