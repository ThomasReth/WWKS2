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
    public class InputRequestPack:IEquatable<InputRequestPack>
    {
        public static bool operator==( InputRequestPack? left, InputRequestPack? right )
		{
            return InputRequestPack.Equals( left, right );
		}
		
		public static bool operator!=( InputRequestPack? left, InputRequestPack? right )
		{
			return !( InputRequestPack.Equals( left, right ) );
		}

        public static bool Equals( InputRequestPack? left, InputRequestPack? right )
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

            return result;
		}

        public InputRequestPack( string scanCode )
        {
            scanCode.ThrowIfEmpty( "Scancode must not be empty." );

            this.ScanCode = scanCode;
        }

        public InputRequestPack(    string scanCode,
                                    string? deliveryNumber,
                                    string? batchNumber,
                                    string? externalId,
                                    string? serialNumber,
                                    string? machineLocation,
                                    StockLocationId? stockLocationId,
                                    PackDate? expiryDate,
                                    int? index,
                                    int? subItemQuantity    )
        {
            scanCode.ThrowIfEmpty( "Scancode must not be empty." );
            index?.ThrowIfNegative();
            subItemQuantity?.ThrowIfNegative();

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
        }

        public string ScanCode
        {
            get;
        } = string.Empty;
        
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

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as InputRequestPack );
		}
		
		public bool Equals( InputRequestPack? other )
		{
            return InputRequestPack.Equals( this, other );
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
