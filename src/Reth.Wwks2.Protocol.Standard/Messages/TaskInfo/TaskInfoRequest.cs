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

namespace Reth.Wwks2.Protocol.Standard.Messages.TaskInfo
{
    public class TaskInfoRequest:SubscriberMessage, IEquatable<TaskInfoRequest>
    {
        public static bool operator==( TaskInfoRequest? left, TaskInfoRequest? right )
		{
            return TaskInfoRequest.Equals( left, right );
		}
		
		public static bool operator!=( TaskInfoRequest? left, TaskInfoRequest? right )
		{
			return !( TaskInfoRequest.Equals( left, right ) );
		}

        public static bool Equals( TaskInfoRequest? left, TaskInfoRequest? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IncludeTaskDetails, right?.IncludeTaskDetails ) : false );
            result &= ( result ? TaskInfoRequestTask.Equals( left?.Task, right?.Task ) : false );

            return result;
		}

        public TaskInfoRequest( SubscriberId source,
                                SubscriberId destination,
                                TaskInfoRequestTask task,
                                bool? includeTaskDetails    )
        :
            base( source, destination )
        {
            this.Task = task;
            this.IncludeTaskDetails = includeTaskDetails;
        }

        public TaskInfoRequest( SubscriberId source,
                                SubscriberId destination,
                                TaskInfoRequestTask task,
                                bool? includeTaskDetails,
                                MessageId id    )
        :
            base( source, destination, id )
        {
            this.Task = task;
            this.IncludeTaskDetails = includeTaskDetails;
        }

        public bool? IncludeTaskDetails
        {
            get;
        }

        public TaskInfoRequestTask Task
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as TaskInfoRequest );
		}
		
        public bool Equals( TaskInfoRequest? other )
		{
            return TaskInfoRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
