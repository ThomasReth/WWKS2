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

namespace Reth.Wwks2.Protocol.Standard.Messages.Hello
{
    public class HelloResponse:Message, IEquatable<HelloResponse>
    {
        public static bool operator==( HelloResponse? left, HelloResponse? right )
		{
            return HelloResponse.Equals( left, right );
		}
		
		public static bool operator!=( HelloResponse? left, HelloResponse? right )
		{
			return !( HelloResponse.Equals( left, right ) );
		}

        public static bool Equals( HelloResponse? left, HelloResponse? right )
		{
            bool result = Message.Equals( left, right );

            result &= ( result ? EqualityComparer<Subscriber?>.Default.Equals( left?.Subscriber, right?.Subscriber ) : false );

            return result;
		}

        public HelloResponse( Subscriber subscriber, MessageId id )
        :
            base( id )
        {
            this.Subscriber = subscriber;
        }

        public HelloResponse( Subscriber subscriber, HelloRequest request )
        :
            this( subscriber, request.Id )
        {
        }

        public Subscriber Subscriber
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as HelloResponse );
		}
		
        public bool Equals( HelloResponse? other )
		{
            return HelloResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
