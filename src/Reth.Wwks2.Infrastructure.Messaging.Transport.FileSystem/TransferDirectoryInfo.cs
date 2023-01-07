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

namespace Reth.Wwks2.Infrastructure.Messaging.Transport.FileSystem
{
    public class TransferDirectoryInfo
    {
        public TransferDirectoryInfo( string fullName, string fileExtension, Encoding fileEncoding )
        {
            this.FullName = fullName;
            this.FileExtension = fileExtension;
            this.FileEncoding = fileEncoding;
        }

        public string FullName
        {
            get;
        }

        public string FileExtension
        {
            get;
        }

        public Encoding FileEncoding
        {
            get;
        }

        public override string ToString()
        {
            return this.FullName;
        }
    }
}
