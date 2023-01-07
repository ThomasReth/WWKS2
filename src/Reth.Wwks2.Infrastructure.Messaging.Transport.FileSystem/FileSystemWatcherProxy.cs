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
using System.IO;

namespace Reth.Wwks2.Infrastructure.Messaging.Transport.FileSystem
{
    internal class FileSystemWatcherProxy:IFileSystemWatcher
    {
        public event RenamedEventHandler? Renamed;

        private bool isDisposed;

        public FileSystemWatcherProxy( string path, string filter )
        {
            this.Subject = new FileSystemWatcher( path, filter );
            this.Subject.Renamed += this.Subject_Renamed;
        }

        private void Subject_Renamed( object sender, RenamedEventArgs e )
        {
            this.Renamed?.Invoke( sender, e );
        }

        private FileSystemWatcher Subject
        {
            get;
        }

        public bool EnableRaisingEvents
        {
            get{ return this.Subject.EnableRaisingEvents; }
            set{ this.Subject.EnableRaisingEvents = value; }
        }

        public bool IncludeSubdirectories
        {
            get{ return this.Subject.IncludeSubdirectories; }
            set{ this.Subject.IncludeSubdirectories = value; }
        }

        public void Dispose()
        {
            this.Dispose( disposing:true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing )
                {
                    this.Subject.Renamed -= this.Subject_Renamed;
                    this.Subject.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
