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

using Reth.Wwks2.Protocol.Messages;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Reth.Wwks2.Protocol.Standard.Messages.StockDeliverySet
{
    public class StockDeliverySetRequest:SubscriberMessage, IEquatable<StockDeliverySetRequest>
    {
        public static bool operator==( StockDeliverySetRequest? left, StockDeliverySetRequest? right )
		{
            return StockDeliverySetRequest.Equals( left, right );
		}
		
		public static bool operator!=( StockDeliverySetRequest? left, StockDeliverySetRequest? right )
		{
			return !( StockDeliverySetRequest.Equals( left, right ) );
		}

        public static bool Equals( StockDeliverySetRequest? left, StockDeliverySetRequest? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? ( left?.Deliveries.SequenceEqual( right?.Deliveries ) ).GetValueOrDefault() : false );

            return result;
		}

        public StockDeliverySetRequest( SubscriberId source,
                                        SubscriberId destination,
                                        IEnumerable<StockDelivery>? deliveries  )
        :
            base( source, destination )
        {
            if( deliveries is not null )
            {
                this.Deliveries = deliveries.ToList();
            }
        }

        public StockDeliverySetRequest( SubscriberId source,
                                        SubscriberId destination,
                                        IEnumerable<StockDelivery>? deliveries,
                                        MessageId id    )
        :
            base( source, destination, id )
        {
            if( deliveries is not null )
            {
                this.Deliveries = deliveries.ToList();
            }
        }

        public IReadOnlyList<StockDelivery> Deliveries
        {
            get;
        } = Array.Empty<StockDelivery>();

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as StockDeliverySetRequest );
		}
		
        public bool Equals( StockDeliverySetRequest? other )
		{
            return StockDeliverySetRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
