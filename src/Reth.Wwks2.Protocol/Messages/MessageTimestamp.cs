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
using System.Globalization;

namespace Reth.Wwks2.Protocol.Messages
{
    public sealed class MessageTimestamp:IEquatable<MessageTimestamp>
    {
        public static bool operator==( MessageTimestamp? left, MessageTimestamp? right )
		{
            return MessageTimestamp.Equals( left, right );
		}
		
		public static bool operator!=( MessageTimestamp? left, MessageTimestamp? right )
		{
			return !( MessageTimestamp.Equals( left, right ) );
		}

        public static bool Equals( MessageTimestamp? left, MessageTimestamp? right )
		{
            return EqualityComparer<DateTimeOffset?>.Default.Equals( left?.Value, right?.Value );
		}

        public static MessageTimestamp Parse( string value )
        {
            return new( DateTimeOffset.Parse( value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal ) );
        }

        public static bool TryParse( string? value, out MessageTimestamp? result )
        {
            result = default( MessageTimestamp );

            bool success = DateTimeOffset.TryParse( value, out DateTimeOffset timeStamp );

            if( success == true )
            {
                result = new( timeStamp );
            }
            
            return success;
        }

        public static MessageTimestamp UtcNow
        {
            get{ return new( DateTimeOffset.UtcNow ); }
        }

        public MessageTimestamp()
        :
            this( DateTimeOffset.UtcNow )
        {
        }

        public MessageTimestamp( DateTimeOffset value )
        {
            this.Value = value;
        }

        public DateTimeOffset Value
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as MessageTimestamp );
		}
		
        public bool Equals( MessageTimestamp? other )
		{
            return MessageTimestamp.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

        public override string ToString()
        {
            return string.Format( CultureInfo.InvariantCulture, "{0:yyyy-MM-ddTHH:mm:ssZ}", this.Value );
        }
    }
}
