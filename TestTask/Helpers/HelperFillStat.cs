using System.Collections.Generic;

namespace TestTask.Helpers
{
    public static class HelperFillStat
    {
        public static void FillSingleStatMap(char c, Dictionary<string, LetterStats> map)
        {
            if (IsGoodSingleChar(c))
            {
                FillStatMap(BuildSingleKey(c), map);
            }
        }

        public static void FillDoubleStatMap(char c1, char c2, Dictionary<string, LetterStats> map)
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

        /// <summary>
        /// Метод увеличивает счётчик вхождений по переданной структуре.
        /// </summary>
        /// <param name="letterStats"></param>
        private static void IncStatistic(LetterStats letterStats)
        {
            letterStats.Count++;
        }
    }
}
