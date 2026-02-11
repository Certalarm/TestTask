using System.Collections.Generic;

namespace TestTask.Helpers
{
    public static class HelperFillStat
    {
        /// <summary>
        /// Ф-ция считывающая из входящего потока все буквы, и возвращающая коллекцию статистик вхождения каждой буквы.
        /// Статистика РЕГИСТРОЗАВИСИМАЯ!
        /// </summary>
        /// <param name="stream">Стрим для считывания символов для последующего анализа</param>
        /// <returns>Коллекция статистик по каждой букве, что была прочитана из стрима.</returns>
        public static IList<LetterStats> FillSingleLetterStats(IReadOnlyStream stream)
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
        public static IList<LetterStats> FillDoubleLetterStats(IReadOnlyStream stream)
        {
            stream.ResetPositionToStart();
            Dictionary<string, LetterStats> pairCountsMap = new Dictionary<string, LetterStats>();
            char prevC = '\0';
            while (!stream.IsEof)
            {
                char c = stream.ReadNextChar();
                // TODO : заполнять статистику с использованием метода IncStatistic. Учёт букв - НЕ регистрозависимый. (+)
                HelperFillStat.FillDoubleStatMap(prevC, c, pairCountsMap);
                prevC = c;
            }
            return new List<LetterStats>(pairCountsMap.Values);
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
                IncStatistic(ref existingStat);
                map[key] = existingStat;
            }
            else
            {
                map[key] = CreateLetterStats(key);
            }
        }

        private static LetterStats CreateLetterStats(string key) =>
            new LetterStats
            {
                Letter = key,
                Count = 1
            };

        /// <summary>
        /// Метод увеличивает счётчик вхождений по переданной структуре.
        /// </summary>
        /// <param name="letterStats"></param>
        private static void IncStatistic(ref LetterStats letterStats)
        {
            letterStats.Count++;
        }
    }
}
