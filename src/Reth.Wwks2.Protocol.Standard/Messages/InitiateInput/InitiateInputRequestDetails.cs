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

namespace Reth.Wwks2.Protocol.Standard.Messages.InitiateInput
{
    public class InitiateInputRequestDetails:IEquatable<InitiateInputRequestDetails>
    {
        public static bool operator==( InitiateInputRequestDetails? left, InitiateInputRequestDetails? right )
		{
            return InitiateInputRequestDetails.Equals( left, right );
		}
		
		public static bool operator!=( InitiateInputRequestDetails? left, InitiateInputRequestDetails? right )
		{
			return !( InitiateInputRequestDetails.Equals( left, right ) );
		}

        public static bool Equals( InitiateInputRequestDetails? left, InitiateInputRequestDetails? right )
		{
            bool result = EqualityComparer<int?>.Default.Equals( left?.InputSource, right?.InputSource );

            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.InputPoint, right?.InputPoint ) : false );

            return result;
		}
        
        public InitiateInputRequestDetails( int inputSource )
        :
            this( inputSource, null )
        {
        }

        public InitiateInputRequestDetails( int inputSource,
                                            int? inputPoint    )
        {
            inputSource.ThrowIfNegative();

            this.InputSource = inputSource;
            this.InputPoint = inputPoint;
        }

        public int InputSource{ get; }
        public int? InputPoint{ get; }
        
        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as InitiateInputRequestDetails );
		}
		
		public bool Equals( InitiateInputRequestDetails? other )
		{
            return InitiateInputRequestDetails.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

        public override string ToString()
        {
            return this.InputSource.ToString();
        }
    }
}
