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

namespace Reth.Wwks2.Protocol.Standard.Messages.Input
{
    public class InputRequestArticle:IEquatable<InputRequestArticle>
    {
        public static bool operator==( InputRequestArticle? left, InputRequestArticle? right )
		{
            return InputRequestArticle.Equals( left, right );
		}
		
		public static bool operator!=( InputRequestArticle? left, InputRequestArticle? right )
		{
			return !( InputRequestArticle.Equals( left, right ) );
		}

        public static bool Equals( InputRequestArticle? left, InputRequestArticle? right )
		{
            bool result = ArticleId.Equals( left?.Id, right?.Id );
            
            result &= ( result ? string.Equals( left?.FmdId, right?.FmdId, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? ( left?.Packs.SequenceEqual( right?.Packs ) ).GetValueOrDefault() : false );
            
            return result;
		}

        public InputRequestArticle()
        {
        }

        public InputRequestArticle( ArticleId? id,
                                    string? fmdId,
                                    IEnumerable<InputRequestPack>? packs  )
        {
            this.Id = id;
            this.FmdId = fmdId;

            if( packs is not null )
            {
                this.Packs = packs.ToList();
            }
        }

        public ArticleId? Id
        {
            get;
        }

        public string? FmdId
        {
            get;
        }

        public IReadOnlyList<InputRequestPack> Packs
        {
            get;
        } = Array.Empty<InputRequestPack>();
        
        public override bool Equals( object? obj )
		{
			return this.Equals( obj as InputRequestArticle );
		}
		
        public bool Equals( InputRequestArticle? other )
		{
            return InputRequestArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
