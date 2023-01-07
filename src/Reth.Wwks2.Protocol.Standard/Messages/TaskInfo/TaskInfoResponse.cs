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

namespace Reth.Wwks2.Protocol.Standard.Messages.TaskInfo
{
    public class TaskInfoResponse:SubscriberMessage, IEquatable<TaskInfoResponse>
    {
        public static bool operator==( TaskInfoResponse? left, TaskInfoResponse? right )
		{
            return TaskInfoResponse.Equals( left, right );
		}
		
		public static bool operator!=( TaskInfoResponse? left, TaskInfoResponse? right )
		{
			return !( TaskInfoResponse.Equals( left, right ) );
		}

        public static bool Equals( TaskInfoResponse? left, TaskInfoResponse? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? TaskInfoResponseTask.Equals( left?.Task, right?.Task ) : false );

            return result;
		}

        public TaskInfoResponse(    SubscriberId source,
                                    SubscriberId destination,
                                    MessageId id,
                                    TaskInfoResponseTask task   )
        :
            base( source, destination, id )
        {
            this.Task = task;
        }

        public TaskInfoResponse(    TaskInfoRequest request,
                                    TaskInfoResponseTask task  )
        :
            base( request )
        {
            this.Task = task;
        }

        public TaskInfoResponseTask Task
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as TaskInfoResponse );
		}
		
        public bool Equals( TaskInfoResponse? other )
		{
            return TaskInfoResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
