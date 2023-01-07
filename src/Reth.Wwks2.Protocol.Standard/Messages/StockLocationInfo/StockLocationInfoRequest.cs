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

namespace Reth.Wwks2.Protocol.Standard.Messages.StockLocationInfo
{
    public class StockLocationInfoRequest:SubscriberMessage, IEquatable<StockLocationInfoRequest>
    {
        public static bool operator==( StockLocationInfoRequest? left, StockLocationInfoRequest? right )
		{
            return StockLocationInfoRequest.Equals( left, right );
		}
		
		public static bool operator!=( StockLocationInfoRequest? left, StockLocationInfoRequest? right )
		{
			return !( StockLocationInfoRequest.Equals( left, right ) );
		}

        public static bool Equals( StockLocationInfoRequest? left, StockLocationInfoRequest? right )
		{
            return SubscriberMessage.Equals( left, right );
		}

		public StockLocationInfoRequest(	SubscriberId source,
											SubscriberId destination	)
        :
            base( source, destination )
        {
        }

        public StockLocationInfoRequest(	SubscriberId source,
											SubscriberId destination,
											MessageId id	)
        :
            base( source, destination, id )
        {
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as StockLocationInfoRequest );
		}
		
        public bool Equals( StockLocationInfoRequest? other )
		{
            return StockLocationInfoRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
