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

namespace Reth.Wwks2.Protocol.Standard.Messages.TaskCancel
{
    public class TaskCancelRequest:SubscriberMessage, IEquatable<TaskCancelRequest>
    {
        public static bool operator==( TaskCancelRequest? left, TaskCancelRequest? right )
		{
            return TaskCancelRequest.Equals( left, right );
		}
		
		public static bool operator!=( TaskCancelRequest? left, TaskCancelRequest? right )
		{
			return !( TaskCancelRequest.Equals( left, right ) );
		}

        public static bool Equals( TaskCancelRequest? left, TaskCancelRequest? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? ( left?.Tasks.SequenceEqual( right?.Tasks ) ).GetValueOrDefault() : false );

            return result;
		}

        public TaskCancelRequest(   SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<TaskCancelRequestTask>? tasks   )
        :
            base( source, destination )
        {
            if( tasks is not null )
            {
                this.Tasks = tasks.ToList();
            }
        }

        public TaskCancelRequest(   SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<TaskCancelRequestTask>? tasks,
                                    MessageId id    )
        :
            base( source, destination, id )
        {
            if( tasks is not null )
            {
                this.Tasks = tasks.ToList();
            }
        }

        public IReadOnlyList<TaskCancelRequestTask> Tasks
        {
            get;
        } = Array.Empty<TaskCancelRequestTask>();

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as TaskCancelRequest );
		}
		
        public bool Equals( TaskCancelRequest? other )
		{
            return TaskCancelRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
