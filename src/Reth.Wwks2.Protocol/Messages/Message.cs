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

using System;
using System.Collections.Generic;

namespace Reth.Wwks2.Protocol.Messages
{
    public abstract class Message:IMessage, IEquatable<Message>
    {
        public static bool operator==( Message? left, Message? right )
		{
            return Message.Equals( left, right );
		}
		
		public static bool operator!=( Message? left, Message? right )
		{
			return !( Message.Equals( left, right ) );
		}

        public static bool Equals( Message? left, Message? right )
		{
            bool result = EqualityComparer<MessageId?>.Default.Equals( left?.Id, right?.Id );

            result &= ( result ? ( string.Equals( left?.Name, right?.Name, StringComparison.OrdinalIgnoreCase ) ) : false );

            return result;
		}

        protected Message()
        {
            this.Id = MessageIdGenerator.Default.NextId();
            this.Name = this.GetType().Name;
        }

        protected Message( MessageId id )
        {
            this.Id = id;
            this.Name = this.GetType().Name;
        }

        protected Message( MessageId id, string name )
        {
            this.Id = id;
            this.Name = name;
        }

        protected Message( Message other )
        :
            this( other.Id, other.Name )
        {
        }

        public MessageId Id
        {
            get;
        }

        public string Name
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as Message );
		}
		
        public bool Equals( Message? other )
		{
            return Message.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

        public override string ToString()
        {
            return $"{ this.Name } ({ this.Id })'";
        }
    }
}
