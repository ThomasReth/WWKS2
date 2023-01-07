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
    public class OutputMessageDetails:IEquatable<OutputMessageDetails>
    {
        public static bool operator==( OutputMessageDetails? left, OutputMessageDetails? right )
		{
            return OutputMessageDetails.Equals( left, right );
		}
		
		public static bool operator!=( OutputMessageDetails? left, OutputMessageDetails? right )
		{
			return !( OutputMessageDetails.Equals( left, right ) );
		}

        public static bool Equals( OutputMessageDetails? left, OutputMessageDetails? right )
		{
            bool result = EqualityComparer<int?>.Default.Equals( left?.OutputDestination, right?.OutputDestination );

            result &= ( result ? EqualityComparer<OutputMessageStatus?>.Default.Equals( left?.Status, right?.Status ) : false );
            result &= ( result ? EqualityComparer<OutputPriority?>.Default.Equals( left?.Priority, right?.Priority ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.OutputPoint, right?.OutputPoint ) : false );

            return result;
		}
        
        public OutputMessageDetails(    int outputDestination,
                                        OutputMessageStatus status )
        :
            this( outputDestination, status, priority:null, outputPoint:null )
        {
        }

        public OutputMessageDetails(    int outputDestination,
                                        OutputMessageStatus status,
                                        OutputPriority? priority,
                                        int? outputPoint   )
        {
            this.OutputDestination = outputDestination;
            this.Status = status;
            this.Priority = priority;
            this.OutputPoint = outputPoint;
        }

        public int OutputDestination
        {
            get;
        }

        public OutputMessageStatus Status
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
			return this.Equals( obj as OutputMessageDetails );
		}
		
		public bool Equals( OutputMessageDetails? other )
		{
            return OutputMessageDetails.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return HashCode.Combine( this.OutputDestination, this.Status );
		}

        public override string ToString()
        {
            return $"{ this.OutputDestination }, { this.Status }";
        }
    }
}
