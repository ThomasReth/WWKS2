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

namespace Reth.Wwks2.Protocol.Standard.Messages.Status
{
    public class StatusResponse:SubscriberMessage, IEquatable<StatusResponse>
    {
        public static bool operator==( StatusResponse? left, StatusResponse? right )
		{
            return StatusResponse.Equals( left, right );
		}
		
		public static bool operator!=( StatusResponse? left, StatusResponse? right )
		{
			return !( StatusResponse.Equals( left, right ) );
		}

        public static bool Equals( StatusResponse? left, StatusResponse? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? ( left?.Components.SequenceEqual( right?.Components ) ).GetValueOrDefault() : false );

            return result;
		}

        public StatusResponse(  SubscriberId source,
                                SubscriberId destination,
                                MessageId id,
                                ComponentState state,
                                string? stateText,
                                IEnumerable<Component>? components   )
        :
            base( source, destination, id )
        {
            this.State = state;
            this.StateText = stateText;

            if( components is not null )
            {
                this.Components = components.ToList();
            }
        }

        public StatusResponse(  StatusRequest request,
                                ComponentState state,
                                string? stateText,
                                IEnumerable<Component>? components  )
        :
            base( request )
        {
            this.State = state;
            this.StateText = stateText;

            if( components is not null )
            {
                this.Components = components.ToList();
            }
        }

        public ComponentState State
        {
            get;
        } = ComponentState.NotReady;

        public string? StateText
        {
            get;
        }

        public IReadOnlyList<Component> Components
        {
            get;
        } = Array.Empty<Component>();

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as StatusResponse );
		}
		
        public bool Equals( StatusResponse? other )
		{
            return StatusResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
