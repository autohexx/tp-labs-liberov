using System.Net.Sockets;

namespace Server;

public class ClientConnection(
    string nick,
    TcpClient client,
    StreamReader reader,
    StreamWriter writer)
{
    public string Nick { get; } = nick;

    public TcpClient Client { get; } = client;

    public StreamReader Reader { get; } = reader;

    public StreamWriter Writer { get; } = writer;
}