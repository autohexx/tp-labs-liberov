using System.Net.Sockets;
using System.Net;

namespace Server;

public class ChatServerApp
{
    private readonly int _port;

    private readonly Dictionary<string, ClientConnection> _clients = new Dictionary<string, ClientConnection>(StringComparer.OrdinalIgnoreCase);

    private readonly object _clientsLock = new();

    private readonly object _logLock = new();

    private readonly string _logPath = Path.Combine(AppContext.BaseDirectory, "chat.log");

    public ChatServerApp(int port)
    {
        _port = port;
    }

    public async Task StartAsync()
    {
        TcpListener listener = new(IPAddress.Any, _port);

        listener.Start();

        Console.WriteLine($"Сервер запущен на порту {_port}.");

        Log($"Сервер запущен на порту {_port}");

        while (true) {
            try {
                TcpClient client = await listener.AcceptTcpClientAsync();
                await HandleClientAsync(client);
            }
            catch (Exception ex) {
                Console.WriteLine($"Ошибка принятия клиента: {ex.Message}");
                Log($"Ошибка сервера: {ex.Message}");
            }
        }
    }

    private async Task HandleClientAsync(
        TcpClient tcpClient)
    {
        string? nick = null;
        ClientConnection? connection = null;

        try {
            NetworkStream stream = tcpClient.GetStream();
            StreamReader reader = new(stream);
            StreamWriter writer = new(stream){ AutoFlush = true };

            while (true)
            {
                nick = await reader.ReadLineAsync();

                if (nick == null) {
                    tcpClient.Close();
                    return;
                }

                nick = nick.Trim();

                if (string.IsNullOrWhiteSpace(nick)) {
                    await writer.WriteLineAsync("NICK_ERROR Ник не может быть пустым.");
                    continue;
                }

                if (nick.Contains(' ')) {
                    await writer.WriteLineAsync("NICK_ERROR Ник не должен содержать пробелы.");
                    continue;
                }

                bool nickAdded;

                lock (_clientsLock) {
                    if (_clients.ContainsKey(nick)) {
                        nickAdded = false;
                    } else {
                        connection = new ClientConnection(nick, tcpClient, reader, writer);
                        _clients.Add(nick, connection);
                        nickAdded = true;
                    }
                }

                if (!nickAdded) {
                    await writer.WriteLineAsync("NICK_ERROR Этот ник уже занят.");
                    continue;
                }

                await writer.WriteLineAsync("NICK_OK");
                break;
            }

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {nick} подключился");
            Log($"{nick} подключился");

            await BroadcastAsync($"*** {nick} вошёл в чат ***");

            string? line;

            while ((line = await connection!.Reader.ReadLineAsync()) != null) {
                if (line == "/exit") {
                    break;
                }

                if (line == "/list") {
                    await SendUserListAsync(connection);
                    continue;
                }

                if (line.StartsWith("/w ")) {
                    await HandlePrivateMessageAsync(connection, line);
                    continue;
                }

                string message = $"[{DateTime.Now:HH:mm:ss}] {nick}: {line}";
                Console.WriteLine(message);
                Log($"{nick}: {line}");

                await BroadcastAsync(message);
            }
        }
        catch (IOException)
        {
            if (nick != null)
            {
                Log($"{nick}: соединение неожиданно разорвано");
            }
        }
        catch (SocketException)
        {
            if (nick != null)
            {
                Log($"{nick}: ошибка сетевого соединения");
            }
        }
        catch (Exception ex)
        {
            Log($"Ошибка клиента {nick}: {ex.Message}");
        }
        finally
        {
            if (connection != null)
            {
                RemoveClient(connection);

                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {connection.Nick} отключился");
                Log($"{connection.Nick} отключился");
                await BroadcastAsync($"*** {connection.Nick} покинул чат ***");
            }

            tcpClient.Close();
        }
    }

    private async Task SendUserListAsync(
        ClientConnection client)
    {
        List<string> users;

        lock (_clientsLock)
        {
            users = _clients.Keys.OrderBy(x => x).ToList();
        }

        string text = "Пользователи онлайн: " + string.Join(", ", users);

        try {
            await client.Writer.WriteLineAsync(text);
        } catch (IOException) {}

        Log($"{client.Nick} запросил список пользователей");
    }

    private async Task HandlePrivateMessageAsync(ClientConnection sender, string command) {
        string[] parts = command.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 3) {
            await sender.Writer.WriteLineAsync("Использование: /w ник сообщение");
            return;
        }

        string targetNick = parts[1];
        string text = parts[2];

        ClientConnection? target;

        lock (_clientsLock)
        {
            _clients.TryGetValue(targetNick, out target);
        }

        if (target == null)
        {
            await sender.Writer.WriteLineAsync($"Пользователь '{targetNick}' не найден.");
            return;
        }

        string message = $"[Лично от {sender.Nick}] {text}";

        try
        {
            await target.Writer.WriteLineAsync(message);

            if (!ReferenceEquals(sender, target))
            {
                await sender.Writer.WriteLineAsync($"[Лично для {target.Nick}] {text}");
            }

            Console.WriteLine($"[ЛС] {sender.Nick} -> {target.Nick}: {text}");
            Log($"ЛС {sender.Nick} -> {target.Nick}: {text}");
        }
        catch (IOException)
        {
            await sender.Writer.WriteLineAsync("Не удалось отправить сообщение.");
        }
    }

    private async Task BroadcastAsync(
        string message)
    {
        List<ClientConnection> snapshot;

        lock (_clientsLock)
        {
            snapshot = _clients.Values.ToList();
        }

        foreach (ClientConnection client in snapshot)
        {
            try {
                await client.Writer.WriteLineAsync(message);
            }
            catch (IOException) {}
        }
    }

    private void RemoveClient(
        ClientConnection client)
    {
        lock (_clientsLock)
        {
            if (_clients.TryGetValue(
                    client.Nick,
                    out ClientConnection? existing) &&
                ReferenceEquals(existing, client))
            {
                _clients.Remove(client.Nick);
            }
        }
    }

    private void Log(string message)
    {
        string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";

        lock (_logLock)
        {
            File.AppendAllText(
                _logPath,
                line + Environment.NewLine);
        }
    }
}