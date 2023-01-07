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
using System.Linq;

namespace Reth.Wwks2.Protocol.Standard.Messages.TaskInfo
{
    public class TaskInfoResponseTask:IEquatable<TaskInfoResponseTask>
    {
        public static bool operator==( TaskInfoResponseTask? left, TaskInfoResponseTask? right )
		{
            return TaskInfoResponseTask.Equals( left, right );
		}
		
		public static bool operator!=( TaskInfoResponseTask? left, TaskInfoResponseTask? right )
		{
			return !( TaskInfoResponseTask.Equals( left, right ) );
		}

        public static bool Equals( TaskInfoResponseTask? left, TaskInfoResponseTask? right )
		{
            bool result = string.Equals( left?.Id, right?.Id, StringComparison.OrdinalIgnoreCase );

            result &= ( result ? EqualityComparer<TaskInfoType?>.Default.Equals( left?.Type, right?.Type ) : false );
            result &= ( result ? EqualityComparer<TaskInfoStatus?>.Default.Equals( left?.Status, right?.Status ) : false );
            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );
            result &= ( result ? ( left?.Boxes.SequenceEqual( right?.Boxes ) ).GetValueOrDefault() : false );

            return result;
		}

        public TaskInfoResponseTask(    string id,
                                        TaskInfoType type,
                                        TaskInfoStatus status,
                                        IEnumerable<TaskInfoArticle>? articles,
                                        IEnumerable<Box>? boxes )
        {
            this.Id = id;
            this.Type = type;
            this.Status = status;

            if( articles is not null )
            {
                this.Articles = articles.ToList();
            }

            if( boxes is not null )
            {
                this.Boxes = boxes.ToList();
            }
        }

        public string Id
        {
            get;
        }

        public TaskInfoType Type
        {
            get;
        }

        public TaskInfoStatus Status
        {
            get;
        }

        public IReadOnlyList<TaskInfoArticle> Articles
        {
            get;
        } = Array.Empty<TaskInfoArticle>();

        public IReadOnlyList<Box> Boxes
        {
            get;
        } = Array.Empty<Box>();

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as TaskInfoResponseTask );
		}
		
        public bool Equals( TaskInfoResponseTask? other )
		{
            return TaskInfoResponseTask.Equals( this, other );
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
