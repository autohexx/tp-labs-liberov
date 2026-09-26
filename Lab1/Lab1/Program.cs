using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Меню выбора:");
                Console.WriteLine("1) Задание 1. Факториал");
                Console.WriteLine("2) Задание 2. Фибоначчи");
                Console.WriteLine("3) Задание 3. Значение функции");
                Console.WriteLine("4) Выход");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": Task1_Factorial(); break;
                    case "2": Task2_Fibonacci(); break;
                    case "3": Task3_Function(); break;
                    case "0":
                        Console.WriteLine("Выход из программы.");
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню. Попробуйте снова.");
                        break;
                }
            }
        }


        static void Task1_Factorial()
        {
            int n;
            while (true)
            {
                Console.Write("Введите целое неотрицательное число n: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out n) && n >= 0)
                    break;
                Console.WriteLine("Ошибка! Введите целое число >= 0.");
            }
            long factorial = 1;
            for (int i = 2; i <= n; i++)
                factorial *= i;

            Console.WriteLine($"{n}! = {factorial}");
        }


        static void Task2_Fibonacci()
        {
            int n;
            while (true)
            {
                Console.Write("Введите целое неотрицательное n: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out n) && n >= 0)
                    break;
                Console.WriteLine("Ошибка! Введите целое число >= 0.");
            }

            long a = 0, b = 1;
            string result = "";
            for (int i = 0; i <= n; i++)
            {
                result += a + (i < n ? ", " : "");
                long next = a + b;
                a = b;
                b = next;
            }
            Console.WriteLine(result);
        }


        static void Task3_Function()
        {
            double x;
            while (true)
            {
                Console.Write("Введите x: ");
                string input = Console.ReadLine();
                if (double.TryParse(input, System.Globalization.NumberStyles.Float,
                                    System.Globalization.CultureInfo.InvariantCulture, out x))
                    break;
                Console.WriteLine("Ошибка! Введите число.");
            }


            double ln43 = Math.Log(4.0 / 3.0);
            double sqrtPart = Math.Sqrt(ln43);
            double A = sqrtPart + (x + 9.0 / 7.0) - Math.Exp(Math.Sin(1.3 * x - 0.7));

            Console.WriteLine($"A = {A:F6}");
        }

       
    }
}


