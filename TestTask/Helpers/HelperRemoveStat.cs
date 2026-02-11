using System.Collections.Generic;
using System.Linq;

namespace TestTask.Helpers
{
    public static class HelperRemoveStat
    {
        private static HashSet<char> __vowelsEnRu = new HashSet<char>("aeiouAEIOUаеёиоуыэюяАЕЁИОУЫЭЮЯ".ToCharArray());

        /// <summary>
        /// Ф-ция перебирает все найденные буквы/парные буквы, содержащие в себе только гласные или согласные буквы.
        /// (Тип букв для перебора определяется параметром charType)
        /// Все найденные буквы/пары соответствующие параметру поиска - удаляются из переданной коллекции статистик.
        /// </summary>
        /// <param name="letters">Коллекция со статистиками вхождения букв/пар</param>
        /// <param name="charType">Тип букв для анализа</param>
        public static void RemoveCharStatsByType(IList<LetterStats> letters, CharType charType)
        {
            // TODO : Удалить статистику по запрошенному типу букв. (+)
            switch (charType)
            {
                case CharType.Consonants:
                    RemoveConsonants(letters);
                    break;
                case CharType.Vowel:
                    RemoveVowels(letters);
                    break;
            }
        }

        private static void RemoveVowels(IList<LetterStats> letters)
        {
            letters = GetConsonantsOnly(letters)
                .ToList();
        }

        private static void RemoveConsonants(IList<LetterStats> letters)
        {
            letters = GetVowelsOnly(letters)
                .ToList();
        }

        private static IEnumerable<LetterStats> GetVowelsOnly(IList<LetterStats> letters) =>
            letters
                .Where(x => __vowelsEnRu.Contains(x.Letter.First()));

        private static IEnumerable<LetterStats> GetConsonantsOnly(IList<LetterStats> letters) =>
            letters
                .Where(x => !__vowelsEnRu.Contains(x.Letter.First()));
    }
}
