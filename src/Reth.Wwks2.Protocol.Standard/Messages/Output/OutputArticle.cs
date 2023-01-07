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

namespace Reth.Wwks2.Protocol.Standard.Messages.Output
{
    public class OutputArticle:IEquatable<OutputArticle>
    {
        public static bool operator==( OutputArticle? left, OutputArticle? right )
		{
            return OutputArticle.Equals( left, right );
		}
		
		public static bool operator!=( OutputArticle? left, OutputArticle? right )
		{
			return !( OutputArticle.Equals( left, right ) );
		}

        public static bool Equals( OutputArticle? left, OutputArticle? right )
		{
            bool result = ArticleId.Equals( left?.Id, right?.Id );
            
            result &= ( result ? ( left?.Packs.SequenceEqual( right?.Packs ) ).GetValueOrDefault() : false );
            
            return result;
		}

        public OutputArticle(   ArticleId id,
                                IEnumerable<OutputPack>? packs    )
        {
            this.Id = id;

            if( packs is not null )
            {
                this.Packs = packs.ToList();
            }
        }

        public ArticleId Id
        {
            get;
        }

        public IReadOnlyList<OutputPack> Packs
        {
            get;
        } = Array.Empty<OutputPack>();

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as OutputArticle );
		}

        public bool Equals( OutputArticle? other )
        {
            return OutputArticle.Equals( this, other );
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
