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
    public class InitiateInputResponseDetails:IEquatable<InitiateInputResponseDetails>
    {
        public static bool operator==( InitiateInputResponseDetails? left, InitiateInputResponseDetails? right )
		{
            return InitiateInputResponseDetails.Equals( left, right );
		}
		
		public static bool operator!=( InitiateInputResponseDetails? left, InitiateInputResponseDetails? right )
		{
			return !( InitiateInputResponseDetails.Equals( left, right ) );
		}

        public static bool Equals( InitiateInputResponseDetails? left, InitiateInputResponseDetails? right )
		{
            bool result = EqualityComparer<int?>.Default.Equals( left?.InputSource, right?.InputSource );

            result &= ( result ? EqualityComparer<InitiateInputResponseStatus?>.Default.Equals( left?.Status, right?.Status ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.InputPoint, right?.InputPoint ) : false );

            return result;
		}
        
        public InitiateInputResponseDetails(    InitiateInputResponseStatus status,
                                                int inputSource )
        :
            this( status, inputSource, null )
        {
        }

        public InitiateInputResponseDetails(    InitiateInputResponseStatus status,
                                                int inputSource,
                                                int? inputPoint )
        {
            inputSource.ThrowIfNegative();

            this.Status = status;
            this.InputSource = inputSource;
            this.InputPoint = inputPoint;
        }

        public InitiateInputResponseStatus Status
        {
            get;
        }

        public int InputSource
        {
            get;
        }

        public int? InputPoint
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as InitiateInputResponseDetails );
		}
		
		public bool Equals( InitiateInputResponseDetails? other )
		{
            return InitiateInputResponseDetails.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

        public override string ToString()
        {
            return $"{ this.InputSource } ({ this.Status })";
        }
    }
}
