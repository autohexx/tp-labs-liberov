namespace Server;

internal class Program
{
     static async Task Main(string[] args)
    {
        int port = 5555;

        ChatServerApp server = new(port);

        await server.StartAsync();
    }
}
