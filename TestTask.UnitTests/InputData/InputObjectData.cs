using System.Collections.Generic;

namespace TestTask.UnitTests.InputData
{
    public static class InputObjectData
    {
        // format: inputData
        public static List<object[]> DataWithNoLetters = new List<object[]>()
        {
            new object[] { "" },
            new object[] { "123454321708090706,-./" },
            new object[] { "12345\0\0\0\0\0\0\0\0\0\0\t\t\t\r\n\r\n"},
        };

        // format: inputData, singleStatCount, doubleStatCount
        public static List<object[]> DataWithLettersOnlyFillStat = new List<object[]>()
        {
            new object[] { "ААБББВВВВГДЕЖЗЯЯЯ", 9, 4 },
            new object[] { "аабббввввгдежзяяя", 9, 4 },
            new object[] { "аАБбБвВВвГдЕЖзяЯЯ", 13, 4},
        };

        // format: inputData, singleStatCount, doubleStatCount
        public static List<object[]> DataWithLettersOnlyRemoveVowelStat = new List<object[]>()
        {
            new object[] { "ААБББВВВВГДЕЖЗЯЯЯ", 6, 2 },
            new object[] { "аабббввввгдежзяяя", 6, 2 },
            new object[] { "аАБбБвВВвГдЕЖзяЯЯ", 8, 2 },
        };

        // format: inputData, singleStatCount, doubleStatCount
        public static List<object[]> DataWithLettersOnlyRemoveConsonantStat = new List<object[]>()
        {
            new object[] { "ААБББВВВВГДЕЖЗЯЯЯ", 3, 2 },
            new object[] { "аабббввввгдежзяяя", 3, 2 },
            new object[] { "аАБбБвВВвГдЕЖзяЯЯ", 5, 2 },
        };


        // format: inputData, singleStatLineCount
        public static List<object[]> DataWithLettersOnlySinglePrintStat = new List<object[]>()
        {
            new object[] { "ААБББВВВВГДЕЖЗЯЯЯ", 7 },
            new object[] { "аабббввввгдежзяяя", 7 },
            new object[] { "аАБбБвВВвГдЕЖзяЯЯ", 9 },
        };

        // format: inputData, doubleStatCount
        public static List<object[]> DataWithLettersOnlyDoublePrintStat = new List<object[]>()
        {
            new object[] { "ААБББВВВВГДЕЖЗЯЯЯ", 3 },
            new object[] { "аабббввввгдежзяяя", 3 },
            new object[] { "аАБбБвВВвГдЕЖзяЯЯ", 3 },
        };
    }
}
