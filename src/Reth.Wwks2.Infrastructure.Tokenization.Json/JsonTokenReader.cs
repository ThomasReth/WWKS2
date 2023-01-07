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

using System.IO;
using System.Text;

namespace Reth.Wwks2.Infrastructure.Tokenization.Json
{
    public class JsonTokenReader:TokenReader<JsonTokenState>
    {
        public JsonTokenReader( Stream stream,
                                Encoding encoding   )
        :
            base( stream, encoding )
        {
            this.TokenFinder = new JsonTokenFinder( encoding );
        }

        public JsonTokenReader( Stream stream,
                                Encoding encoding,
                                int bufferSize,
                                int maximumMessageSize  )
        :
            base( stream, encoding, bufferSize, maximumMessageSize )
        {
            this.TokenFinder = new JsonTokenFinder( encoding );
        }

        protected override ITokenFinder<JsonTokenState> TokenFinder
        {
            get;
        }
    }
}
