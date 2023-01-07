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
using System.Globalization;

namespace Reth.Wwks2.Protocol.Standard.Messages
{
    public class SubscriberId:Id<SubscriberId, int>
    {
        public const int MinReserved = 900;
        public const int MaxReserved = 999;

        public static SubscriberId Parse( string value )
        {
            try
            {
                return new( int.Parse( value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite ) );
            }catch( Exception ex )
            {
                throw new ArgumentException( $"The value '{ value }' is not a valid subscriber id.", ex );
            }
        }

        public static bool TryParse( string? value, out SubscriberId? result )
        {
            result = default( SubscriberId? );

            bool success = int.TryParse( value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out int number );
            
            if( success == true )
            {
                result = new( number );

                success = true;
            }

            return success;
        }

        public static SubscriberId DefaultIMS
        {
            get;
        } = new( 100 );

        public static SubscriberId DefaultRobot
        {
            get;
        } = new( 999 );

        public static bool IsReserved( int value )
        {
            bool result = false;

            if( value >= SubscriberId.MinReserved && value <= SubscriberId.MaxReserved )
            {
                result = true;
            }

            return result;
        }

        public SubscriberId( int value )
        :
            base( value )
        {
            value.ThrowIfNotPositive();
        }

        public bool IsReserved()
        {
            return SubscriberId.IsReserved( this.Value );
        }
    }
}
