using BexsTestDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BexsTestBS
{
    public interface IReadDestinationFile
    {
       
        IEnumerable<string> ReadFile(string filePath);
    }
}
