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
    public class StockInfoRequest:SubscriberMessage, IEquatable<StockInfoRequest>
    {
        public static bool operator==( StockInfoRequest? left, StockInfoRequest? right )
		{
            return StockInfoRequest.Equals( left, right );
		}
		
		public static bool operator!=( StockInfoRequest? left, StockInfoRequest? right )
		{
			return !( StockInfoRequest.Equals( left, right ) );
		}

        public static bool Equals( StockInfoRequest? left, StockInfoRequest? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IncludePacks, right?.IncludePacks ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IncludeArticleDetails, right?.IncludeArticleDetails ) : false );
            result &= ( result ? ( left?.Criteria.SequenceEqual( right?.Criteria ) ).GetValueOrDefault() : false );

            return result;
		}

        public StockInfoRequest(	SubscriberId source,
                                    SubscriberId destination,
                                    bool? includePacks,
                                    bool? includeArticleDetails,
                                    IEnumerable<StockInfoCriteria>? criteria )
        :
            base( source, destination )
        {
            this.IncludePacks = includePacks;
            this.IncludeArticleDetails = includeArticleDetails;

            if( criteria is not null )
            {
                this.Criteria = criteria.ToList();
            }
        }

        public StockInfoRequest(	SubscriberId source,
                                    SubscriberId destination,
                                    bool? includePacks,
                                    bool? includeArticleDetails,
                                    IEnumerable<StockInfoCriteria>? criteria,
                                    MessageId id    )
        :
            base( source, destination, id )
        {
            this.IncludePacks = includePacks;
            this.IncludeArticleDetails = includeArticleDetails;

            if( criteria is not null )
            {
                this.Criteria = criteria.ToList();
            }
        }

		public bool? IncludePacks
        {
            get;
        }

        public bool? IncludeArticleDetails
        {
            get;
        }

        public IReadOnlyList<StockInfoCriteria> Criteria
        {
            get;
        } = Array.Empty<StockInfoCriteria>();

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as StockInfoRequest );
		}
		
        public bool Equals( StockInfoRequest? other )
		{
            return StockInfoRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
