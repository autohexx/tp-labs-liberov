using System.Text;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextFile tfile = new TextFile();

            try
            {
                Console.WriteLine("Введите текст:");
                Console.WriteLine("Для завершения ввода введите пустую строку:");
                Console.WriteLine();

                StringBuilder textBuilder = new StringBuilder();

                while (true)
                {
                    string? line = Console.ReadLine();

                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }

                    textBuilder.AppendLine(line);
                }

                string text = textBuilder.ToString();

                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.WriteLine("Текст не был введён");
                    return;
                }

                Console.Write("Введите путь к папке: ");
                string folder = Console.ReadLine() ?? "";

                Console.Write("Введите имя файла, например text.txt: ");
                string fileName = Console.ReadLine() ?? "";

                Console.WriteLine();
                Console.WriteLine("Выберите кодировку:");
                Console.WriteLine("1: UTF-8");
                Console.WriteLine("2: Windows-1251");
                Console.WriteLine("3: CP866");

                Console.Write("Ваш выбор: ");

                if (!int.TryParse(Console.ReadLine(), out int encodingNumber))
                {
                    Console.WriteLine("Необходимо ввести число");
                    return;
                }

                if (encodingNumber < 1 || encodingNumber > 3)
                {
                    Console.WriteLine("Такой кодировки нет");
                    return;
                }

                Encoding selectedEncoding =
                    tfile.GetEncoding(encodingNumber);

                string fullPath = tfile.SaveText(
                    text,
                    folder,
                    fileName,
                    selectedEncoding);

                Console.WriteLine();
                Console.WriteLine("Файл успешно сохранён");
                Console.WriteLine($"Путь: {fullPath}");
                Console.WriteLine($"Кодировка: {tfile.GetEncodingName(encodingNumber)}");

                Console.WriteLine();
                Console.WriteLine("Размер этого текста в разных кодировках:");

                Encoding utf8 = tfile.GetEncoding(1);
                Encoding windows1251 = tfile.GetEncoding(2);
                Encoding cp866 = tfile.GetEncoding(3);

                Console.WriteLine($"UTF-8: {tfile.GetTextSize(text, utf8)} байт");
                Console.WriteLine($"Windows-1251: {tfile.GetTextSize(text, windows1251)} байт");
                Console.WriteLine($"CP866: {tfile.GetTextSize(text, cp866)} байт");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}