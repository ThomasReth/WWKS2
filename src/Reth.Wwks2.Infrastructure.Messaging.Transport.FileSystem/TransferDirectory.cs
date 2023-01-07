﻿// Implementation of the WWKS2 protocol.
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

namespace Reth.Wwks2.Infrastructure.Messaging.Transport.FileSystem
{
    public class TransferDirectory:ITransferDirectory
    {
        public TransferDirectory( TransferDirectoryInfo info )
        {
            this.Info = info;
        }

        public TransferDirectoryInfo Info
        {
            get;
        }

        public override string ToString()
        {
            return this.Info.ToString();
        }

        public void Dispose()
        {
            this.Dispose( disposing:true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
    }
}
