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

namespace Reth.Wwks2.Protocol.Standard.Messages.StockInfo
{
    public class StockInfoResponse:SubscriberMessage, IEquatable<StockInfoResponse>
    {
        public static bool operator==( StockInfoResponse? left, StockInfoResponse? right )
		{
            return StockInfoResponse.Equals( left, right );
		}
		
		public static bool operator!=( StockInfoResponse? left, StockInfoResponse? right )
		{
			return !( StockInfoResponse.Equals( left, right ) );
		}

        public static bool Equals( StockInfoResponse? left, StockInfoResponse? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );

            return result;
		}

        public StockInfoResponse(   SubscriberId source,
                                    SubscriberId destination,
                                    MessageId id,
                                    IEnumerable<StockInfoArticle>? articles  )
        :
            base( source, destination, id )
        {
            if( articles is not null )
            {
                this.Articles = articles.ToList();
            }
        }

        public StockInfoResponse(   StockInfoRequest request,
                                    IEnumerable<StockInfoArticle>? articles  )
        :
            base( request )
        {
            if( articles is not null )
            {
                this.Articles = articles.ToList();
            }
        }

        public IReadOnlyList<StockInfoArticle> Articles
        {
            get;
        } = Array.Empty<StockInfoArticle>();

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as StockInfoResponse );
		}
		
        public bool Equals( StockInfoResponse? other )
		{
            return StockInfoResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
