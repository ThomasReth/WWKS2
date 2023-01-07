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

namespace Reth.Wwks2.Protocol.Standard.Messages.Output
{
    public class OutputMessage:SubscriberMessage, IEquatable<OutputMessage>
    {
        public static bool operator==( OutputMessage? left, OutputMessage? right )
		{
            return OutputMessage.Equals( left, right );
		}
		
		public static bool operator!=( OutputMessage? left, OutputMessage? right )
		{
			return !( OutputMessage.Equals( left, right ) );
		}

        public static bool Equals( OutputMessage? left, OutputMessage? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? OutputMessageDetails.Equals( left?.Details, right?.Details ) : false );
            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );
            result &= ( result ? ( left?.Boxes.SequenceEqual( right?.Boxes ) ).GetValueOrDefault() : false );

            return result;
		}

        public OutputMessage(   SubscriberId source,
                                SubscriberId destination,
                                MessageId id,
                                OutputMessageDetails details,
                                IEnumerable<OutputArticle>? articles,
                                IEnumerable<Box>? boxes    )
        :
            base( source, destination, id )
        {
            this.Details = details;

            if( articles is not null )
            {
                this.Articles = articles.ToList();
            }

            if( boxes is not null )
            {
                this.Boxes = boxes.ToList();
            }
        }

        public OutputMessage(   OutputRequest request,
                                OutputMessageDetails details,
                                IEnumerable<OutputArticle>? articles,
                                IEnumerable<Box>? boxes    )
        :
            base( request )
        {
            this.Details = details;
            
            if( articles is not null )
            {
                this.Articles = articles.ToList();
            }

            if( boxes is not null )
            {
                this.Boxes = boxes.ToList();
            }
        }

        public OutputMessageDetails Details
        {
            get;
        }

        public IReadOnlyList<OutputArticle> Articles
        {
            get;
        } = Array.Empty<OutputArticle>();

        public IReadOnlyList<Box> Boxes
        {
            get;
        } = Array.Empty<Box>();

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as OutputMessage );
		}
		
        public bool Equals( OutputMessage? other )
		{
            return OutputMessage.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
