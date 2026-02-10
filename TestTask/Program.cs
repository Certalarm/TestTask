using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace TestTask
{
    public class Program
    {

        /// <summary>
        /// Программа принимает на входе 2 пути до файлов.
        /// Анализирует в первом файле кол-во вхождений каждой буквы (регистрозависимо). Например А, б, Б, Г и т.д.
        /// Анализирует во втором файле кол-во вхождений парных букв (не регистрозависимо). Например АА, Оо, еЕ, тт и т.д.
        /// По окончанию работы - выводит данную статистику на экран.
        /// </summary>
        /// <param name="args">Первый параметр - путь до первого файла.
        /// Второй параметр - путь до второго файла.</param>
        static void Main(string[] args)
        {
            IList<LetterStats> singleLetterStats = new List<LetterStats>();
            using (IReadOnlyStream inputStream1 = GetInputStream(args[0]))
            {
                singleLetterStats = FillSingleLetterStats(inputStream1);
            }
            RemoveCharStatsByType(singleLetterStats, CharType.Vowel);
            PrintStatistic(singleLetterStats);

            IList<LetterStats> doubleLetterStats = new List<LetterStats>();
            using (IReadOnlyStream inputStream2 = GetInputStream(args[1]))
            {
                doubleLetterStats = FillDoubleLetterStats(inputStream2);
            }
            RemoveCharStatsByType(doubleLetterStats, CharType.Consonants);
            PrintStatistic(doubleLetterStats);


            // TODO : Необжодимо дождаться нажатия клавиши, прежде чем завершать выполнение программы. (+)
            Console.ReadKey();
        }

        /// <summary>
        /// Ф-ция возвращает экземпляр потока с уже загруженным файлом для последующего посимвольного чтения.
        /// </summary>
        /// <param name="fileFullPath">Полный путь до файла для чтения</param>
        /// <returns>Поток для последующего чтения.</returns>
        private static IReadOnlyStream GetInputStream(string fileFullPath)
        {
            return new ReadOnlyStream(fileFullPath);
        }

        /// <summary>
        /// Ф-ция считывающая из входящего потока все буквы, и возвращающая коллекцию статистик вхождения каждой буквы.
        /// Статистика РЕГИСТРОЗАВИСИМАЯ!
        /// </summary>
        /// <param name="stream">Стрим для считывания символов для последующего анализа</param>
        /// <returns>Коллекция статистик по каждой букве, что была прочитана из стрима.</returns>
        private static IList<LetterStats> FillSingleLetterStats(IReadOnlyStream stream)
        {
            stream.ResetPositionToStart();
            Dictionary<string, LetterStats> letterCountsMap = new Dictionary<string, LetterStats>();
            while (!stream.IsEof)
            {
                char c = stream.ReadNextChar();
                // TODO : заполнять статистику с использованием метода IncStatistic. Учёт букв - регистрозависимый. (+)
                FillSingleStatMap(c, letterCountsMap);
            }
            return new List<LetterStats>(letterCountsMap.Values);
        }

        /// <summary>
        /// Ф-ция считывающая из входящего потока все буквы, и возвращающая коллекцию статистик вхождения парных букв.
        /// В статистику должны попадать только пары из одинаковых букв, например АА, СС, УУ, ЕЕ и т.д.
        /// Статистика - НЕ регистрозависимая!
        /// </summary>
        /// <param name="stream">Стрим для считывания символов для последующего анализа</param>
        /// <returns>Коллекция статистик по каждой букве, что была прочитана из стрима.</returns>
        private static IList<LetterStats> FillDoubleLetterStats(IReadOnlyStream stream)
        {
            stream.ResetPositionToStart();
            Dictionary<string, LetterStats> pairCountsMap = new Dictionary<string, LetterStats>();
            char prevC = '\0';
            while (!stream.IsEof)
            {
                char c = stream.ReadNextChar();
                // TODO : заполнять статистику с использованием метода IncStatistic. Учёт букв - НЕ регистрозависимый. (+)
                FillDoubleStatMap(prevC, c, pairCountsMap);    
                prevC = c;
            }
            return new List<LetterStats>(pairCountsMap.Values);
        }

        /// <summary>
        /// Ф-ция перебирает все найденные буквы/парные буквы, содержащие в себе только гласные или согласные буквы.
        /// (Тип букв для перебора определяется параметром charType)
        /// Все найденные буквы/пары соответствующие параметру поиска - удаляются из переданной коллекции статистик.
        /// </summary>
        /// <param name="letters">Коллекция со статистиками вхождения букв/пар</param>
        /// <param name="charType">Тип букв для анализа</param>
        private static void RemoveCharStatsByType(IList<LetterStats> letters, CharType charType)
        {
            // TODO : Удалить статистику по запрошенному типу букв.
            switch (charType)
            {
                case CharType.Consonants:
                    break;
                case CharType.Vowel:
                    break;
            }
            
        }

        /// <summary>
        /// Ф-ция выводит на экран полученную статистику в формате "{Буква} : {Кол-во}"
        /// Каждая буква - с новой строки.
        /// Выводить на экран необходимо предварительно отсортировав набор по алфавиту.
        /// В конце отдельная строчка с ИТОГО, содержащая в себе общее кол-во найденных букв/пар
        /// </summary>
        /// <param name="letters">Коллекция со статистикой</param>
        private static void PrintStatistic(IEnumerable<LetterStats> letters)
        {
            // TODO : Выводить на экран статистику. Выводить предварительно отсортировав по алфавиту!
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод увеличивает счётчик вхождений по переданной структуре.
        /// </summary>
        /// <param name="letterStats"></param>
        private static void IncStatistic(LetterStats letterStats)
        {
            letterStats.Count++;
        }





        private static void FillSingleStatMap(char c, Dictionary<string, LetterStats> map)
        {
            if (IsGoodSingleChar(c))
            {
                FillStatMap(BuildSingleKey(c), map);
            }
        }

        private static void FillDoubleStatMap(char c1, char c2, Dictionary<string, LetterStats> map)
        {
            if (IsGoodDoubleChar(c1, c2))
            {
                FillStatMap(BuildDoubleKey(c1, c2), map);
            }
        }

        private static string BuildSingleKey(char c) => c.ToString();

        private static string BuildDoubleKey(char c1, char c2) => $"{c1}{c2}".ToUpper();

        private static bool IsGoodSingleChar(char c) =>
            char.IsLetter(c);

        private static bool IsGoodDoubleChar(char c1, char c2) =>
            IsLetterBoth(c1, c2) && IsEquallyBoth(c1, c2);

        private static bool IsLetterBoth(char c1, char c2) =>
            char.IsLetter(c1) && char.IsLetter(c2);

        private static bool IsEquallyBoth(char c1, char c2) =>
            char.ToUpperInvariant(c1) == char.ToUpperInvariant(c2);

        private static void FillStatMap(string key, Dictionary<string, LetterStats> map)
        {
            if (map.TryGetValue(key, out LetterStats existingStat))
            {
                IncStatistic(existingStat);
            }
            else
            {
                map[key] = new LetterStats
                {
                    Letter = key,
                    Count = 1
                };
            }
        }
    }
}
