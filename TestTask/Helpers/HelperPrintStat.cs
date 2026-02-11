using System;
using System.Collections.Generic;
using System.Linq;

namespace TestTask.Helpers
{
    public static class HelperPrintStat
    {
        private const string __space = " ";
        private const string __colon = ":";
        private const string __total = "ИТОГО";
        private const string __empty = "[ПУСТО]";
        private static readonly string __rn = Environment.NewLine;

        public static string BuildReport(IEnumerable<LetterStats> letters)
        {
            if (!letters.Any())
                return __empty;
            var sortedLetters = SortedLetters(letters);
            var body = string.Join(__rn, BuildLines(sortedLetters));
            var total = BuildTotal(letters.Count());
            return $"{body}{__rn}{total}";
        }

        private static IEnumerable<LetterStats> SortedLetters(IEnumerable<LetterStats> letters) =>
            letters
                .OrderBy(x => x.Letter);

        private static IEnumerable<string> BuildLines(IEnumerable<LetterStats> letters) =>
            letters
                .Select(x => BuildLine(x));

        private static string BuildLine(LetterStats stat) => $"{stat.Letter}{BuildBreak()}{stat.Count}";

        private static string BuildTotal(int count) => $"{__total}{BuildBreak()}{count}";

        private static string BuildBreak() => $"{__space}{__colon}{__space}"; 
    }
}
