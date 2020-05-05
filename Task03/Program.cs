using System;
using System.Collections.Generic;
using System.Linq;

/*Все действия по обработке данных выполнять с использованием LINQ
 * 
 * Объявите перечисление Manufacturer, состоящее из элементов
 * Dell (код производителя - 0), Asus (1), Apple (2), Microsoft (3).
 * 
 * Обратите внимание на класс ComputerInfo, он содержит поле типа Manufacturer
 * 
 * На вход подается число N.
 * На следующих N строках через пробел записана информация о компьютере: 
 * фамилия владельца, код производителя (от 0 до 3) и год выпуска (в диапазоне 1970-2020).
 * Затем с помощью средств LINQ двумя разными способами (как запрос или через методы)
 * отсортируйте коллекцию следующим образом:
 * 1. Первоочередно объекты ComputerInfo сортируются по фамилии владельца в убывающем порядке
 * 2. Для объектов, у которых фамилии владельцев сопадают, 
 * сортировка идет по названию компании производителя (НЕ по коду) в возрастающем порядке.
 * 3. Если совпадают и фамилия, и имя производителя, то сортировать по году выпуска в порядке убывания.
 * 
 * Выведите элементы каждой коллекции на экран в формате:
 * <Фамилия_владельца>: <Имя_производителя> [<Год_производства>]
 * 
 * Пример ввода:
 * 3
 * Ivanov 1970 0
 * Ivanov 1971 0
 * Ivanov 1970 1
 * 
 * Пример вывода:
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * 
 *  * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * При некорректных входных данных (не связанных с созданием объекта) выбрасывайте FormatException
 * При невозможности создать объект класса ComputerInfo выбрасывайте ArgumentException!
 */
namespace Task03
{
    class Program
    {
        static void Main()
        {
            int N;  //Количество компьютеров
            List<ComputerInfo> computerInfoList = new List<ComputerInfo>(); //Список компьютеров
            try
            {
                N = int.Parse(Console.ReadLine());
                
                for (int i = 0; i < N; i++)
                {
                    string[] info = Console.ReadLine().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    computerInfoList.Add(new ComputerInfo()
                    {
                        Owner = info[0],
                        Year = int.Parse(info[1]),
                        ComputerManufacturer = (Manufacturer)int.Parse(info[2]),
                    });
                }
            }
            catch (FormatException) { Console.WriteLine("FormatException"); }
            catch (ArgumentNullException) { Console.WriteLine("ArgumentNullException"); }
            catch (OverflowException) { Console.WriteLine("OverflowException"); }
            catch (ArgumentException) { Console.WriteLine("ArgumentException"); }


            // выполните сортировку одним выражением
            var computerInfoQuery = from computer in computerInfoList
                orderby computer.Owner descending , Enum.GetName(typeof(Manufacturer), computer.ComputerManufacturer), computer.Year descending 
                select computer;

            PrintCollectionInOneLine(computerInfoQuery);

            Console.WriteLine();

            // выполните сортировку одним выражением
            var computerInfoMethods = computerInfoList.OrderByDescending(computer =>
                    computer.Owner)
                .ThenBy(computer => Enum.GetName(typeof(Manufacturer), computer.ComputerManufacturer))
                .ThenByDescending(computer =>
                    computer.Year);

            PrintCollectionInOneLine(computerInfoMethods);
            Console.ReadLine();
        }

        // выведите элементы коллекции на экран с помощью кода, состоящего из одной линии (должна быть одна точка с запятой)
        /// <summary>
        /// Выводит элементы коллекции на экран в формате:
        /// <Фамилия_владельца>: <Имя_производителя> [<Год_производства>]
        /// </summary>
        public static void PrintCollectionInOneLine(IEnumerable<ComputerInfo> collection)
        {
            Console.Write(collection.Aggregate(string.Empty, (s, val) => 
                s + string.Format("{0}: {1} [{2}]", val.Owner, Enum.GetName(typeof(Manufacturer), val.ComputerManufacturer), val.Year) + Environment.NewLine));
        }
    }


    class ComputerInfo
    {
        /// <summary>
        /// Владелец
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// Год выпуска
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Производитель
        /// </summary>
        public Manufacturer ComputerManufacturer { get; set; }
    }


    enum Manufacturer
    {
        Dell = 0, 
        Asus = 1, 
        Apple = 2, 
        Microsoft = 3
    }
}
