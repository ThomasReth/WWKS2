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

using System.Text;

namespace Reth.Wwks2.Infrastructure.Tokenization.Xml
{
    internal class XmlTokenPatterns
    {
        public XmlTokenPatterns( Encoding encoding )
        {
            this.BeginOfMessage = new BeginOfMessageTokenPattern( encoding );
            this.EndOfMessage = new EndOfMessageTokenPattern( encoding );
            this.BeginOfData = new BeginOfDataTokenPattern( encoding );
            this.EndOfData = new EndOfDataTokenPattern( encoding );
        }

        public BeginOfMessageTokenPattern BeginOfMessage
        {
            get;
        }

        public EndOfMessageTokenPattern EndOfMessage
        {
            get;
        }

        public BeginOfDataTokenPattern BeginOfData
        {
            get;
        }

        public EndOfDataTokenPattern EndOfData
        {
            get;
        }
    }
}
