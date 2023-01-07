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
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;

namespace Reth.Wwks2.Infrastructure.Messaging.Transport.FileSystem
{
    public class InboundTransferDirectory:TransferDirectory, IInboundTransferDirectory
    {
        private bool isDisposed;

        public InboundTransferDirectory( TransferDirectoryInfo info )
        :
            this(   info,
                    ( string path ) =>
                    {
                        return new FileStream( path, FileMode.Open, FileAccess.Read, FileShare.Read );
                    },
                    ( string path, string filter ) =>
                    {
                        return new FileSystemWatcherProxy( path, filter );
                    },
                    ( string fileName ) =>
                    {
                        File.Delete( fileName );
                    }   )
        {
        }

        public InboundTransferDirectory(    TransferDirectoryInfo info,
                                            Func<string, Stream> createStream,
                                            Func<string, string, IFileSystemWatcher> createFileSystemWatcher,
                                            Action<string> deleteFile   )
        :
            base( info )
        {
            this.CreateStream = createStream;
            this.DeleteFile = deleteFile;

            this.FileSystemWatcher = createFileSystemWatcher( info.FullName, $"*.{ info.FileExtension }" );

            this.Source = Observable
                            .FromEventPattern<FileSystemEventArgs>( this.FileSystemWatcher, nameof( this.FileSystemWatcher.Renamed ) )
                            .Select(    ( EventPattern<FileSystemEventArgs> eventPattern ) =>
                                        {
                                            string fullPath = eventPattern.EventArgs.FullPath;
                                            string fileName = Path.GetFileNameWithoutExtension( fullPath );

                                            string message = this.Read( fullPath );

                                            TransferFileName transferFileName = TransferFileName.Parse( fileName );

                                            return new TransferFile( transferFileName, message );
                                        }   );
            
            this.FileSystemWatcher.IncludeSubdirectories = false;
            this.FileSystemWatcher.EnableRaisingEvents = true;
        }

        private Func<string, Stream> CreateStream
        {
            get;
        }

        private Action<string> DeleteFile
        {
            get;
        }

        private IFileSystemWatcher FileSystemWatcher
        {
            get;
        }

        private IObservable<TransferFile> Source
        {
            get;
        }

        private void Cleanup( string fullPath )
        {
            try
            {
                this.DeleteFile( fullPath );
            }catch
            {
                // We're not interested in cleanup errors.
            }
        }

        private string Read( string fullPath )
        {
            string result = string.Empty;

            int retryCount = 3;

            Exception? exception = null;

            do
            {
                exception = null;

                try
                {
                    using( Stream stream = this.CreateStream( fullPath ) )
                    {
                        using( StreamReader streamReader = new( stream, this.Info.FileEncoding ) )
                        {
                            result = streamReader.ReadToEnd();
                        }
                    }

                    retryCount = 0;
                }catch( Exception ex )
                {
                    // TODO: Change to async wait.
                    Thread.Sleep( Random.Shared.Next( 10, 100 ) );

                    exception = ex;

                    --retryCount;
                }
            }while( retryCount > 0 );

            if( exception is not null )
            {
                throw exception;
            }

            this.Cleanup( fullPath );
            
            return result;
        }
        
        public IDisposable Subscribe( IObserver<TransferFile> observer )
        {
            return this.Source.Subscribe( observer );
        }

        protected override void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing )
                {
                    this.FileSystemWatcher.Dispose();
                }

                this.isDisposed = true;

                base.Dispose( disposing );
            }
        }
    }
}
