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

namespace Reth.Wwks2.Protocol.Standard.Messages.KeepAlive
{
    public class KeepAliveRequest:SubscriberMessage, IEquatable<KeepAliveRequest>
    {
        public static bool operator==( KeepAliveRequest? left, KeepAliveRequest? right )
		{
            return KeepAliveRequest.Equals( left, right );
		}
		
		public static bool operator!=( KeepAliveRequest? left, KeepAliveRequest? right )
		{
			return !( KeepAliveRequest.Equals( left, right ) );
		}

        public static bool Equals( KeepAliveRequest? left, KeepAliveRequest? right )
		{
            return SubscriberMessage.Equals( left, right );
		}

		public KeepAliveRequest(	SubscriberId source,
                                    SubscriberId destination	)
        :
            base( source, destination )
        {
        }

        public KeepAliveRequest(	SubscriberId source,
                                    SubscriberId destination,
									MessageId id	)
        :
            base( source, destination, id )
        {
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as KeepAliveRequest );
		}
		
        public bool Equals( KeepAliveRequest? other )
		{
            return KeepAliveRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
