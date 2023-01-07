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

using Reth.Wwks2.Protocol.Messages;

using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Wwks2.Infrastructure.Messaging
{
    public interface IMessageEndpoint:IConnectableObservable<IMessageEnvelope>, IDisposable, IMessageReceiver, IMessageSender
    {
        IDisposable Subscribe<TMessage>( IObserver<TMessage> observer )where TMessage:class, IMessage;

        TResponse SendRequest<TRequest, TResponse>( TRequest request )
            where TRequest:IMessage
            where TResponse:IMessage;

        Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request, CancellationToken cancellationToken = default )
            where TRequest:IMessage
            where TResponse:IMessage;

        IMessageEnvelope SendRequest<TResponse>( IMessageEnvelope requestEnvelope )
            where TResponse:IMessage;

        Task<IMessageEnvelope> SendRequestAsync<TResponse>( IMessageEnvelope requestEnvelope, CancellationToken cancellationToken = default )
            where TResponse:IMessage;

        void SendMessage<TMessage>( TMessage message )
            where TMessage:IMessage;

        Task SendMessageAsync<TMessage>( TMessage message, CancellationToken cancellationToken = default )
            where TMessage:IMessage;
    }
}
