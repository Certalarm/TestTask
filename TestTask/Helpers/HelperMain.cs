using System;

namespace TestTask.Helpers
{
    public static class HelperMain
    {
        private const string __help = "Пожалуйста введите 2 аргумента командной строки! Пути до файлов для обработки.";
        private const string __pressAnyKey = "Для закрытия окна, нажмите любую клавишу...";

        public static bool IsBadArgs(string[] args) =>
            args == null || args.Length < 2 || string.IsNullOrWhiteSpace(args[0]) || string.IsNullOrWhiteSpace(args[1]);

        public static void PrintHelp() => PrintToConsole(__help);

        public static void WaitPressKey()
        {
            PrintToConsole(string.Empty);
            PrintToConsole(__pressAnyKey);
            Console.ReadKey();
        }

        public static void PrintToConsole(string report) =>
            Console.WriteLine(report);

    }
}
