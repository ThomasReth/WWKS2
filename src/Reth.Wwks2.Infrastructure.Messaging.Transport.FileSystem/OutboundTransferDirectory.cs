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
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Wwks2.Infrastructure.Messaging.Transport.FileSystem
{
    public class OutboundTransferDirectory:TransferDirectory, IOutboundTransferDirectory
    {
        public OutboundTransferDirectory( TransferDirectoryInfo info )
        :
            this(   info,
                    ( string path ) =>
                    {
                        return new FileStream( path, FileMode.CreateNew, FileAccess.Write, FileShare.None );
                    },
                    ( string sourceFileName, string destinationFileName ) =>
                    {
                        File.Move( sourceFileName, destinationFileName );
                    },
                    ( string fileName ) =>
                    {
                        File.Delete( fileName );
                    }   )
        {
        }

        public OutboundTransferDirectory(   TransferDirectoryInfo info,
                                            Func<string, Stream> createStream,
                                            Action<string, string> moveFile,
                                            Action<string> deleteFile   )
        :
            base( info )
        {
            this.CreateStream = createStream;
            this.MoveFile = moveFile;
            this.DeleteFile = deleteFile;
        }

        private Func<string, Stream> CreateStream
        {
            get;
        }

        private Action<string, string> MoveFile
        {
            get;
        }

        private Action<string> DeleteFile
        {
            get;
        }

        private void Cleanup( string intermediateFilePath )
        {
            try
            {
                this.DeleteFile( intermediateFilePath );
            }catch
            {
                // We're not interested in cleanup errors.
            }
        }

        public void WriteFile( TransferFile file )
        {
            string filePath = Path.Combine( this.Info.FullName, file.Name.ToString() );

            string intermediateFilePath = $"{ filePath }.tmp";
            string targetFilePath = $"{ filePath }.{ this.Info.FileExtension }";

            try
            {
                using( Stream stream = this.CreateStream( intermediateFilePath ) )
                {
                    stream.Write( this.Info.FileEncoding.GetBytes( file.Message ) );
                    stream.Flush();
                }

                this.MoveFile( intermediateFilePath, targetFilePath );
            }catch
            {
                this.Cleanup( intermediateFilePath );    

                throw;
            }
        }

        public Task WriteFileAsync( TransferFile file, CancellationToken cancellationToken = default )
        {
            return Task.Run(    () =>
                                {
                                    this.WriteFile( file );
                                },
                                cancellationToken );
        }
    }
}
