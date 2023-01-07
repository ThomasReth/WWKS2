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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reth.Wwks2.Protocol.Standard.Messages.Hello
{
    public class Subscriber:IEquatable<Subscriber>
    {
        public static bool operator==( Subscriber? left, Subscriber? right )
		{
            return Subscriber.Equals( left, right );
		}
		
		public static bool operator!=( Subscriber? left, Subscriber? right )
		{
			return !( Subscriber.Equals( left, right ) );
		}

        public static bool Equals( Subscriber? left, Subscriber? right )
		{
            bool result = SubscriberId.Equals( left?.Id, right?.Id );

            result &= ( result ? EqualityComparer<SubscriberType?>.Default.Equals( left?.Type, right?.Type ) : false );
            result &= ( result ? string.Equals( left?.Manufacturer, right?.Manufacturer, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.ProductInfo, right?.ProductInfo, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.VersionInfo, right?.VersionInfo, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? string.Equals( left?.TenantId, right?.TenantId, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? ( left?.Capabilities.SequenceEqual( right?.Capabilities ) ).GetValueOrDefault() : false );

            return result;
		}

        public Subscriber(  IEnumerable<Capability>? capabilities,
                            SubscriberId id,
                            SubscriberType type,
                            string? tenantId,
                            string manufacturer,
                            string productInfo  )
        :
            this( capabilities,
                  id,
                  type,
                  tenantId,
                  manufacturer,
                  productInfo,
                  Assembly.GetCallingAssembly().GetName().Version?.ToString() ?? string.Empty   )
        {
        }

        public Subscriber(  IEnumerable<Capability>? capabilities,
                            SubscriberId id,
                            SubscriberType type,
                            string? tenantId,
                            string manufacturer,
                            string productInfo,
                            string versionInfo  )
        {
            manufacturer.ThrowIfEmpty( "Subscriber manufacturer must not be empty." );
            productInfo.ThrowIfEmpty( "Subscriber product info must not be empty." );
            versionInfo.ThrowIfEmpty( "Subscriber version info must not be empty." );

            this.Id = id;
            this.Type = type;
            this.Manufacturer = manufacturer;
            this.TenantId = tenantId;
            this.ProductInfo = productInfo;
            this.VersionInfo = versionInfo;

            if( capabilities is not null )
            {
                foreach( Capability capability in capabilities )
                {
                    this.CapabilityMap.Add( capability.Name, capability );
                }
            }
        }

        public SubscriberId Id
        {
            get;
        }

        public SubscriberType Type
        {
            get;
        } = SubscriberType.IMS;

        public string? TenantId
        {
            get;
        }

        public string Manufacturer
        {
            get;
        }

        public string ProductInfo
        {
            get;
        }

        public string VersionInfo
        {
            get;
        }
        
        private Dictionary<string, Capability> CapabilityMap
        {
            get;
        } = new Dictionary<string, Capability>( StringComparer.OrdinalIgnoreCase );

        public IReadOnlyList<Capability> Capabilities
        {
            get
            {
                return this.CapabilityMap.Values.ToArray();
            }
        }

        public bool IsSupported( Capability capability )
        {
            return this.CapabilityMap.ContainsKey( capability.Name );
        }

        public bool IsSupported( string capability )
        {
           return this.CapabilityMap.ContainsKey( capability );
        }

        public override bool Equals( object? obj )
		{
			return this.Equals( obj as Subscriber );
		}
		
        public bool Equals( Subscriber? other )
		{
            return Subscriber.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
