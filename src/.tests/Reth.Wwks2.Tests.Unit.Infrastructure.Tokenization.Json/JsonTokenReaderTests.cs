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

using Reth.Wwks2.Infrastructure.Tokenization;
using Reth.Wwks2.Infrastructure.Tokenization.Json;
using Reth.Wwks2.Tests.Unit.TestData.Json;

using System.Collections.Generic;
using System.IO;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Tokenization.Json
{
    public class XmlTokenReaderTests:TokenReaderTestBase<JsonTokenState>
    {
        protected override IEqualityComparer<string?> Comparer
        {
            get;
        } = JsonComparer.Default;

        protected override ITokenReader CreateTokenReader( Stream stream )
        {
            return new JsonTokenReader( stream, this.Encoding );
        }

        [Fact]
        public void Wait_WithCompleteMessage_ReturnsHelloRequest()
        {
            base.Wait_WithCompleteMessage_ReturnsMessage( JsonTestData.HelloRequest.Json );
        }

        [Fact]
        public void Subscribe_WithMultipleSubscriptions_DispatchesHelloRequestToAllSubscriptions()
        {
            base.Subscribe_WithMultipleSubscriptions_DispatchesMessageToAllSubscriptions( JsonTestData.HelloRequest.Json );
        }
    }
}
