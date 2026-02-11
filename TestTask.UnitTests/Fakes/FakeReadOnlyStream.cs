using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.UnitTests.Fakes
{
    public class FakeReadOnlyStream : IReadOnlyStream
    {
        public bool IsEof => throw new NotImplementedException();

        #region .ctors
        public FakeReadOnlyStream(string inputData)
        {
            
        }
        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public char ReadNextChar()
        {
            throw new NotImplementedException();
        }

        public void ResetPositionToStart()
        {
            throw new NotImplementedException();
        }
    }
}
