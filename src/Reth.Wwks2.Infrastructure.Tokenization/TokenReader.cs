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
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Wwks2.Infrastructure.Tokenization
{
    public abstract class TokenReader<TState>:ITokenReader
        where TState:Enum
    {
        public const int DefaultBufferSize = 4096;
        public const int DefaultMaximumMessageSize = 0x100000;

        private bool isDisposed;

        protected TokenReader( Stream stream, Encoding encoding )
        :
            this( stream, encoding, TokenReader<TState>.DefaultBufferSize, TokenReader<TState>.DefaultMaximumMessageSize )
        {
        }

        protected TokenReader( Stream stream, Encoding encoding, int bufferSize, int maximumMessageSize )
        {
            this.Stream = stream;
            this.Encoding = encoding;
            this.PipeReader = PipeReader.Create( stream, new StreamPipeReaderOptions( leaveOpen:true, bufferSize:bufferSize ) );
            this.Source = Observable.Create<string>( this.RunAsync );

            this.MaximumMessageSize = maximumMessageSize;
        }

        private PipeReader PipeReader
        {
            get;
        }

        private IObservable<string> Source
        {
            get;
        }

        private int MaximumMessageSize
        {
            get;
        }

        public Stream Stream
        {
            get;
        }
        
        public Encoding Encoding
        {
            get;
        }

        protected abstract ITokenFinder<TState> TokenFinder
        {
            get;
        }

        private List<ITokenTransition<TState>> Transitions
        {
            get;
        } = new List<ITokenTransition<TState>>();

        private long BufferPosition
        {
            get; set;
        }

        public IDisposable Subscribe( IObserver<string> observer )
        {
            return this.Source.Subscribe( observer );
        }

        private Task RunAsync( IObserver<string> observer, CancellationToken cancellationToken = default )
        {
            return Task.Factory.StartNew(   () =>
                                            {
                                                bool failed = false;

                                                string? message = null;

                                                using( CancellationTokenRegistration cancellationTokenRegistration = cancellationToken.Register(    () =>
                                                                                                                                                    {
                                                                                                                                                        this.PipeReader.CancelPendingRead();
                                                                                                                                                    }   ) )
                                                {
                                                    while( cancellationToken.IsCancellationRequested == false )
                                                    {
                                                        try
                                                        {
                                                            message = this.GetMessage( out _, cancellationToken );
                                                        }catch( Exception ex )
                                                        {
                                                            observer.OnError( ex );

                                                            failed = true;

                                                            break;
                                                        }

                                                        if( message is not null )
                                                        {
                                                            observer.OnNext( message );
                                                        }else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                }
                                                
                                                if( failed == false )
                                                {
                                                    observer.OnCompleted();
                                                }
                                            },
                                            cancellationToken,
                                            TaskCreationOptions.LongRunning,
                                            TaskScheduler.Default   );
        }

        private string? GetMessage( out ReadOnlySequence<byte>? skippedTrash, CancellationToken cancellationToken = default )
        {
            skippedTrash = null;

            string? result = null;

            ReadOnlySequence<byte> message = ReadOnlySequence<byte>.Empty;

            ReadResult readResult;

            do
            {    
                readResult = this.PipeReader.ReadAsync( cancellationToken ).AsTask().Result;

                if( readResult.IsCanceled == true )
                {
                    break;
                }

                result = this.HandleRead( ref readResult, ref message, out skippedTrash );

                if( readResult.IsCompleted == true )
                {
                    break;
                }
            }while( result is null );

            return result;
        }

        private string? HandleRead( ref ReadResult readResult,
                                    ref ReadOnlySequence<byte> message,
                                    out ReadOnlySequence<byte>? skippedTrash    )
        {
            string? result = null;

            ReadOnlySequence<byte> buffer = readResult.Buffer;

            if( buffer.Length >= this.MaximumMessageSize )
            {
                throw new InvalidOperationException( $"Maximum message size exceeded." );
            }

            SequenceReader<byte> sequenceReader = new( buffer );

            sequenceReader.Advance( this.BufferPosition );

            if( this.TryGetMessage( ref sequenceReader, out message, out skippedTrash ) == true )
            {
                this.BufferPosition = 0;

                this.PipeReader.AdvanceTo( sequenceReader.Position );

                result = this.Encoding.GetString( message );
            }else
            {
                this.BufferPosition = buffer.Length;
                                
                this.PipeReader.AdvanceTo( buffer.GetPosition( 0 ), buffer.End );
            }

            return result;
        }

        private bool TryGetMessage( ref SequenceReader<byte> sequenceReader,
                                    out ReadOnlySequence<byte> message,
                                    out ReadOnlySequence<byte>? skippedTrash  )
        {
            message = ReadOnlySequence<byte>.Empty;
            skippedTrash = null;

            bool result = false;

            List<ITokenTransition<TState>> transitions = this.Transitions;

            ITokenTransition<TState>? currentTransition = this.TokenFinder.FindNextTransition( transitions, ref sequenceReader );

            while( currentTransition is not null && result == false )
            {
                transitions.Add( currentTransition );

                if( transitions.TryGetMessage(  ref sequenceReader,
                                                out message,
                                                out skippedTrash,
                                                out ITokenTransition<TState>? _,
                                                out ITokenTransition<TState>? endOfMessage    ) == true )
                {
                    transitions.RemoveRange( 0, transitions.IndexOf( endOfMessage! ) + 1 );

                    result = true;
                }

                if( result == false )
                {
                    currentTransition = this.TokenFinder.FindNextTransition( transitions, ref sequenceReader );
                }
            }

            return result;
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                this.PipeReader.Complete();

                this.isDisposed = true;
            }
        }
    }
}
