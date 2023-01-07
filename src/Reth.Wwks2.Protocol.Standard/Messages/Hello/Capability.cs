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

namespace Reth.Wwks2.Protocol.Standard.Messages.Hello
{
    public class Capability:IEquatable<Capability>
    {
        public static bool operator==( Capability? left, Capability? right )
		{
            return Capability.Equals( left, right );
		}
		
		public static bool operator!=( Capability? left, Capability? right )
		{
			return !( Capability.Equals( left, right ) );
		}

        public static bool Equals( Capability? left, Capability? right )
		{
            return string.Equals( left?.Name, right?.Name, StringComparison.OrdinalIgnoreCase );
		}

        public Capability( string name )
        {
            this.Name = name;
        }
        
        public string Name
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as Capability );
		}
		
        public bool Equals( Capability? other )
		{
            return Capability.Equals( this, other );
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

        public override string ToString()
        {
            return this.Name;
        }
    }
}
