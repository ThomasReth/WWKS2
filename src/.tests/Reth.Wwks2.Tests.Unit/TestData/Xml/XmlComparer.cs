// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

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

using Microsoft.XmlDiffPatch;

using System.Collections.Generic;
using System.Xml;

namespace Reth.Wwks2.Tests.Unit.TestData.Xml
{
    public class XmlComparer:IEqualityComparer<string?>
    {
        public static XmlComparer Default
        {
            get;
        } = new();

        public bool Equals( string? expected, string? actual )
        {
            XmlDiff xmlDiff = new( XmlDiffOptions.IgnoreChildOrder );

            XmlDocument expectedDocument = new();
            XmlDocument actualDocument = new();

            expectedDocument.LoadXml( expected ?? string.Empty );
            actualDocument.LoadXml( actual ?? string.Empty );
            
            return xmlDiff.Compare( expectedDocument, actualDocument );
        }

        public int GetHashCode( string? value )
        {
            return value?.GetHashCode() ?? 0;
        }
    }
}
