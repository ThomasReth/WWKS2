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

namespace Reth.Wwks2.Protocol.Standard.Messages
{
    public class Box:IEquatable<Box>
    {
        public static bool operator==( Box? left, Box? right )
		{
            return Box.Equals( left, right );
		}
		
		public static bool operator!=( Box? left, Box? right )
		{
			return !( Box.Equals( left, right ) );
		}

        public static bool Equals( Box? left, Box? right )
		{
            return string.Equals( left?.Number, right?.Number, StringComparison.OrdinalIgnoreCase );
		}

        public Box( string number )
        {
            number.ThrowIfEmpty( "Box number must not be empty." );

            this.Number = number;
        }

        public string Number
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as Box );
		}
		
		public bool Equals( Box? other )
		{
            return Box.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Number.GetHashCode();
		}

        public override string ToString()
        {
            return this.Number;
        }
    }
}
