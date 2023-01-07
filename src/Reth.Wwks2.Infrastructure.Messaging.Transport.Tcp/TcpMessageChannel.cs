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
using Reth.Wwks2.Infrastructure.Tokenization;
using Reth.Wwks2.Protocol.Messages;

using System;
using System.IO;
using System.Net.Sockets;

namespace Reth.Wwks2.Infrastructure.Messaging.Transport.Tcp
{
    public class TcpMessageChannel:StreamBased.MessageChannel
    {
        private bool isDisposed;

        public TcpMessageChannel(   IMessageSerializer messageSerializer,
                                    ITokenReader tokenReader,
                                    Stream stream,
                                    TcpClient tcpClient )
        :
            base(   messageSerializer,
                    tokenReader,
                    stream   )
        {
            this.LocalEndpoint = tcpClient.Client.LocalEndPoint!.ToString()!;
            this.RemoteEndpoint = tcpClient.Client.RemoteEndPoint!.ToString()!;

            this.TcpClient = tcpClient;
        }

        public TcpMessageChannel(   IMessageSerializer messageSerializer,
                                    ITokenReader tokenReader,
                                    Stream stream,
                                    TcpClient tcpClient,
                                    IObservable<IMessageEnvelope> source    )
        :
            base(   messageSerializer,
                    tokenReader,
                    stream,
                    source  )
        {
            this.LocalEndpoint = tcpClient.Client.LocalEndPoint!.ToString()!;
            this.RemoteEndpoint = tcpClient.Client.RemoteEndPoint!.ToString()!;

            this.TcpClient = tcpClient;
        }

        private TcpClient TcpClient
        {
            get;
        }

        public string LocalEndpoint
        {
            get;
        }
        
        public string RemoteEndpoint
        {
            get;
        }

        public override string ToString()
        {
            return $"{ this.LocalEndpoint } -> { this.RemoteEndpoint }";
        }

        protected override void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.TcpClient.Dispose();
                }

                this.isDisposed = true;
            }

            base.Dispose( disposing );
        }
    }
}
