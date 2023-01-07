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

using Reth.Wwks2.Infrastructure.Serialization;
using Reth.Wwks2.Protocol.Messages;

using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Wwks2.Infrastructure.Messaging.Transport.FileSystem
{
    public class FileSystemMessageChannel:MessageChannelBase
    {
        public FileSystemMessageChannel(    IMessageSerializer messageSerializer,
                                            IInboundTransferDirectory inboundDirectory,
                                            IOutboundTransferDirectory outboundDirectory    )
        :
            this(   messageSerializer,
                    inboundDirectory,
                    outboundDirectory,
                    inboundDirectory.Select(    ( TransferFile transferFile ) =>
                                                {
                                                    return messageSerializer.Deserialize( transferFile.Message );
                                                }   )   )
        {
        }

        public FileSystemMessageChannel(    IMessageSerializer messageSerializer,
                                            IInboundTransferDirectory inboundDirectory,
                                            IOutboundTransferDirectory outboundDirectory,
                                            IObservable<IMessageEnvelope> source    )
        :
            base( messageSerializer )
        {
            this.InboundDirectory = inboundDirectory;
            this.OutboundDirectory = outboundDirectory;

            this.Source = source;
        }

        private IInboundTransferDirectory InboundDirectory
        {
            get;
        }

        private IOutboundTransferDirectory OutboundDirectory
        {
            get;
        }

        public TransferDirectoryInfo InboundInfo
        {
            get{ return this.InboundDirectory.Info; }
        }

        public TransferDirectoryInfo OutboundInfo
        {
            get{ return this.OutboundDirectory.Info; }
        }

        private IObservable<IMessageEnvelope> Source
        {
            get;
        }

        public override IDisposable Subscribe( IObserver<IMessageEnvelope> observer )
        {
            return this.Source.Subscribe( observer );
        }
        
        public override void SendMessage( IMessageEnvelope messageEnvelope )
        {
            string serializedMessage = this.MessageSerializer.Serialize( messageEnvelope );
            
            TransferFileName fileName = new(    messageEnvelope.Message.Name,
                                                messageEnvelope.Timestamp.Value );

            this.OutboundDirectory.WriteFile( new TransferFile( fileName, serializedMessage ) );
        }

        public override async Task SendMessageAsync( IMessageEnvelope messageEnvelope, CancellationToken cancellationToken = default )
        {
            string serializedMessage = await this.MessageSerializer.SerializeAsync( messageEnvelope, cancellationToken ).ConfigureAwait( continueOnCapturedContext:false );
            
            TransferFileName fileName = new(    messageEnvelope.Message.Name,
                                                messageEnvelope.Timestamp.Value );

            await this.OutboundDirectory.WriteFileAsync( new TransferFile( fileName, serializedMessage ), cancellationToken ).ConfigureAwait( continueOnCapturedContext:false );
        }

        private TransferFileName GetTransferFileName( string messageEnvelope)
        {
            string messageName = this.MessageSerializer.MessageParser.GetName( messageEnvelope );

            DateTimeOffset timestamp = this.MessageSerializer.MessageParser.GetTimestamp( messageEnvelope );
            
            return new( messageName, timestamp );
        }

        public override void SendMessage( string messageEnvelope )
        {
            TransferFileName fileName = this.GetTransferFileName( messageEnvelope );

            this.OutboundDirectory.WriteFile( new TransferFile( fileName, messageEnvelope ) );
        }

        public override async Task SendMessageAsync( string messageEnvelope, CancellationToken cancellationToken = default )
        {
            TransferFileName fileName = this.GetTransferFileName( messageEnvelope );

            await this.OutboundDirectory.WriteFileAsync( new TransferFile( fileName, messageEnvelope ), cancellationToken ).ConfigureAwait( continueOnCapturedContext:false );
        }
    }
}
