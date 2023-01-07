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

using FluentAssertions;

using Reth.Wwks2.Infrastructure.Tokenization;
using Reth.Wwks2.Infrastructure.Tokenization.Json;

using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Tokenization.Json
{
    public class ExtensionMethodsTests:TestBase
    {
        public ExtensionMethodsTests()
        {
            this.TokenPatterns = new JsonTokenPatterns( this.Encoding );
        }

        private JsonTokenPatterns TokenPatterns
        {
            get;
        }

        [Fact]
        public void TryGetMessage_WithMessageTransitions_ReturnsTrue()
        {
            List<JsonTokenTransition> transitions = new();

            transitions.Add( new JsonTokenTransition(   JsonTokenState.OutOfMessage,
                                                        JsonTokenState.WithinObject,
                                                        new TokenPatternMatch( this.TokenPatterns.BeginOfObject, 0 ) ) );

            transitions.Add( new JsonTokenTransition(   JsonTokenState.WithinObject,
                                                        JsonTokenState.WithinString,
                                                        new TokenPatternMatch( this.TokenPatterns.BeginOfString, transitions.Last().Match.EndIndex ) ) );

            transitions.Add( new JsonTokenTransition(   JsonTokenState.WithinString,
                                                        JsonTokenState.WithinObject,
                                                        new TokenPatternMatch( this.TokenPatterns.EndOfString, transitions.Last().Match.EndIndex ) ) );

            transitions.Add( new JsonTokenTransition(   JsonTokenState.WithinObject,
                                                        JsonTokenState.OutOfMessage,
                                                        new TokenPatternMatch( this.TokenPatterns.EndOfObject, transitions.Last().Match.EndIndex ) ) );

            List<byte> bufferContent = new();

            bufferContent.AddRange( this.TokenPatterns.BeginOfObject.Value );
            bufferContent.AddRange( this.TokenPatterns.BeginOfString.Value );
            bufferContent.AddRange( this.TokenPatterns.EndOfString.Value );
            bufferContent.AddRange( this.TokenPatterns.EndOfObject.Value );

            ReadOnlySequence<byte> buffer = new( bufferContent.ToArray() );

            SequenceReader<byte> bufferReader = new( buffer );

            bool actualResult = transitions.TryGetMessage(  ref bufferReader,
                                                            out ReadOnlySequence<byte> token,
                                                            out _,
                                                            out ITokenTransition<JsonTokenState>? _,
                                                            out ITokenTransition<JsonTokenState>? _   );

            actualResult.Should().BeTrue();

            SequenceReader<byte> tokenReader = new SequenceReader<byte>( token );

            tokenReader.IsNext( this.TokenPatterns.BeginOfObject.Value.AsSpan(), advancePast:true ).Should().BeTrue();
            tokenReader.IsNext( this.TokenPatterns.BeginOfString.Value.AsSpan(), advancePast:true ).Should().BeTrue();
            tokenReader.IsNext( this.TokenPatterns.EndOfString.Value.AsSpan(), advancePast:true ).Should().BeTrue();
            tokenReader.IsNext( this.TokenPatterns.EndOfObject.Value.AsSpan(), advancePast:true ).Should().BeTrue();
        }

        [Fact]
        public void TryGetMessage_WithMessage_ReturnsTrue()
        {
            string messageContent = "abcd";

            List<JsonTokenTransition> transitions = new();

            transitions.Add( new JsonTokenTransition(   JsonTokenState.OutOfMessage,
                                                        JsonTokenState.WithinObject,
                                                        new TokenPatternMatch( this.TokenPatterns.BeginOfObject, 0 ) ) );

            transitions.Add( new JsonTokenTransition(   JsonTokenState.WithinObject,
                                                        JsonTokenState.OutOfMessage,
                                                        new TokenPatternMatch( this.TokenPatterns.EndOfObject, transitions.Last().Match.EndIndex + messageContent.Length ) ) );

            List<byte> bufferContent = new();

            bufferContent.AddRange( this.TokenPatterns.BeginOfObject.Value );
            bufferContent.AddRange( this.Encoding.GetBytes( messageContent ) );
            bufferContent.AddRange( this.TokenPatterns.EndOfObject.Value );

            ReadOnlySequence<byte> buffer = new( bufferContent.ToArray() );

            string expectedMessage = this.Encoding.GetString( buffer );

            SequenceReader<byte> sequenceReader = new( buffer );

            bool actualResult = transitions.TryGetMessage(  ref sequenceReader,
                                                            out ReadOnlySequence<byte> token,
                                                            out _,
                                                            out ITokenTransition<JsonTokenState>? _,
                                                            out ITokenTransition<JsonTokenState>? _   );

            actualResult.Should().BeTrue();

            string? actualMessage = token.ToString( this.Encoding );

            actualMessage.Should().Be( expectedMessage );
        }
    }
}
