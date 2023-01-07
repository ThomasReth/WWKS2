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
using Reth.Wwks2.Infrastructure.Tokenization.Xml;

using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Tokenization.Xml
{
    public class ExtensionMethodsTests:TestBase
    {
        public ExtensionMethodsTests()
        {
            this.TokenPatterns = new XmlTokenPatterns( this.Encoding );
        }

        private XmlTokenPatterns TokenPatterns
        {
            get;
        }

        [Fact]
        public void TryGetMessage_WithMessageTransitions_ReturnsTrue()
        {
            List<XmlTokenTransition> transitions = new();

            transitions.Add( new XmlTokenTransition(    XmlTokenState.OutOfMessage,
                                                        XmlTokenState.WithinMessage,
                                                        new TokenPatternMatch( this.TokenPatterns.BeginOfMessage, 0 ) ) );

            transitions.Add( new XmlTokenTransition(    XmlTokenState.WithinMessage,
                                                        XmlTokenState.WithinData,
                                                        new TokenPatternMatch( this.TokenPatterns.BeginOfData, transitions.Last().Match.EndIndex ) ) );

            transitions.Add( new XmlTokenTransition(    XmlTokenState.WithinData,
                                                        XmlTokenState.WithinMessage,
                                                        new TokenPatternMatch( this.TokenPatterns.EndOfData, transitions.Last().Match.EndIndex ) ) );

            transitions.Add( new XmlTokenTransition(    XmlTokenState.WithinMessage,
                                                        XmlTokenState.OutOfMessage,
                                                        new TokenPatternMatch( this.TokenPatterns.EndOfMessage, transitions.Last().Match.EndIndex ) ) );

            List<byte> bufferContent = new();

            bufferContent.AddRange( this.TokenPatterns.BeginOfMessage.Value );
            bufferContent.AddRange( this.TokenPatterns.BeginOfData.Value );
            bufferContent.AddRange( this.TokenPatterns.EndOfData.Value );
            bufferContent.AddRange( this.TokenPatterns.EndOfMessage.Value );

            ReadOnlySequence<byte> buffer = new( bufferContent.ToArray() );

            SequenceReader<byte> bufferReader = new( buffer );

            bool actualResult = transitions.TryGetMessage(  ref bufferReader,
                                                            out ReadOnlySequence<byte> token,
                                                            out _,
                                                            out ITokenTransition<XmlTokenState>? _,
                                                            out ITokenTransition<XmlTokenState>? _   );

            actualResult.Should().BeTrue();

            SequenceReader<byte> tokenReader = new SequenceReader<byte>( token );

            tokenReader.IsNext( this.TokenPatterns.BeginOfMessage.Value.AsSpan(), advancePast:true ).Should().BeTrue();
            tokenReader.IsNext( this.TokenPatterns.BeginOfData.Value.AsSpan(), advancePast:true ).Should().BeTrue();
            tokenReader.IsNext( this.TokenPatterns.EndOfData.Value.AsSpan(), advancePast:true ).Should().BeTrue();
            tokenReader.IsNext( this.TokenPatterns.EndOfMessage.Value.AsSpan(), advancePast:true ).Should().BeTrue();
        }

        [Fact]
        public void TryGetMessage_WithMessage_ReturnsTrue()
        {
            string messageContent = "abcd";

            List<XmlTokenTransition> transitions = new();

            transitions.Add( new XmlTokenTransition(    XmlTokenState.OutOfMessage,
                                                        XmlTokenState.WithinMessage,
                                                        new TokenPatternMatch( this.TokenPatterns.BeginOfMessage, 0 ) ) );

            transitions.Add( new XmlTokenTransition(    XmlTokenState.WithinMessage,
                                                        XmlTokenState.OutOfMessage,
                                                        new TokenPatternMatch( this.TokenPatterns.EndOfMessage, transitions.Last().Match.EndIndex + messageContent.Length ) ) );

            List<byte> bufferContent = new();

            bufferContent.AddRange( this.TokenPatterns.BeginOfMessage.Value );
            bufferContent.AddRange( this.Encoding.GetBytes( messageContent ) );
            bufferContent.AddRange( this.TokenPatterns.EndOfMessage.Value );

            ReadOnlySequence<byte> buffer = new( bufferContent.ToArray() );

            string expectedMessage = this.Encoding.GetString( buffer );

            SequenceReader<byte> sequenceReader = new( buffer );

            bool actualResult = transitions.TryGetMessage(  ref sequenceReader,
                                                            out ReadOnlySequence<byte> token,
                                                            out _,
                                                            out ITokenTransition<XmlTokenState>? _,
                                                            out ITokenTransition<XmlTokenState>? _   );

            actualResult.Should().BeTrue();

            string? actualMessage = token.ToString( this.Encoding );

            actualMessage.Should().Be( expectedMessage );
        }
    }
}
