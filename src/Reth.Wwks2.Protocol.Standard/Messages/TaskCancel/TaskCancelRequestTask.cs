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

namespace Reth.Wwks2.Protocol.Standard.Messages.TaskCancel
{
    public class TaskCancelRequestTask:IEquatable<TaskCancelRequestTask>
    {
        public static bool operator==( TaskCancelRequestTask? left, TaskCancelRequestTask? right )
		{
            return TaskCancelRequestTask.Equals( left, right );
		}
		
		public static bool operator!=( TaskCancelRequestTask? left, TaskCancelRequestTask? right )
		{
			return !( TaskCancelRequestTask.Equals( left, right ) );
		}

        public static bool Equals( TaskCancelRequestTask? left, TaskCancelRequestTask? right )
		{
            bool result = string.Equals( left?.Id, right?.Id, StringComparison.OrdinalIgnoreCase );

            result &= ( result ? EqualityComparer<TaskCancelType?>.Default.Equals( left?.Type, right?.Type ) : false );
            
            return result;
		}

        public TaskCancelRequestTask( string id, TaskCancelType type )
        {
            this.Id = id;
            this.Type = type;
        }

        public string Id
        {
            get;
        }

        public TaskCancelType Type
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as TaskCancelRequestTask );
		}
		
        public bool Equals( TaskCancelRequestTask? other )
		{
            return TaskCancelRequestTask.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{ this.Id }, { this.Type }";
        }
    }
}
