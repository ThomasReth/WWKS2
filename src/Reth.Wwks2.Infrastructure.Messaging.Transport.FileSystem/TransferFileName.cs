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
using System.Globalization;
using System.Text.RegularExpressions;

namespace Reth.Wwks2.Infrastructure.Messaging.Transport.FileSystem
{
    public class TransferFileName
    {
        private const string MessageGroup = "message";
        private const string TimestampGroup = "timestamp";

        private const string TimestampFormat = "yyyy-MM-dd_HH-mm-ss-fff";

        private static readonly Regex ParseExpression = new( @$"^(?'{ TransferFileName.TimestampGroup }'[0-9]{{4}}-[0-9]{{2}}-[0-9]{{2}}_[0-9]{{2}}-[0-9]{{2}}-[0-9]{{2}}-[0-9]{{3}})_(?'{ TransferFileName.MessageGroup }'[a-zA-Z]+)$", RegexOptions.IgnoreCase );

        public static TransferFileName Parse( string? value )
        {
            if( value is null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }       

            Match match = TransferFileName.ParseExpression.Match( value );

            if( match.Success == false )
            {
                throw new ArgumentOutOfRangeException( nameof( value ), value, $"Invalid transfer file name: '{ value }'" );
            }

            string messageName = match.Groups[ TransferFileName.MessageGroup ].Value;
            string timestamp = match.Groups[ TransferFileName.TimestampGroup ].Value;

            return new( messageName, DateTimeOffset.ParseExact( timestamp, TransferFileName.TimestampFormat, CultureInfo.InvariantCulture ) );
        }

        public TransferFileName( string messageName, DateTimeOffset timestamp )
        {
            this.MessageName = messageName;
            this.Timestamp = timestamp;
        }

        public string MessageName
        {
            get;
        }

        public DateTimeOffset Timestamp
        {
            get;
        }

        public override string ToString()
        {
            
            return $"{ this.Timestamp.ToString( TransferFileName.TimestampFormat ) }_{ this.MessageName }";
        }
    }
}
