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
    public class MessageId:Id<MessageId, ulong>
    {
        public static MessageId Parse( string value )
        {
            return new( ulong.Parse( value ) );
        }

        public static bool TryParse( string? value, out MessageId? result )
        {
            result = default( MessageId? );

            bool success = ulong.TryParse( value, out ulong resultValue );

            if( success == true )
            {
                result = new( resultValue );
            }

            return success;
        }

		public MessageId( ulong value )
        :
            base( value )
		{
		}
    }
}
