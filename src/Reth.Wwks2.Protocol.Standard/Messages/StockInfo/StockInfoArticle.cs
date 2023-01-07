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

namespace Reth.Wwks2.Protocol.Standard.Messages.StockInfo
{
    public class StockInfoArticle:IEquatable<StockInfoArticle>
    {
        public static bool operator==( StockInfoArticle? left, StockInfoArticle? right )
		{
            return StockInfoArticle.Equals( left, right );
		}
		
		public static bool operator!=( StockInfoArticle? left, StockInfoArticle? right )
		{
			return !( StockInfoArticle.Equals( left, right ) );
		}

        public static bool Equals( StockInfoArticle? left, StockInfoArticle? right )
		{
            bool result = ArticleId.Equals( left?.Id, right?.Id );

            result &= ( result ? ( EqualityComparer<int?>.Default.Equals( left?.Quantity, right?.Quantity ) ) : false );
            result &= ( result ? string.Equals( left?.Name, right?.Name, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.DosageForm, right?.DosageForm, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.PackagingUnit, right?.PackagingUnit, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.MaxSubItemQuantity, right?.MaxSubItemQuantity ) : false );
            result &= ( result ? ( left?.ProductCodes.SequenceEqual( right?.ProductCodes ) ).GetValueOrDefault() : false );
            result &= ( result ? ( left?.Packs.SequenceEqual( right?.Packs ) ).GetValueOrDefault() : false );

            return result;
		}

        public StockInfoArticle( ArticleId id, int quantity )
        {
            this.Id = id;
            this.Quantity = quantity;
        }

        public StockInfoArticle(    ArticleId id,
                                    int quantity,
                                    string? name,
                                    string? dosageForm,
                                    string? packagingUnit,
                                    int? maxSubItemQuantity,
                                    IEnumerable<ProductCode>? productCodes,
                                    IEnumerable<StockInfoPack>? packs    )
        {
            quantity.ThrowIfNotPositive();
            maxSubItemQuantity?.ThrowIfNegative();

            this.Id = id;
            this.Quantity = quantity;
            this.Name = name;
            this.DosageForm = dosageForm;
            this.PackagingUnit = packagingUnit;
            this.MaxSubItemQuantity = maxSubItemQuantity;

            if( productCodes is not null )
            {
                this.ProductCodes = productCodes.ToList();
            }

            if( packs is not null )
            {
                this.Packs = packs.ToList();
            }
        }

        public ArticleId Id
        {
            get;
        }

        public int Quantity
        {
            get;
        }

        public string? Name
        {
            get;
        }

        public string? DosageForm
        {
            get;
        }

        public string? PackagingUnit
        {
            get;
        }

        public int? MaxSubItemQuantity
        {
            get;
        }

        public IReadOnlyList<ProductCode> ProductCodes
        {
            get;
        } = Array.Empty<ProductCode>();

        public IReadOnlyList<StockInfoPack> Packs
        {
            get;
        } = Array.Empty<StockInfoPack>();
        
        public override bool Equals( object? obj )
		{
			return this.Equals( obj as StockInfoArticle );
		}
		
        public bool Equals( StockInfoArticle? other )
		{
            return StockInfoArticle.Equals( this, other );
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
