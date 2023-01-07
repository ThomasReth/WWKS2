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
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Reth.Wwks2.Infrastructure.Messaging.Bridging
{
    public class MessageChannelBridge
    {
        public MessageChannelBridge( IMessageChannel route, IMessageChannel target )
        {
            this.Route = route;
            this.Target = target;

            this.RouteSource = route.Publish();
            this.TargetSource = target.Publish();

            this.RouteSource.Subscribe( async( IMessageEnvelope messageEnvelope ) =>
                                        {
                                            await this.Target.SendMessageAsync( messageEnvelope );
                                        }   );

            this.TargetSource.Subscribe(    async( IMessageEnvelope messageEnvelope ) =>
                                            {
                                                await this.Route.SendMessageAsync( messageEnvelope );
                                            }   );

            this.RouteSource.Connect();
            this.TargetSource.Connect();
        }

        private IMessageChannel Route
        {
            get;
        }

        private IMessageChannel Target
        {
            get;
        }

        private IConnectableObservable<IMessageEnvelope> RouteSource
        {
            get;
        }

        private IConnectableObservable<IMessageEnvelope> TargetSource
        {
            get;
        }
    }
}
