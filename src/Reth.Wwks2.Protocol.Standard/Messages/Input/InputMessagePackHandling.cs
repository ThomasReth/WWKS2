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

namespace Reth.Wwks2.Protocol.Standard.Messages.Input
{
    public class InputMessagePackHandling:IEquatable<InputMessagePackHandling>
    {
        public static bool operator==( InputMessagePackHandling? left, InputMessagePackHandling? right )
		{
            return InputMessagePackHandling.Equals( left, right );
		}
		
		public static bool operator!=( InputMessagePackHandling? left, InputMessagePackHandling? right )
		{
			return !( InputMessagePackHandling.Equals( left, right ) );
		}

        public static bool Equals( InputMessagePackHandling? left, InputMessagePackHandling? right )
		{
            bool result = EqualityComparer<InputMessagePackHandlingInput?>.Default.Equals( left?.Input, right?.Input );

            result &= ( result ? string.Equals( left?.Text, right?.Text, StringComparison.OrdinalIgnoreCase ) : false );

            return result;
		}

        public InputMessagePackHandling( InputMessagePackHandlingInput input )
        :
            this( input, null )
        {
        }

        public InputMessagePackHandling(    InputMessagePackHandlingInput input,
                                            string? text    )
        {
            this.Input = input;
            this.Text = text;
        }

        public InputMessagePackHandlingInput Input
        {
            get;
        }

        public string? Text
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as InputMessagePackHandling );
		}
		
        public bool Equals( InputMessagePackHandling? other )
		{
            return InputMessagePackHandling.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Input.GetHashCode();
        }

        public override string ToString()
        {
            return this.Input.ToString();
        }
    }
}
