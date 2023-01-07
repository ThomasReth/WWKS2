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

namespace Reth.Wwks2.Protocol.Standard.Messages.TaskInfo
{
    public class TaskInfoPack:IEquatable<TaskInfoPack>
    {
        public static bool operator==( TaskInfoPack? left, TaskInfoPack? right )
		{
            return TaskInfoPack.Equals( left, right );
		}
		
		public static bool operator!=( TaskInfoPack? left, TaskInfoPack? right )
		{
			return !( TaskInfoPack.Equals( left, right ) );
		}

        public static bool Equals( TaskInfoPack? left, TaskInfoPack? right )
		{
            bool result = PackId.Equals( left?.Id, right?.Id );

            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.OutputDestination, right?.OutputDestination ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.OutputPoint, right?.OutputPoint ) : false );
            result &= ( result ? string.Equals( left?.DeliveryNumber, right?.DeliveryNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.BatchNumber, right?.BatchNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.ExternalId, right?.ExternalId, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.SerialNumber, right?.SerialNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.ScanCode, right?.ScanCode, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.BoxNumber, right?.BoxNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.MachineLocation, right?.MachineLocation, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? StockLocationId.Equals( left?.StockLocationId, right?.StockLocationId ) : false );
            result &= ( result ? PackDate.Equals( left?.ExpiryDate, right?.ExpiryDate ) : false );
            result &= ( result ? PackDate.Equals( left?.StockInDate, right?.StockInDate ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.SubItemQuantity, right?.SubItemQuantity ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Depth, right?.Depth ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Width, right?.Width ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Height, right?.Height ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Weight, right?.Weight ) : false );
            result &= ( result ? PackShape.Equals( left?.Shape, right?.Shape ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IsInFridge, right?.IsInFridge ) : false );
            result &= ( result ? EqualityComparer<LabelStatus?>.Default.Equals( left?.LabelStatus, right?.LabelStatus ) : false );

            return result;
		}

        public TaskInfoPack( PackId id, int outputDestination )
        {
            this.Id = id;
            this.OutputDestination = outputDestination;
        }

        public TaskInfoPack(    PackId id,
                                int outputDestination,
                                int? outputPoint,
                                string? deliveryNumber,
                                string? batchNumber,
                                string? externalId,
                                string? serialNumber,
                                string? scanCode,
                                string? boxNumber,
                                string? machineLocation,
                                StockLocationId? stockLocationId,
                                PackDate? expiryDate,
                                PackDate? stockInDate,
                                int? subItemQuantity,
                                int? depth,
                                int? width,
                                int? height,
                                
                                int? weight,

                                PackShape? shape,
                                bool? isInFridge,
                                LabelStatus? labelStatus    )
        {
            subItemQuantity?.ThrowIfNegative();
            depth?.ThrowIfNegative();
            width?.ThrowIfNegative();
            height?.ThrowIfNegative();
            weight?.ThrowIfNegative();

            this.Id = id;
            this.OutputDestination = outputDestination;
            this.OutputPoint = outputPoint;
            this.DeliveryNumber = deliveryNumber;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.ScanCode = scanCode;
            this.BoxNumber = boxNumber;
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
            this.ExpiryDate = expiryDate;
            this.StockInDate = stockInDate;
            this.SubItemQuantity = subItemQuantity;
            this.Depth = depth;
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
            this.Shape = shape;
            this.IsInFridge = isInFridge;
            this.LabelStatus = labelStatus;
        }

        public PackId Id
        {
            get;
        }

        public int OutputDestination
        {
            get;
        }

        public int? OutputPoint
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

        public string? ScanCode
        {
            get;
        }

        public string? BoxNumber
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

        public PackDate? StockInDate
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

        public bool? IsInFridge
        {
            get;
        }

        public LabelStatus? LabelStatus
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as TaskInfoPack );
		}
		
		public bool Equals( TaskInfoPack? other )
		{
            return TaskInfoPack.Equals( this, other );
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
