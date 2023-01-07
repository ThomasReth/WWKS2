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
    public class KeepAliveResponse:SubscriberMessage, IEquatable<KeepAliveResponse>
    {
        public static bool operator==( KeepAliveResponse? left, KeepAliveResponse? right )
		{
            return KeepAliveResponse.Equals( left, right );
		}
		
		public static bool operator!=( KeepAliveResponse? left, KeepAliveResponse? right )
		{
			return !( KeepAliveResponse.Equals( left, right ) );
		}

        public static bool Equals( KeepAliveResponse? left, KeepAliveResponse? right )
		{
            return SubscriberMessage.Equals( left, right );
		}

        public KeepAliveResponse(   SubscriberId source,
                                    SubscriberId destination,
                                    MessageId id    )
        :
            base( source, destination, id )
        {
        }

        public KeepAliveResponse( KeepAliveRequest request )
        :
            base( request )
        {
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as KeepAliveResponse );
		}
		
        public bool Equals( KeepAliveResponse? other )
		{
            return KeepAliveResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
