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

namespace Reth.Wwks2.Protocol.Standard.Messages.ConfigurationGet
{
    public class ConfigurationGetResponse:SubscriberMessage, IEquatable<ConfigurationGetResponse>
    {
        public static bool operator==( ConfigurationGetResponse? left, ConfigurationGetResponse? right )
		{
            return ConfigurationGetResponse.Equals( left, right );
		}
		
		public static bool operator!=( ConfigurationGetResponse? left, ConfigurationGetResponse? right )
		{
			return !( ConfigurationGetResponse.Equals( left, right ) );
		}

        public static bool Equals( ConfigurationGetResponse? left, ConfigurationGetResponse? right )
		{
            bool result = SubscriberMessage.Equals( left, right );

            result &= ( result ? string.Equals( left?.Configuration, right?.Configuration, StringComparison.OrdinalIgnoreCase ) : false );

            return result;
		}

        public ConfigurationGetResponse(    SubscriberId source,
                                            SubscriberId destination,
                                            MessageId id,
                                            string configuration )
        :
            base( source, destination, id )
        {
            this.Configuration = configuration;
        }

        public ConfigurationGetResponse(    ConfigurationGetRequest request,
                                            string configuration )
        :
            base( request )
        {
            this.Configuration = configuration;
        }

        public string Configuration
        {
            get;
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as ConfigurationGetResponse );
		}
		
        public bool Equals( ConfigurationGetResponse? other )
		{
            return ConfigurationGetResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Configuration.GetHashCode();
		}
    }
}
