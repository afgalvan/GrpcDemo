using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using SharedLib;

namespace Client
{
    internal class CallbackHandler
    {
        private readonly IAsyncStreamReader<ChatMessage> _responseStream;
        public           Task                            Task { get; }

        public CallbackHandler(IAsyncStreamReader<ChatMessage> responseStream)
            : this(responseStream, CancellationToken.None)
        {
        }

        public CallbackHandler(IAsyncStreamReader<ChatMessage> responseStream,
            CancellationToken cancellationToken)
        {
            _responseStream = responseStream ??
                              throw new ArgumentNullException(
                                  nameof(responseStream));
            Task               = Task.Run(Consume, cancellationToken);
        }


        private async Task Consume()
        {
            await foreach (var response in _responseStream.ReadAllAsync())
            {
                Console.WriteLine(
                    $"{response.User}: {response.Text}");
            }
        }
    }
}
