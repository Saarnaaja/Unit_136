using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_1362
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileName = InputFileName();
            var count = InputWordCount();
            ShowTopCountOfWord(fileName, count);
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

        static int InputWordCount()
        {
            Console.WriteLine("Введите количество выводимых слов");
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

        static void ShowTopCountOfWord(string fileName, int wordCount)
        {
            var text = File.ReadAllText(fileName).Replace('\n', ' ');
            var separator = new char[] { ' ' };
            var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray()).Split(separator, StringSplitOptions.RemoveEmptyEntries);
            var grouped = noPunctuationText.GroupBy(word => word)
                                           .Select(word => new { Word = word.Key, Count = word.Count() })
                                           .OrderByDescending(word => word.Count);
            Console.WriteLine($"{wordCount} слов, чаще всего встречающихся в тексте:");
            Console.WriteLine("Слово\tКоличество встреченных раз");
            foreach (var i in grouped.Take(wordCount)) 
                Console.WriteLine($"{i.Word}\t{i.Count}");

        }
    }
}
