using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using TestTask.Helpers;
using TestTask.UnitTests.Fakes;
using Xunit;

namespace TestTask.UnitTests
{
    public class FillStatsTests
    {
        // format: inputData
        public static List<object[]> DataWithNoLetters = new List<object[]>()
        { 
            new object[] { "" },
            new object[] { "123454321708090706,-./" },
            new object[] { "12345\0\0\0\0\0\0\0\0\0\0\t\t\t\r\n\r\n"},
        };

        // format: inputData, singleStatCount, doubleStatCount
        public static List<object[]> DataWithLettersOnly = new List<object[]>()
        { 
            new object[] { "ААБББВВВВГДЕЖЗЯЯЯ", 9, 4 },
            new object[] { "аабббввввгдежзяяя", 9, 4 },
            new object[] { "аАБбБвВВвГдЕЖзяЯЯ", 13, 4},
        };

        public FillStatsTests()
        {
        }

        [Fact]
        public void create_reader_with_null_param_will_be_exception()
        {
            FakeReadOnlyStream sut;

            Assert.Throws<ArgumentNullException>(() => sut = new FakeReadOnlyStream(null));
        }

        [Fact]
        public void fill_stats_with_control_symbols_only_will_be_exception()
        {
            string stream = "\0\0\0\0\0\0\0\0\0\0\t\t\t\r\n\r\n";

            IList<LetterStats> singleResult = new List<LetterStats>();
            IList<LetterStats> doubleResult = new List<LetterStats>();

            using (IReadOnlyStream sut = new FakeReadOnlyStream(stream))
            {
                Assert.Throws<EndOfStreamException>(() => singleResult = HelperFillStat.FillSingleLetterStats(sut));
                Assert.Throws<EndOfStreamException>(() => doubleResult = HelperFillStat.FillDoubleLetterStats(sut));
            }
        }

        [Theory]
        [MemberData(nameof(DataWithNoLetters))]
        public void fill_stats_with_no_letters_symbols_only_will_be_empty_stats(string inputData)
        {
            IList<LetterStats> singleResult = new List<LetterStats>();
            IList<LetterStats> doubleResult = new List<LetterStats>();

            using (IReadOnlyStream sut = new FakeReadOnlyStream(inputData))
            {
                singleResult = HelperFillStat.FillSingleLetterStats(sut);
                doubleResult = HelperFillStat.FillDoubleLetterStats(sut);
            }

            singleResult.Should().HaveCount(0);
            doubleResult.Should().HaveCount(0);
        }

        [Theory]
        [MemberData(nameof(DataWithLettersOnly))]
        public void fill_stats_with_letters_symbols_only_will_has_count(string inputData, int singleStatsCount, int doubleStatsCount)
        {
            IList<LetterStats> singleResult = new List<LetterStats>();
            IList<LetterStats> doubleResult = new List<LetterStats>();

            using (IReadOnlyStream sut = new FakeReadOnlyStream(inputData))
            {
                singleResult = HelperFillStat.FillSingleLetterStats(sut);
                doubleResult = HelperFillStat.FillDoubleLetterStats(sut);
            }

            singleResult.Should().HaveCount(singleStatsCount);
            doubleResult.Should().HaveCount(doubleStatsCount);
        }
    }
}
