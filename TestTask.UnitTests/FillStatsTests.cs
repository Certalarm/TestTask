using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TestTask.UnitTests.Fakes;
using TestTask.UnitTests.InputData;
using Xunit;
using Xunit.Abstractions;

namespace TestTask.UnitTests
{
    public class FillStatsTests
    {
        public static List<object[]> Data = new List<object[]>()
        {
           // new object[] { null, new Exception() },
            new object[] { "",  },
            new object[] { "\0\0\0\0\0\0\0\0\0\0\t\t\t\r\n\r\n"},
            new object[] { "123454321708090706,-./" },
        };

        public FillStatsTests()
        {
            //foreach(var inputData in DataFillStat.Data)
            //    _sut.Add(new FakeReadOnlyStream(inputData));
        }

        // [Theory]
        // [MemberData(nameof(Data))]
        [Fact]
        public void create_reader_with_null_param_will_be_exception()
        {
            Assert.Throws<ArgumentNullException>(() => new FakeReadOnlyStream(null));
        }
    }
}
