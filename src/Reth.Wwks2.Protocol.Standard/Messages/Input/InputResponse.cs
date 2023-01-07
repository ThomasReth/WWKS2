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

namespace Reth.Wwks2.Protocol.Standard.Messages.Input
{
    public class InputResponse:SubscriberMessage, IEquatable<InputResponse>
    {
        public static bool operator==( InputResponse? left, InputResponse? right )
		{
            return InputResponse.Equals( left, right );
		}
		
		public static bool operator!=( InputResponse? left, InputResponse? right )
		{
			return !( InputResponse.Equals( left, right ) );
		}

        public static bool Equals( InputResponse? left, InputResponse? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IsNewDelivery, right?.IsNewDelivery ) : false );
            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );

            return result;
		}

        public InputResponse(   SubscriberId source,
                                SubscriberId destination,
                                MessageId id,
                                IEnumerable<InputResponseArticle>? articles,
                                bool? isNewDelivery   )
        :
            base( source, destination, id )
        {
            if( articles is not null )
            {
                this.Articles = articles.ToList();
            }

            this.IsNewDelivery = isNewDelivery;
        }

        public InputResponse(   InputRequest request,
                                IEnumerable<InputResponseArticle>? articles,
                                bool? isNewDelivery )
        :
            base( request )
        {
            if( articles is not null )
            {
                this.Articles = articles.ToList();
            }

            this.IsNewDelivery = isNewDelivery;
        }

        public bool? IsNewDelivery
        {
            get;
        }

        public IReadOnlyList<InputResponseArticle> Articles
        {
            get;
        } = Array.Empty<InputResponseArticle>();

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as InputResponse );
		}
		
        public bool Equals( InputResponse? other )
		{
            return InputResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
