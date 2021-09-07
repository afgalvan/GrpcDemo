using System;
using System.Threading.Tasks;
using Grpc.Core;
using SharedLib;
using static SharedLib.ChatService;

namespace ServiceLib
{
    public class ChatService : ChatServiceBase
    {
        private readonly ChatHub _chatHub;

        public ChatService(ChatHub chatHub)
        {
            _chatHub = chatHub ??
                       throw new ArgumentNullException(nameof(chatHub));
        }

        public override async Task Chat(
            IAsyncStreamReader<ChatMessage> requestStream,
            IServerStreamWriter<ChatMessage> responseStream,
            ServerCallContext context)
        {
            await foreach (ChatMessage request in requestStream.ReadAllAsync())
            {
                await _chatHub.HandleIncomingMessage(request, responseStream);
            }
        }
    }
}
