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

using Reth.Wwks2.Protocol.Standard.Messages.Hello;

namespace Reth.Wwks2.Protocol.Standard.Messages
{
    public class Capabilities
    {
        public static readonly Capability ArticleMasterSet = new( "ArticleMaster" );
        public static readonly Capability ConfigurationGet = new( "Configuration" );
        public static readonly Capability Hello = new( "Hello" );
        public static readonly Capability InitiateInput = new( "InitiateInput" );
        public static readonly Capability Input = new( "Input" );
        public static readonly Capability KeepAlive = new( "KeepAlive" );
        public static readonly Capability Output = new( "Output" );
        public static readonly Capability Status = new( "Status" );
        public static readonly Capability StockDeliverySet = new( "StockDelivery" );
        public static readonly Capability StockInfo = new( "StockInfo" );
        public static readonly Capability StockLocationInfo = new( "StockLocationInfo" );
        public static readonly Capability TaskCancel = new( "TaskCancel" );
        public static readonly Capability TaskInfo = new( "TaskInfo" );

        protected Capabilities()
        {
        }
    }
}
