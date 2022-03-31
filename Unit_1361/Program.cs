using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Unit_1361
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileName = InputFileName();
            var iterationCount = InputIterationCount();
            Console.WriteLine("Время на вставку в List<T>");
            EstimateList(WriteToList, iterationCount, fileName);
            Console.WriteLine();
            Console.WriteLine("Время на вставку в LinkedList<T>");
            EstimateList(WriteToLinkedList, iterationCount, fileName);
            Console.ReadLine();
        }

        static string InputFileName()
        {
            Console.WriteLine("Введите имя файла");
            while (true)
            {
                var fileName = Console.ReadLine();
                if (File.Exists(fileName)) return fileName;
                else Console.WriteLine("Файл не найден");
            }
        }

        static int InputIterationCount()
        {
            Console.WriteLine("Введите количество итераций");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result > 0) return result;
                    else Console.WriteLine("Значение должно быть > 0");
                }
                else Console.WriteLine("Введено не число");
            }
        }

        static void EstimateList(Action<string> action, int iterationCount, string fileName)
        {
            var max = 0L;
            var min = long.MaxValue;
            var avg = 0L;
            var stopWath = new Stopwatch();
            for (int i = 0; i < iterationCount; i++)
            {
                Thread.Sleep(500);
                stopWath.Restart();
                action.Invoke(fileName);
                stopWath.Stop();
                if (min > stopWath.ElapsedMilliseconds) min = stopWath.ElapsedMilliseconds;
                if (max < stopWath.ElapsedMilliseconds) max = stopWath.ElapsedMilliseconds;
                avg += stopWath.ElapsedMilliseconds;
                Console.WriteLine($"Итерация {i + 1}/{iterationCount}: Затрачено времени: {stopWath.ElapsedMilliseconds}мс");
            }
            avg /= iterationCount;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Минимально затраченное время на вставку: {min}мс");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Максимально затраченное время на вставку: {max}мс");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Среднее арифметическое затраченное время на вставку: {avg}мс");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void WriteToList(string fileName)
        {
            var list = new List<string>();
            foreach (var line in File.ReadLines(fileName))
            {
                list.Add(line);
            }
        }

        static void WriteToLinkedList(string fileName)
        {
            var linkedList = new LinkedList<string>();
            foreach (var line in File.ReadLines(fileName))
            {
                linkedList.AddLast(line);
            }
        }
    }
}
