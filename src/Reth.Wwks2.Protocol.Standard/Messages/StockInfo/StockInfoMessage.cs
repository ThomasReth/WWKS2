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
    public class StockInfoMessage:SubscriberMessage, IEquatable<StockInfoMessage>
    {
        public static bool operator==( StockInfoMessage? left, StockInfoMessage? right )
		{
            return StockInfoMessage.Equals( left, right );
		}
		
		public static bool operator!=( StockInfoMessage? left, StockInfoMessage? right )
		{
			return !( StockInfoMessage.Equals( left, right ) );
		}

        public static bool Equals( StockInfoMessage? left, StockInfoMessage? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );

            return result;
		}

        public StockInfoMessage(    SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<StockInfoArticle>? articles  )
        :
            base( source, destination )
        {
            if( articles is not null )
            {
                this.Articles = articles.ToList();
            }
        }

        public StockInfoMessage(    SubscriberId source,
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

        public IReadOnlyList<StockInfoArticle> Articles
        {
            get;
        } = new List<StockInfoArticle>();
        
        public override bool Equals( object? obj )
		{
			return this.Equals( obj as StockInfoMessage );
		}
		
        public bool Equals( StockInfoMessage? other )
		{
            return StockInfoMessage.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
