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

using Reth.Wwks2.Protocol.Messages;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Reth.Wwks2.Protocol.Standard.Messages.Output
{
    public class OutputRequest:SubscriberMessage, IEquatable<OutputRequest>
    {
        public static bool operator==( OutputRequest? left, OutputRequest? right )
		{
            return OutputRequest.Equals( left, right );
		}
		
		public static bool operator!=( OutputRequest? left, OutputRequest? right )
		{
			return !( OutputRequest.Equals( left, right ) );
		}

        public static bool Equals( OutputRequest? left, OutputRequest? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? OutputRequestDetails.Equals( left?.Details, right?.Details ) : false );
            result &= ( result ? string.Equals( left?.BoxNumber, right?.BoxNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? ( left?.Criteria.SequenceEqual( right?.Criteria ) ).GetValueOrDefault() : false );

            return result;
		}

        public OutputRequest(   SubscriberId source,
                                SubscriberId destination,
                                MessageId id,
                                OutputRequestDetails details,
                                IEnumerable<OutputCriteria>? criteria,
                                string? boxNumber   )
        :
            base( source, destination, id )
        {
            this.Details = details;
            
            if( criteria is not null )
            {
                this.Criteria = criteria.ToList();
            }

            this.BoxNumber = boxNumber;
        }

        public OutputRequestDetails Details
        {
            get;
        }

        public string? BoxNumber
        {
            get;
        }

        public IReadOnlyList<OutputCriteria> Criteria
        {
            get;
        } = Array.Empty<OutputCriteria>();

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as OutputRequest );
		}
		
        public bool Equals( OutputRequest? other )
		{
            return OutputRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
