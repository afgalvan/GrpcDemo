#nullable enable
using System;
using System.Threading.Tasks;
using Grpc.Core;
using SharedLib;

namespace Client
{
    class Program
    {
        private static string? _userName;

        static async Task Main()
        {
            const string url = "localhost:5000";
            var channel = new Channel(url, ChannelCredentials.Insecure);
            var client = new ChatService.ChatServiceClient(channel);

            Console.Write("Enter user name to join chat: ");
            _userName = Console.ReadLine();

            using var chatStream = client.Chat();

            var callbackHandler =
                new CallbackHandler(chatStream.ResponseStream);

            await chatStream.RequestStream.WriteAsync(new ChatMessage
                { User = _userName, Text = string.Empty });

            await InputLoop(chatStream);

            await callbackHandler.Task;
        }

        private static async Task InputLoop(
            AsyncDuplexStreamingCall<ChatMessage, ChatMessage> chatStream)
        {
            while (true)
            {
                string? input = Console.ReadLine();
                await chatStream.RequestStream.WriteAsync(new ChatMessage
                    { User = _userName, Text = input });
            }
        }
    }
}
