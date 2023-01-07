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
    public class TaskInfoArticle:IEquatable<TaskInfoArticle>
    {
        public static bool operator==( TaskInfoArticle? left, TaskInfoArticle? right )
		{
            return TaskInfoArticle.Equals( left, right );
		}
		
		public static bool operator!=( TaskInfoArticle? left, TaskInfoArticle? right )
		{
			return !( TaskInfoArticle.Equals( left, right ) );
		}

        public static bool Equals( TaskInfoArticle? left, TaskInfoArticle? right )
		{
            bool result = ArticleId.Equals( left?.Id, right?.Id );
            
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Quantity, right?.Quantity ) : false );
            result &= ( result ? ( left?.Packs.SequenceEqual( right?.Packs ) ).GetValueOrDefault() : false );
            
            return result;
		}

        public TaskInfoArticle( ArticleId id )
        {
            this.Id = id;
        }

        public TaskInfoArticle( ArticleId id,
                                int? quantity,
                                IEnumerable<TaskInfoPack>? packs  )
        {
            this.Id = id;
            this.Quantity = quantity;

            if( packs is not null )
            {
                this.Packs = packs.ToList();
            }
        }

        public ArticleId Id
        {
            get;
        }

        public int? Quantity
        {
            get;
        }

        public IReadOnlyList<TaskInfoPack> Packs
        {
            get;
        } = Array.Empty<TaskInfoPack>();
        
        public override bool Equals( object? obj )
		{
			return this.Equals( obj as TaskInfoArticle );
		}
		
        public bool Equals( TaskInfoArticle? other )
		{
            return TaskInfoArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
