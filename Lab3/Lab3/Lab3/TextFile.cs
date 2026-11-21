using System.Text;

namespace Program
{
    public class TextFile
    {
        public TextFile()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public Encoding GetEncoding(int number)
        {
            switch (number)
            {
                case 1:
                    return new UTF8Encoding(false);

                case 2:
                    return Encoding.GetEncoding(1251);

                case 3:
                    return Encoding.GetEncoding(866);

                default:
                    throw new ArgumentException("Неизвестная кодировка");
            }
        }

        public string GetEncodingName(int number)
        {
            switch (number)
            {
                case 1:
                    return "UTF-8";

                case 2:
                    return "Windows-1251";

                case 3:
                    return "CP866";

                default:
                    return "Неизвестная кодировка";
            }
        }

        public string SaveText(
            string text,
            string folder,
            string fileName,
            Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(folder))
                throw new ArgumentException("Путь к папке не может быть пустым.");

            if (!Directory.Exists(folder))
                throw new DirectoryNotFoundException(
                    "Указанная папка не существует.");

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException(
                    "Имя файла не может быть пустым.");

            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new ArgumentException(
                    "Имя файла содержит недопустимые символы.");

            string fullPath = Path.Combine(folder, fileName);

            File.WriteAllText(fullPath, text, encoding);

            return fullPath;
        }

        public long GetFileSize(string path)
        {
            FileInfo info = new FileInfo(path);
            return info.Length;
        }

        public int GetTextSize(string text, Encoding encoding)
        {
            return encoding.GetByteCount(text);
        }
    }
}