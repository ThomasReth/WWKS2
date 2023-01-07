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

namespace Reth.Wwks2.Protocol.Standard.Messages.Output
{
    public class OutputRequestDetails:IEquatable<OutputRequestDetails>
    {
        public static bool operator==( OutputRequestDetails? left, OutputRequestDetails? right )
		{
            return OutputRequestDetails.Equals( left, right );
		}
		
		public static bool operator!=( OutputRequestDetails? left, OutputRequestDetails? right )
		{
			return !( OutputRequestDetails.Equals( left, right ) );
		}

        public static bool Equals( OutputRequestDetails? left, OutputRequestDetails? right )
		{
            bool result = EqualityComparer<int?>.Default.Equals( left?.OutputDestination, right?.OutputDestination );

            result &= ( result ? EqualityComparer<OutputPriority?>.Default.Equals( left?.Priority, right?.Priority ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.OutputPoint, right?.OutputPoint ) : false );

            return result;
		}
        
        public OutputRequestDetails( int outputDestination )
        :
            this( outputDestination, null, null )
        {
        }

        public OutputRequestDetails(    int outputDestination,
                                        OutputPriority? priority,
                                        int? outputPoint   )
        {
            this.OutputDestination = outputDestination;
            this.Priority = priority;
            this.OutputPoint = outputPoint;
        }

        public int OutputDestination
        {
            get;
        }

        public OutputPriority? Priority
        {
            get;
        }

        public int? OutputPoint
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as OutputRequestDetails );
		}
		
		public bool Equals( OutputRequestDetails? other )
		{
            return OutputRequestDetails.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.OutputDestination.GetHashCode();
		}

        public override string ToString()
        {
            return this.OutputDestination.ToString();
        }
    }
}
