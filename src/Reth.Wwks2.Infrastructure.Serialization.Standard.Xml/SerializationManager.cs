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
using System.Xml.Serialization;

namespace Reth.Wwks2.Infrastructure.Serialization.Standard.Xml
{
    internal class SerializationManager:ISerializationManager
    {
        public SerializationManager()
        {
            this.EnvelopeRoot = XmlSerializationSettings.EnvelopeRoot;
        }

        private object SyncRoot
        {
            get;
        } = new();

        private XmlRootAttribute EnvelopeRoot
        {
            get;
        }

        private Dictionary<Type, XmlSerializer> Serializers
        {
            get;
        } = new();

        public XmlSerializer GetSerializer( Type dataContractType )
        {
            lock( this.SyncRoot )
            {
                if( this.Serializers.TryGetValue( dataContractType, out XmlSerializer? result ) )
                {
                    return result;
                }else
                {
                    result = new XmlSerializer( dataContractType, this.EnvelopeRoot );

                    this.Serializers.Add( dataContractType, result );

                    return result;
                }
            }
        }
    }
}
