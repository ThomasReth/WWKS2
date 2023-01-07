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
    public class TaskCancelResponseTask:IEquatable<TaskCancelResponseTask>
    {
        public static bool operator==( TaskCancelResponseTask? left, TaskCancelResponseTask? right )
		{
            return TaskCancelResponseTask.Equals( left, right );
		}
		
		public static bool operator!=( TaskCancelResponseTask? left, TaskCancelResponseTask? right )
		{
			return !( TaskCancelResponseTask.Equals( left, right ) );
		}

        public static bool Equals( TaskCancelResponseTask? left, TaskCancelResponseTask? right )
		{
            bool result = string.Equals( left?.Id, right?.Id, StringComparison.OrdinalIgnoreCase );

            result &= ( result ? EqualityComparer<TaskCancelType?>.Default.Equals( left?.Type, right?.Type ) : false );
            result &= ( result ? EqualityComparer<TaskCancelStatus?>.Default.Equals( left?.Status, right?.Status ) : false );

            return result;
		}

        public TaskCancelResponseTask( string id, TaskCancelType type, TaskCancelStatus status )
        {
            this.Id = id;
            this.Type = type;
            this.Status = status;
        }

        public string Id
        {
            get;
        }

        public TaskCancelType Type
        {
            get;
        }

        public TaskCancelStatus Status
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as TaskCancelResponseTask );
		}
		
        public bool Equals( TaskCancelResponseTask? other )
		{
            return TaskCancelResponseTask.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{ this.Id }, { this.Type }, { this.Status }";
        }
    }
}
