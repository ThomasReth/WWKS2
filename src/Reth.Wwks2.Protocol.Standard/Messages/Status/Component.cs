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

namespace Reth.Wwks2.Protocol.Standard.Messages.Status
{
    public class Component:IEquatable<Component>
    {
        public static bool operator==( Component? left, Component? right )
		{
            return Component.Equals( left, right );
		}
		
		public static bool operator!=( Component? left, Component? right )
		{
			return !( Component.Equals( left, right ) );
		}

        public static bool Equals( Component? left, Component? right )
		{
            bool result = EqualityComparer<ComponentType?>.Default.Equals( left?.Type, right?.Type );

            result &= ( result ? EqualityComparer<ComponentState?>.Default.Equals( left?.State, right?.State ) : false );
            result &= ( result ? string.Equals( left?.Description, right?.Description, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.StateText, right?.StateText, StringComparison.OrdinalIgnoreCase ) : false );
            
            return result;
		}

        public Component(   ComponentType type,
                            ComponentState state,                    
                            string description    )
        :
            this( type, state, description, stateText:null )
        {
        }

        public Component(   ComponentType type,
                            ComponentState state,                    
                            string description,
                            string? stateText    )
        {
            description.ThrowIfEmpty( "Component description must not be empty." );

            this.Type = type;
            this.State = state;
            this.Description = description;
            this.StateText = stateText;
        }

        public ComponentType Type
        {
            get;
        }

        public ComponentState State
        {
            get;
        }

        public string Description
        {
            get;
        }

        public string? StateText
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as Component );
		}
		
        public bool Equals( Component? other )
		{
            return Component.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return HashCode.Combine( this.Type, this.State, this.Description, this.StateText );
        }

        public override string ToString()
        {
            return $"{ this.Type } ({ this.State })"; 
        }
    }
}
