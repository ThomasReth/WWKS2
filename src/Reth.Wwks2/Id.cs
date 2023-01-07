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

namespace Reth.Wwks2
{
    public abstract class Id<TInstance, TValue>:IEquatable<TInstance>
        where TInstance:notnull, Id<TInstance, TValue>
        where TValue:struct
    {
        public static bool operator==( Id<TInstance, TValue>? left, Id<TInstance, TValue>? right )
		{
			return Id<TInstance, TValue>.Equals( left, right );
		}
		
		public static bool operator!=( Id<TInstance, TValue>? left, Id<TInstance, TValue>? right )
		{
			return !Id<TInstance, TValue>.Equals( left, right );
		}

        public static bool Equals( Id<TInstance, TValue>? left, Id<TInstance, TValue>? right )
		{
            bool result = true;

            if( left is not null )
            {
                result = left.Value.Equals( right?.Value );
            }else if( right is not null )
            {
                result = right.Value.Equals( left?.Value );
            }

            return result;
		}
        
		protected Id( TValue value )
		{
            this.Value = value;
		}

        public TValue Value
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as TInstance );
		}
		
        public bool Equals( TInstance? other )
		{
            return Id<TInstance, TValue>.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return this.Value.ToString()!;
        }
    }
}
