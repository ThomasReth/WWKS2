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
using System.Text.RegularExpressions;

namespace Reth.Wwks2.Infrastructure.Serialization.Standard.Xml
{
    public class XmlMessageParser:IMessageParser
    {
        public XmlMessageParser()
        {
        }

        public string GetName( string messageEnvelope )
        {
            string result = string.Empty;

            if( string.IsNullOrEmpty( messageEnvelope ) == false )
            {
                const string groupName = "MessageName";

                Regex regex = new( @$"^.*\<\s*(?'{ groupName }'[a-zA-Z]+(Request|Response|Message))\s+", RegexOptions.Multiline );

                Match match = regex.Match( messageEnvelope );

                if( match.Success == true )
                {
                    Group messageName = match.Groups[ groupName ];

                    result = messageName.Value;
                }else
                {
                    throw new FormatException( $"Extraction of message name failed." );    
                }
            }

            return result;
        }

        public DateTimeOffset GetTimestamp( string messageEnvelope )
        {
            DateTimeOffset result = DateTimeOffset.UtcNow;

            if( string.IsNullOrEmpty( messageEnvelope ) == false )
            {
                const string groupName = "Timestamp";

                Regex regex = new( @$"^\s*\<(WWKS).+TimeStamp=\""(?'{ groupName }'\d\d\d\d-\d\d-\d\dT\d\d\:\d\d\:\d\dZ)\""", RegexOptions.Multiline | RegexOptions.IgnoreCase );

                Match match = regex.Match( messageEnvelope );

                if( match.Success == true )
                {
                    Group group = match.Groups[ groupName ];

                    result = DateTimeOffset.Parse( group.Value );
                }else
                {
                    throw new FormatException( $"Extraction of message timestamp failed." );
                }
            }

            return result;
        }
    }
}
