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

namespace Reth.Wwks2.Protocol.Messages
{
    public class MessageIdGenerator
    {
        public static MessageIdGenerator Default
        {
            get;
        } = new();

        public static ulong DefaultId
        {
            get;
        } = default( ulong );

        public MessageIdGenerator()
        :
            this( MessageIdGenerator.DefaultId )
        {
        }

        public MessageIdGenerator( ulong seed )
        {
            this.NextIdValue = seed;
        }

        private object SyncRoot
        {
            get;
        } = new();
                
        private ulong NextIdValue
        {
            get; set;
        }

        public MessageId NextId()
        {
            lock( this.SyncRoot )
            {
                unchecked
                {
                    return new MessageId( this.NextIdValue++ );
                }
            }
        }
    }
}
