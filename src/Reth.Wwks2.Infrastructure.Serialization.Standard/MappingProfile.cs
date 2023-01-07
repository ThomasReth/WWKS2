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

using AutoMapper;

using Reth.Wwks2.Protocol.Messages;
using Reth.Wwks2.Protocol.Standard.Messages;

namespace Reth.Wwks2.Infrastructure.Serialization.Standard
{
    public abstract class MappingProfile:Profile
    {
        protected MappingProfile()
        {
            this.CreateMap<string, MessageId>().ConvertUsing( value => MessageId.Parse( value ) );
            this.CreateMap<MessageId, string>().ConvertUsing( value => value.ToString() );

            this.CreateMap<string, MessageTimestamp>().ConvertUsing( value => MessageTimestamp.Parse( value ) );
            this.CreateMap<MessageTimestamp, string>().ConvertUsing( value => value.ToString() );

            this.CreateMap<string, SubscriberId>().ConvertUsing( value => SubscriberId.Parse( value ) );
            this.CreateMap<SubscriberId, string>().ConvertUsing( value => value.ToString() );
        }
    }
}
