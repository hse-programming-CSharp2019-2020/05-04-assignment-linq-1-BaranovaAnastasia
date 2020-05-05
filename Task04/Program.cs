using System;
using System.Collections.Generic;
using System.Linq;

/*
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * На основе полученных чисел получить новое по формуле: 5 + a[0] - a[1] + a[2] - a[3] + ...
 * Это необходимо сделать двумя способами:
 * 1) с помощью встроенного LINQ метода Aggregate
 * 2) с помощью своего метода MyAggregate, сигнатура которого дана в классе MyClass
 * Вывести полученные результаты на экран (естесственно, они должны быть одинаковыми)
 * 
 * Пример входных данных:
 * 1 2 3 4 5
 * 
 * Пример выходных:
 * 8
 * 8
 * 
 * Пояснение:
 * 5 + 1 - 2 + 3 - 4 + 5 = 8
 * 
 * 
 * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 */

namespace Task04
{
    class Program
    {
        static void Main()
        {
            RunTesk04();
        }

        public static void RunTesk04()
        {
            int[] arr = new int[0];
            try
            {
                // Попробуйте осуществить считывание целочисленного массива, записав это ОДНИМ ВЫРАЖЕНИЕМ.
                arr = (Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    .Select(str => int.Parse(str)).ToArray();


                // использовать синтаксис методов! SQL-подобные запросы не писать!

                int arrAggregate = arr.Aggregate(-5, (s, val) =>
                    -s + val) * (int)Math.Pow(-1, arr.Length % 2 + 1);

                int arrMyAggregate = MyClass.MyAggregate(arr);

                Console.WriteLine(arrAggregate);
                Console.WriteLine(arrMyAggregate);
            }
            catch (FormatException) { Console.WriteLine("FormatException"); }
            catch (ArgumentNullException) { Console.WriteLine("ArgumentNullException"); }
            catch (OverflowException) { Console.WriteLine("OverflowException"); }
            catch (ArgumentException) { Console.WriteLine("ArgumentException"); }

            Console.ReadLine();
        }
    }

    static class MyClass
    {
        /// <summary>
        /// Для данной коллекции возвращает сумму
        /// 5 + collection[0] - collection[1] + collection[2] - collection[3] + ...
        /// </summary>
        public static int MyAggregate(IEnumerable<int> collection)
        {
            return 5
                   + collection.Select((val, i) => new { val, i })
                       .Where(p => p.i % 2 == 0)
                       .Select(p => p.val)
                       .Sum()
                   - collection.Select((val, i) => new { val, i })
                       .Where(p => p.i % 2 == 1)
                       .Select(p => p.val)
                       .Sum();
        }
    }
}
