using System;
using System.Threading.Tasks;
using Greetings.Service;
using Grpc.Core;

namespace Greetings.Client
{
    class Program
    {
        static async Task Main()
        {
            const string url = "localhost:5000";
            var channel = new Channel(url, ChannelCredentials.Insecure);
            var client  = new Greeter.GreeterClient(channel);

            HelloReply helloResponse = await client.SayHelloAsync(
                new HelloRequest { Name = "Andres" }
            );
            Console.WriteLine(helloResponse.Message);
        }
    }
}
