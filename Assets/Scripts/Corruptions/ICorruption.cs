using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectFTP.Corruptions
{
    public interface ICorruption
    {
        void SetUp();
        void TearDown();
    }
}
