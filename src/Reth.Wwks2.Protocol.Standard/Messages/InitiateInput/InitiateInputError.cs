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
using System.Text;

namespace Reth.Wwks2.Protocol.Standard.Messages.InitiateInput
{
    public class InitiateInputError:IEquatable<InitiateInputError>
    {
        public static bool operator==( InitiateInputError? left, InitiateInputError? right )
		{
            return InitiateInputError.Equals( left, right );
		}
		
		public static bool operator!=( InitiateInputError? left, InitiateInputError? right )
		{
			return !( InitiateInputError.Equals( left, right ) );
		}

        public static bool Equals( InitiateInputError? left, InitiateInputError? right )
		{
            bool result = InitiateInputErrorType.Equals( left?.Type, right?.Type );
            
            result &= ( result ? string.Equals( left?.Text, right?.Text, StringComparison.OrdinalIgnoreCase ) : false );
            
            return result;
		}

        public InitiateInputError( InitiateInputErrorType type )
        {
            this.Type = type;
        }

        public InitiateInputError( InitiateInputErrorType type, string? text )
        {
            this.Type = type;
            this.Text = text;
        }

        public InitiateInputErrorType Type
        {
            get;
        }

        public string? Text
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as InitiateInputError );
		}
		
        public bool Equals( InitiateInputError? other )
		{
            return InitiateInputError.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Type.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append( this.Type.ToString() );

            if( this.Text is not null )
            {
                result.Append( " (" );
                result.Append( this.Text );
                result.Append( ")" );
            }

            return result.ToString();
        }
    }
}
