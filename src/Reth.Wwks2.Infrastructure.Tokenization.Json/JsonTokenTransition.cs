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

namespace Reth.Wwks2.Infrastructure.Tokenization.Json
{
    internal class JsonTokenTransition:TokenTransition<JsonTokenState>
    {
        public JsonTokenTransition( JsonTokenState from,
                                    JsonTokenState to,
                                    ITokenPatternMatch match    )
        :
            base( from, to, match )
        {
        }

        public override bool IsMessageBegin()
        {
            return  this.From == JsonTokenState.OutOfMessage &&
                    this.To == JsonTokenState.WithinObject;
        }

        public override bool IsMessageEnd()
        {
            return  this.From == JsonTokenState.WithinObject &&
                    this.To == JsonTokenState.OutOfMessage;
        }
    }
}
