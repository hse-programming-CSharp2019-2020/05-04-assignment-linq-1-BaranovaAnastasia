using System;
using System.Collections;
using System.Linq;

/* В задаче не использовать циклы for, while. Все действия по обработке данных выполнять с использованием LINQ
 * 
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * Необходимо оставить только те элементы коллекции, которые предшествуют нулю, или все, если нуля нет.
 * Дважды вывести среднее арифметическое квадратов элементов новой последовательности.
 * Вывести элементы коллекции через пробел.
 * Остальные указания см. непосредственно в коде.
 * 
 * Пример входных данных:
 * 1 2 0 4 5
 * 
 * Пример выходных:
 * 2,500
 * 2,500
 * 1 2
 * 
 * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * В случае возникновения иных нештатных ситуаций (например, в случае попытки итерирования по пустой коллекции) 
 * выбрасывайте InvalidOperationException!
 */
namespace Task02
{
    class Program
    {
        static void Main()
        {
            RunTesk02();
        }

        public static void RunTesk02()
        {
            int[] arr = new int[0];
            try
            {
                // Попробуйте осуществить считывание целочисленного массива, записав это ОДНИМ ВЫРАЖЕНИЕМ.
                arr = (Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    .Select(str => int.Parse(str)).ToArray();
            }
            catch (FormatException) { Console.WriteLine("FormatException"); }
            catch (ArgumentNullException) { Console.WriteLine("ArgumentNullException"); }
            catch (OverflowException) { Console.WriteLine("OverflowException"); }
            catch (ArgumentException) { Console.WriteLine("ArgumentException"); }

            // Выбирает элементы коллекции до 0
            var filteredCollection = arr.TakeWhile(val => val != 0);

            try
            {

                // использовать статическую форму вызова метода подсчета среднего
                double averageUsingStaticForm = checked(Enumerable.Average(Enumerable.Select(filteredCollection, val => val * val)));
                // использовать объектную форму вызова метода подсчета среднего
                double averageUsingInstanceForm = checked(filteredCollection.Select(val => val * val).Average());

                Console.WriteLine($"{averageUsingStaticForm:F3}".Replace('.', ','));
                Console.WriteLine($"{averageUsingInstanceForm:F3}".Replace('.', ','));


                // вывести элементы коллекции в одну строку
                Console.Write(filteredCollection.Skip(1).Aggregate(filteredCollection.First().ToString(),
                    (s, num) => s + ' ' + num));
            }
            catch (InvalidOperationException) { Console.WriteLine("InvalidOperationException"); }
            catch (OverflowException) { Console.WriteLine("OverflowException"); }

            Console.ReadLine();
        }

    }
}
