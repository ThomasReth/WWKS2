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

namespace Reth.Wwks2.Protocol.Standard.Messages.InitiateInput
{
    public class InitiateInputResponse:SubscriberMessage, IEquatable<InitiateInputResponse>
    {
        public static bool operator==( InitiateInputResponse? left, InitiateInputResponse? right )
		{
            return InitiateInputResponse.Equals( left, right );
		}
		
		public static bool operator!=( InitiateInputResponse? left, InitiateInputResponse? right )
		{
			return !( InitiateInputResponse.Equals( left, right ) );
		}

        public static bool Equals( InitiateInputResponse? left, InitiateInputResponse? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? InitiateInputResponseDetails.Equals( left?.Details, right?.Details ) : false );
            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IsNewDelivery, right?.IsNewDelivery ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.SetPickingIndicator, right?.SetPickingIndicator ) : false );

            return result;
		}

        public InitiateInputResponse(   SubscriberId source,
                                        SubscriberId destination,
                                        MessageId id,
                                        InitiateInputResponseDetails details,
                                        IEnumerable<InitiateInputResponseArticle>? articles,
                                        bool? isNewDelivery,
                                        bool? setPickingIndicator   )
        :
            base( source, destination, id )
        {
            this.Details = details;

            if( articles is not null )
            {
                this.Articles = articles.ToList();
            }
            
            this.IsNewDelivery = isNewDelivery;
            this.SetPickingIndicator = setPickingIndicator;
        }

        public InitiateInputResponse(   InitiateInputRequest request,
                                        InitiateInputResponseDetails details,
                                        IEnumerable<InitiateInputResponseArticle>? articles,
                                        bool? isNewDelivery,
                                        bool? setPickingIndicator   )
        :
            base( request )
        {
            this.Details = details;

            if( articles is not null )
            {
                this.Articles = articles.ToList();
            }
            
            this.IsNewDelivery = isNewDelivery;
            this.SetPickingIndicator = setPickingIndicator;
        }

        public bool? IsNewDelivery
        {
            get;
        }

        public bool? SetPickingIndicator
        {
            get;
        }

        public InitiateInputResponseDetails Details
        {
            get;
        }

        public IReadOnlyList<InitiateInputResponseArticle> Articles
        {
            get;
        } = Array.Empty<InitiateInputResponseArticle>();

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as InitiateInputResponse );
		}
		
        public bool Equals( InitiateInputResponse? other )
		{
            return InitiateInputResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
