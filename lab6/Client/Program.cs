using System.Net.Sockets;

const int DefaultPort = 5555;

Console.Write("Адрес сервера (по умолчанию localhost): ");

string host = Console.ReadLine() is { Length: > 0 } value ? value : "localhost";

Console.Write($"Порт сервера (по умолчанию {DefaultPort}): ");

string? portInput = Console.ReadLine();

int port = DefaultPort;

if (!string.IsNullOrWhiteSpace(portInput))
{
    if (!int.TryParse(portInput, out port) ||
        port < 1 ||
        port > 65535)
    {
        Console.WriteLine("Ошибка: некорректный порт.");
        return;
    }
}

using TcpClient client = new();

try
{
    Console.WriteLine();
    Console.WriteLine($"Подключение к {host}:{port}...");
    await client.ConnectAsync(host, port);
}
catch (SocketException)
{
    Console.WriteLine("Не удалось подключиться к серверу.");
    Console.WriteLine("Проверьте адрес, порт и запущен ли сервер.");
    return;
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка подключения: {ex.Message}");
    return;
}

NetworkStream stream = client.GetStream();

using StreamReader reader = new(stream);

using StreamWriter writer = new(stream){ AutoFlush = true };

while (true)
{
    Console.Write("Введите ник: ");

    string nick = Console.ReadLine() ?? "";

    await writer.WriteLineAsync(nick);

    string? response =
        await reader.ReadLineAsync();

    if (response == "NICK_OK")
    {
        Console.WriteLine($"Вы вошли в чат как {nick}.");
        break;
    }

    if (response == null)
    {
        Console.WriteLine("Сервер закрыл соединение.");
        return;
    }

    if (response.StartsWith("NICK_ERROR"))
    {
        string error = response.Substring("NICK_ERROR".Length).Trim();

        Console.WriteLine($"Ошибка: {error}");
        Console.WriteLine("Попробуйте другой ник.");

        continue;
    }

    Console.WriteLine(response);
}

Console.WriteLine();
Console.WriteLine("Команды:");
Console.WriteLine("/list - пользователи онлайн");
Console.WriteLine("/w ник сообщение - личное сообщение");
Console.WriteLine("/exit - выйти");
Console.WriteLine();

CancellationTokenSource cts = new();

Task receiveTask = ReceiveMessagesAsync(reader, cts.Token);

try
{
    while (true)
    {
        string? message = Console.ReadLine();

        if (message == null)
        {
            continue;
        }

        try
        {
            await writer.WriteLineAsync(message);
        }
        catch (IOException)
        {
            Console.WriteLine("Соединение с сервером потеряно.");

            break;
        }

        if (message == "/exit")
        {
            break;
        }
    }
}
finally
{
    cts.Cancel();

    client.Close();

    try
    {
        await receiveTask;
    }
    catch {  }
}

static async Task ReceiveMessagesAsync(
    StreamReader reader,
    CancellationToken token)
{
    try
    {
        while (!token.IsCancellationRequested)
        {
            string? line = await reader.ReadLineAsync();

            if (line == null)
            {
                Console.WriteLine();
                Console.WriteLine("Соединение с сервером закрыто.");

                break;
            }

            Console.WriteLine(line);
        }
    } catch (IOException) {
        if (!token.IsCancellationRequested)
        {
            Console.WriteLine();
            Console.WriteLine("Соединение с сервером потеряно.");
        }
    } catch (ObjectDisposedException) { }
}