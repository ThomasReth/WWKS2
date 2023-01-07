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

namespace Reth.Wwks2.Protocol.Standard.Messages
{
    public abstract class SubscriberMessage:Message, IEquatable<SubscriberMessage>
    {
        public static bool operator==( SubscriberMessage? left, SubscriberMessage? right )
		{
            return SubscriberMessage.Equals( left, right );
		}
		
		public static bool operator!=( SubscriberMessage? left, SubscriberMessage? right )
		{
			return !( SubscriberMessage.Equals( left, right ) );
		}

        public static bool Equals( SubscriberMessage? left, SubscriberMessage? right )
		{
            bool result = Message.Equals( left, right );

            result &= ( result ? EqualityComparer<SubscriberId?>.Default.Equals( left?.Source, right?.Source ) : false );
            result &= ( result ? EqualityComparer<SubscriberId?>.Default.Equals( left?.Destination, right?.Destination ) : false );

            return result;
		}

        protected SubscriberMessage(    SubscriberId source,
                                        SubscriberId destination )
        {
            this.Source = source;
            this.Destination = destination;
        }

        protected SubscriberMessage(    SubscriberId source,
                                        SubscriberId destination,
                                        MessageId id    )
        :
            base( id )
        {
            this.Source = source;
            this.Destination = destination;
        }

        protected SubscriberMessage( SubscriberMessage request )
        :
            base( request.Id )
        {
            this.Source = request.Destination;
            this.Destination = request.Source;
        }

        public SubscriberId Source
        {
            get;
        }

        public SubscriberId Destination
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as SubscriberMessage );
		}
		
        public bool Equals( SubscriberMessage? other )
		{
            return SubscriberMessage.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
