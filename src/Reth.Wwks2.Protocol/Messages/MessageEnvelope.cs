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
    public class MessageEnvelope<TMessage>:IMessageEnvelope, IEquatable<MessageEnvelope<TMessage>>
        where TMessage:IMessage
    {
        public const string DefaultVersion = "2.0";
        
        public static bool operator==( MessageEnvelope<TMessage>? left, MessageEnvelope<TMessage>? right )
		{
            return MessageEnvelope<TMessage>.Equals( left, right );
		}
		
		public static bool operator!=( MessageEnvelope<TMessage>? left, MessageEnvelope<TMessage>? right )
		{
			return !( MessageEnvelope<TMessage>.Equals( left, right ) );
		}

        public static bool Equals( MessageEnvelope<TMessage>? left, MessageEnvelope<TMessage>? right )
		{
            bool result = EqualityComparer<IMessage?>.Default.Equals( left?.Message, right?.Message );

            result &= ( result ? ( EqualityComparer<MessageTimestamp?>.Default.Equals( left?.Timestamp, right?.Timestamp ) ) : false );
            result &= ( result ? ( string.Equals( left?.Version, right?.Version, StringComparison.OrdinalIgnoreCase ) ) : false );

            return result;
		}

        public MessageEnvelope( TMessage message )
        {
            this.Message = message;
        }

        public MessageEnvelope( TMessage message, MessageTimestamp timestamp )
        {
            this.Message = message;
            this.Timestamp = timestamp;
        }

        public MessageEnvelope( TMessage message, MessageTimestamp timestamp, string version )
        {
            this.Message = message;
            this.Timestamp = timestamp;
            this.Version = version;
        }

        public IMessage Message
        {
            get;
        }

        public MessageTimestamp Timestamp
        {
            get;
        } = MessageTimestamp.UtcNow;
        
        public string Version
        {
            get;
        } = MessageEnvelope<TMessage>.DefaultVersion;

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as MessageEnvelope<TMessage> );
		}
		
        public bool Equals( MessageEnvelope<TMessage>? other )
		{
            return MessageEnvelope<TMessage>.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
            return HashCode.Combine( this.Message, this.Timestamp, this.Version );
		}

        public override string ToString()
        {
            return this.Message.ToString()!;
        }
    }
}
