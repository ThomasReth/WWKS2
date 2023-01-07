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

namespace Reth.Wwks2.Protocol.Standard.Messages
{
    public class PackDate:IComparable<PackDate>, IEquatable<PackDate>
    {
        public static bool operator==( PackDate? left, PackDate? right )
		{
            return PackDate.Equals( left, right );
		}
		
		public static bool operator!=( PackDate? left, PackDate? right )
		{
			return !( PackDate.Equals( left, right ) );
		}

        public static bool operator<( PackDate? left, PackDate? right )
		{
			return ( PackDate.Compare( left, right ) < 0 );
		}
		
		public static bool operator<=( PackDate? left, PackDate? right )
		{
			return ( PackDate.Compare( left, right ) <= 0 );
		}
		
		public static bool operator>( PackDate? left, PackDate? right )
		{
            return ( PackDate.Compare( left, right ) > 0 );
		}
		
		public static bool operator>=( PackDate? left, PackDate? right )
		{
			return ( PackDate.Compare( left, right ) >= 0 );
		}

        public static bool Equals( PackDate? left, PackDate? right )
		{
            return EqualityComparer<DateTimeOffset?>.Default.Equals( left?.Value, right?.Value );
		}

        public static int Compare( PackDate? left, PackDate? right )
        {
            int result = 0;

            if( object.ReferenceEquals( left, right ) == false )
            {
                if( left is null )
                {
                    result = -1;
                }else if( right is null )
                {
                    result = 1;
                }else
                {
                    result = DateTimeOffset.Compare( left.Value, right.Value );
                }
            }

            return result;
        }

        public static PackDate Parse( string value )
        {
            return new( DateTimeOffset.Parse( value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal ) );
        }

        public static bool TryParse( string value, out PackDate? result )
        {
            result = default;

            bool success = DateTimeOffset.TryParse( value, out DateTimeOffset timeStamp );

            if( success == true )
            {
                result = new( timeStamp );
            }
            
            return success;
        }

        public static PackDate UtcNow
        {
            get{ return new( DateTimeOffset.UtcNow ); }
        }

        public PackDate()
        :
            this( DateTimeOffset.UtcNow )
        {
        }

        public PackDate( DateTimeOffset value )
        {
            this.Value = value;
        }

        public PackDate( int year, int month, int day )
        {
            this.Value = new( year, month, day, 0, 0, 0, TimeSpan.Zero );
        }

        public DateTimeOffset Value
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as PackDate );
		}
		
		public bool Equals( PackDate? other )
		{
            return PackDate.Equals( this, other );
		}
		
        public int CompareTo( PackDate? other )
		{
            return PackDate.Compare( this, other );
		}

		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

        public override string ToString()
        {
            return string.Format( CultureInfo.InvariantCulture, "{0:yyyy-MM-dd}", this.Value );
        }
    }
}
