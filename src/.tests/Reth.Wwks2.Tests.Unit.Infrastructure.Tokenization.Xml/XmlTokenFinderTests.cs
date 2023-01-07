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

using Moq;

using Reth.Wwks2.Infrastructure.Tokenization;
using Reth.Wwks2.Infrastructure.Tokenization.Xml;

using System.Collections.Generic;
using System.Buffers;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Tokenization.Xml
{
    public class XmlTokenFinderTests:TokenFinderTestBase<XmlTokenState>
    {
        public XmlTokenFinderTests()
        {
            this.TokenPatterns = new XmlTokenPatterns( this.Encoding );
            this.TokenFinder = new XmlTokenFinder( this.Encoding );
        }

        private XmlTokenPatterns TokenPatterns
        {
            get;
        }

        private XmlTokenFinder TokenFinder
        {
            get;
        }

        protected override ITokenTransition<XmlTokenState>? CreateTransition(   IEnumerable<ITokenTransition<XmlTokenState>> transitions,
                                                                                ITokenPatternMatch nextMatch    )
        {
            return this.TokenFinder.CreateTransition( transitions, nextMatch );
        }

        protected override ITokenPatternMatch? FindNextMatch(   XmlTokenState currentState,
                                                                ref SequenceReader<byte> sequenceReader )
        {
            return this.TokenFinder.FindNextMatch( currentState, ref sequenceReader );
        }

        [Fact]
        public void CreateTransition_OutOfMessageToWithinMessage_Succeeds()
        {
            this.CreateTransition_WithGivenContext_Succeeds(    this.TokenPatterns.BeginOfMessage,
                                                                null,
                                                                XmlTokenState.OutOfMessage,
                                                                XmlTokenState.WithinMessage );
        }

        [Fact]
        public void CreateTransition_WithinMessageToOutOfMessage_Succeeds()
        {
            Mock<ITokenTransition<XmlTokenState>> previousTransitionStub = this.CreateTransitionStub(   this.TokenPatterns.BeginOfMessage,
                                                                                                        XmlTokenState.OutOfMessage,
                                                                                                        XmlTokenState.WithinMessage );

            this.CreateTransition_WithGivenContext_Succeeds(    this.TokenPatterns.EndOfMessage,
                                                                previousTransitionStub.Object,
                                                                XmlTokenState.WithinMessage,
                                                                XmlTokenState.OutOfMessage );
        }

        [Fact]
        public void CreateTransition_WithinDataToWithinMessage_Succeeds()
        {
            Mock<ITokenTransition<XmlTokenState>> previousTransitionStub = this.CreateTransitionStub(   this.TokenPatterns.BeginOfData,
                                                                                                        XmlTokenState.WithinMessage,
                                                                                                        XmlTokenState.WithinData  );

            this.CreateTransition_WithGivenContext_Succeeds(    this.TokenPatterns.EndOfData,
                                                                previousTransitionStub.Object,
                                                                XmlTokenState.WithinData,
                                                                XmlTokenState.WithinMessage );
        }

        [Fact]
        public void CreateTransition_WithinMessageToWithinData_Succeeds()
        {
            Mock<ITokenTransition<XmlTokenState>> previousTransitionStub = this.CreateTransitionStub(   this.TokenPatterns.BeginOfMessage,
                                                                                                        XmlTokenState.OutOfMessage,
                                                                                                        XmlTokenState.WithinMessage  );

            this.CreateTransition_WithGivenContext_Succeeds(    this.TokenPatterns.BeginOfData,
                                                                previousTransitionStub.Object,
                                                                XmlTokenState.WithinMessage,
                                                                XmlTokenState.WithinData );
        }

        [Fact]
        public void FindNextMatch_BeginOfMessage_Succeeds()
        {
            this.FindNextMatch_WithGivenContext_Succeeds(   XmlTokenState.OutOfMessage,
                                                            $"   { this.TokenPatterns.BeginOfMessage }",
                                                            this.TokenPatterns.BeginOfMessage );
        }

        [Fact]
        public void FindNextMatch_EndOfMessage_Succeeds()
        {
            this.FindNextMatch_WithGivenContext_Succeeds(   XmlTokenState.WithinMessage,
                                                            $"ABC   { this.TokenPatterns.EndOfMessage }   XYZ",
                                                            this.TokenPatterns.EndOfMessage );
        }

        [Fact]
        public void FindNextMatch_BeginOfData_Succeeds()
        {
            this.FindNextMatch_WithGivenContext_Succeeds(   XmlTokenState.WithinMessage,
                                                            $"   { this.TokenPatterns.BeginOfData }",
                                                            this.TokenPatterns.BeginOfData );
        }

        [Fact]
        public void FindNextMatch_EndOfData_Succeeds()
        {
            this.FindNextMatch_WithGivenContext_Succeeds(   XmlTokenState.WithinData,
                                                            $"ABC   { this.TokenPatterns.EndOfData }   XYZ",
                                                            this.TokenPatterns.EndOfData );
        }
    }
}
