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

namespace Reth.Wwks2.Protocol.Standard.Messages.StockDeliverySet
{
    public class StockDeliverySetResponse:SubscriberMessage, IEquatable<StockDeliverySetResponse>
    {
        public static bool operator==( StockDeliverySetResponse? left, StockDeliverySetResponse? right )
		{
            return StockDeliverySetResponse.Equals( left, right );
		}
		
		public static bool operator!=( StockDeliverySetResponse? left, StockDeliverySetResponse? right )
		{
			return !( StockDeliverySetResponse.Equals( left, right ) );
		}

        public static bool Equals( StockDeliverySetResponse? left, StockDeliverySetResponse? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? StockDeliverySetResult.Equals( left?.Result, right?.Result ) : false );

            return result;
		}

        public StockDeliverySetResponse(    SubscriberId source,
                                            SubscriberId destination,
                                            MessageId id,
                                            StockDeliverySetResult result   )
        :
            base( source, destination, id )
        {
            this.Result = result;
        }

        public StockDeliverySetResponse(    StockDeliverySetRequest request,
                                            StockDeliverySetResult result   )
        :
            base( request )
        {
            this.Result = result;
        }

        public StockDeliverySetResult Result
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as StockDeliverySetResponse );
		}
		
        public bool Equals( StockDeliverySetResponse? other )
		{
            return StockDeliverySetResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
