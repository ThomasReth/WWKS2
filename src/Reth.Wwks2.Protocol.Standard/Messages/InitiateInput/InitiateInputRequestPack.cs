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

namespace Reth.Wwks2.Protocol.Standard.Messages.InitiateInput
{
    public class InitiateInputRequestPack:IEquatable<InitiateInputRequestPack>
    {
        public static bool operator==( InitiateInputRequestPack? left, InitiateInputRequestPack? right )
		{
            return InitiateInputRequestPack.Equals( left, right );
		}
		
		public static bool operator!=( InitiateInputRequestPack? left, InitiateInputRequestPack? right )
		{
			return !( InitiateInputRequestPack.Equals( left, right ) );
		}

        public static bool Equals( InitiateInputRequestPack? left, InitiateInputRequestPack? right )
		{
            bool result = string.Equals( left?.ScanCode, right?.ScanCode, StringComparison.OrdinalIgnoreCase );
            
            result &= ( result ? string.Equals( left?.DeliveryNumber, right?.DeliveryNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.BatchNumber, right?.BatchNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.ExternalId, right?.ExternalId, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.SerialNumber, right?.SerialNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.MachineLocation, right?.MachineLocation, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? StockLocationId.Equals( left?.StockLocationId, right?.StockLocationId ) : false );
            result &= ( result ? PackDate.Equals( left?.ExpiryDate, right?.ExpiryDate ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Index, right?.Index ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.SubItemQuantity, right?.SubItemQuantity ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Depth, right?.Depth ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Width, right?.Width ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Height, right?.Height ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Weight, right?.Weight ) : false );
            result &= ( result ? PackShape.Equals( left?.Shape, right?.Shape ) : false );
            
            return result;
		}

        public InitiateInputRequestPack( string scanCode )
        {
            scanCode.ThrowIfEmpty( "Scancode must not be empty." );

            this.ScanCode = scanCode;
        }

        public InitiateInputRequestPack(    string scanCode,
                                            string? deliveryNumber,
                                            string? batchNumber,
                                            string? externalId,
                                            string? serialNumber,
                                            string? machineLocation,
                                            StockLocationId? stockLocationId,
                                            PackDate? expiryDate,
                                            int? index,
                                            int? subItemQuantity,
                                            int? depth,
                                            int? width,
                                            int? height,
                                            int? weight,
                                            PackShape? shape    )
        {
            scanCode.ThrowIfEmpty( "Scancode must not be empty." );
            index?.ThrowIfNegative();
            subItemQuantity?.ThrowIfNegative();
            depth?.ThrowIfNegative();
            width?.ThrowIfNegative();
            height?.ThrowIfNegative();
            weight?.ThrowIfNegative();

            this.ScanCode = scanCode;
            this.DeliveryNumber = deliveryNumber;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
            this.ExpiryDate = expiryDate;
            this.Index = index;
            this.SubItemQuantity = subItemQuantity;
            this.Depth = depth;
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
            this.Shape = shape;
        }
        
        public string ScanCode
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

        public string? MachineLocation
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

        public PackShape? Shape
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as InitiateInputRequestPack );
		}
		
		public bool Equals( InitiateInputRequestPack? other )
		{
            return InitiateInputRequestPack.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.ScanCode.GetHashCode();
		}

        public override string ToString()
        {
            return this.ScanCode;
        }
    }
}
