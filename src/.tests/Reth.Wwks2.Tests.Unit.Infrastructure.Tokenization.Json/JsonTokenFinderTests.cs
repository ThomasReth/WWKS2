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
using Reth.Wwks2.Infrastructure.Tokenization.Json;

using System.Buffers;
using System.Collections.Generic;

using Xunit;

namespace Reth.Wwks2.Tests.Unit.Infrastructure.Tokenization.Json
{
    public class JsonTokenFinderTests:TokenFinderTestBase<JsonTokenState>
    {
        public JsonTokenFinderTests()
        {
            this.TokenPatterns = new JsonTokenPatterns( this.Encoding );
            this.TokenFinder = new JsonTokenFinder( this.Encoding );
        }

        private JsonTokenPatterns TokenPatterns
        {
            get;
        }

        private JsonTokenFinder TokenFinder
        {
            get;
        }

        protected override ITokenTransition<JsonTokenState>? CreateTransition(  IEnumerable<ITokenTransition<JsonTokenState>> transitions,
                                                                                ITokenPatternMatch nextMatch    )
        {
            return this.TokenFinder.CreateTransition( transitions, nextMatch );
        }

        protected override ITokenPatternMatch? FindNextMatch(   JsonTokenState currentState,
                                                                ref SequenceReader<byte> sequenceReader )
        {
            return this.TokenFinder.FindNextMatch( currentState, ref sequenceReader );
        }

        [Fact]
        public void CreateTransition_OutOfMessageToWithinObject_Succeeds()
        {
            this.CreateTransition_WithGivenContext_Succeeds(    this.TokenPatterns.BeginOfObject,
                                                                null,
                                                                JsonTokenState.OutOfMessage,
                                                                JsonTokenState.WithinObject );
        }

        [Fact]
        public void CreateTransition_WithinObjectToOutOfMessage_Succeeds()
        {
            Mock<ITokenTransition<JsonTokenState>> previousTransitionStub = this.CreateTransitionStub(  this.TokenPatterns.BeginOfObject,
                                                                                                        JsonTokenState.OutOfMessage,
                                                                                                        JsonTokenState.WithinObject );

            this.CreateTransition_WithGivenContext_Succeeds(    this.TokenPatterns.EndOfObject,
                                                                previousTransitionStub.Object,
                                                                JsonTokenState.WithinObject,
                                                                JsonTokenState.OutOfMessage );
        }

        [Fact]
        public void CreateTransition_WithinStringToWithinObject_Succeeds()
        {
            Mock<ITokenTransition<JsonTokenState>> previousTransitionStub = this.CreateTransitionStub(  this.TokenPatterns.BeginOfString,
                                                                                                        JsonTokenState.WithinObject,
                                                                                                        JsonTokenState.WithinString );

            this.CreateTransition_WithGivenContext_Succeeds(    this.TokenPatterns.EndOfString,
                                                                previousTransitionStub.Object,
                                                                JsonTokenState.WithinString,
                                                                JsonTokenState.WithinObject );
        }

        [Fact]
        public void CreateTransition_WithinObjectToWithinString_Succeeds()
        {
            Mock<ITokenTransition<JsonTokenState>> previousTransitionStub = this.CreateTransitionStub(  this.TokenPatterns.EndOfObject,
                                                                                                        JsonTokenState.OutOfMessage,
                                                                                                        JsonTokenState.WithinObject );

            this.CreateTransition_WithGivenContext_Succeeds(    this.TokenPatterns.BeginOfString,
                                                                previousTransitionStub.Object,
                                                                JsonTokenState.WithinObject,
                                                                JsonTokenState.WithinString );
        }

        [Fact]
        public void CreateTransition_WithinObjectToWithinObject_Succeeds()
        {
            Mock<ITokenTransition<JsonTokenState>> previousTransitionStub = this.CreateTransitionStub(  this.TokenPatterns.EndOfObject,
                                                                                                        JsonTokenState.OutOfMessage,
                                                                                                        JsonTokenState.WithinObject );

            this.CreateTransition_WithGivenContext_Succeeds(    this.TokenPatterns.BeginOfObject,
                                                                previousTransitionStub.Object,
                                                                JsonTokenState.WithinObject,
                                                                JsonTokenState.WithinObject );
        }

        [Fact]
        public void FindNextMatch_BeginOfObject_Succeeds()
        {
            this.FindNextMatch_WithGivenContext_Succeeds(   JsonTokenState.OutOfMessage,
                                                            ":[{",
                                                            this.TokenPatterns.BeginOfObject );
        }

        [Fact]
        public void FindNextMatch_EndOfObject_Succeeds()
        {
            this.FindNextMatch_WithGivenContext_Succeeds(   JsonTokenState.WithinObject,
                                                            @"  }",
                                                            this.TokenPatterns.EndOfObject );
        }

        [Fact]
        public void FindNextMatch_BeginOfString_Succeeds()
        {
            this.FindNextMatch_WithGivenContext_Succeeds(   JsonTokenState.WithinObject,
                                                            "\"abc",
                                                            this.TokenPatterns.BeginOfString );
        }

        [Fact]
        public void FindNextMatch_EndOfString_Succeeds()
        {
            this.FindNextMatch_WithGivenContext_Succeeds(   JsonTokenState.WithinString,
                                                            "abc\"",
                                                            this.TokenPatterns.EndOfString );
        }
    }
}
