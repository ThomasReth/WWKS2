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
using System.Linq;

namespace Reth.Wwks2
{
    public static class ExtensionMethods
    {
        public static bool SequenceEqual<TSource>( this IEnumerable<TSource> instance, IEnumerable<TSource>? second )
        {
            return second is null ? false : Enumerable.SequenceEqual( instance, second );
        }

        public static void ThrowIfEmpty( this string value, string message )
        {
            bool isNullOrEmpty = string.IsNullOrEmpty( value );

            if( isNullOrEmpty == true )
            {
                throw new ArgumentException( message, nameof( value ) );
            }
        }

        public static void ThrowIfNegative( this int value )
        {
            if( value < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( value ), $"A negative value ({ value }) is not allowed." );
            }
        }

        public static void ThrowIfNegative( this long value )
        {
            if( value < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( value ), $"A negative value ({ value }) is not allowed." );
            }
        }

        public static void ThrowIfNegative( this decimal value )
        {
            if( value < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( value ), $"A negative value ({ value }) is not allowed." );
            }
        }

        public static void ThrowIfNotPositive( this int value )
        {
            if( value <= 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( value ), $"A positive value instead of ({ value }) is required." );
            }
        }

        public static void ThrowIfNotPositive( this decimal value )
        {
            if( value <= 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( value ), $"A positive value instead of ({ value }) is required." );
            }
        }
    }
}
