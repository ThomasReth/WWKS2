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

namespace Reth.Wwks2.Protocol.Standard.Messages.StockLocationInfo
{
    public class StockLocationInfoResponse:SubscriberMessage, IEquatable<StockLocationInfoResponse>
    {
        public static bool operator==( StockLocationInfoResponse? left, StockLocationInfoResponse? right )
		{
            return StockLocationInfoResponse.Equals( left, right );
		}
		
		public static bool operator!=( StockLocationInfoResponse? left, StockLocationInfoResponse? right )
		{
			return !( StockLocationInfoResponse.Equals( left, right ) );
		}

        public static bool Equals( StockLocationInfoResponse? left, StockLocationInfoResponse? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? ( left?.StockLocations.SequenceEqual( right?.StockLocations ) ).GetValueOrDefault() : false );

            return result;
		}

        public StockLocationInfoResponse(   SubscriberId source,
                                            SubscriberId destination,
                                            MessageId id,
                                            IEnumerable<StockLocation>? stockLocations  )
        :
            base( source, destination, id )
        {
            if( stockLocations is not null )
            {
                this.StockLocations = stockLocations.ToList();
            }
        }

        public StockLocationInfoResponse(   StockLocationInfoRequest request,
                                            IEnumerable<StockLocation>? stockLocations  )
        :
            base( request )
        {
            if( stockLocations is not null )
            {
                this.StockLocations = stockLocations.ToList();
            }
        }

        public IReadOnlyList<StockLocation> StockLocations
        {
            get;
        } = Array.Empty<StockLocation>();

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as StockLocationInfoResponse );
		}
		
        public bool Equals( StockLocationInfoResponse? other )
		{
            return StockLocationInfoResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
