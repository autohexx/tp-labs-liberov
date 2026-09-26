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
                Console.WriteLine("2) Выход");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": Task1_Factorial(); break;
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
        }
 }


