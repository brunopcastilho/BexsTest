using BexsTestBS;
using BexsTestDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitBexsTest
{
    class MockFileReader : IReadDestinationFile
    {
        IEnumerable<string> IReadDestinationFile.ReadFile(string filePath)
        {
            List<string> testList = new List<String>();

            testList.Add("A,B,10");
            testList.Add("B,C,10");
            testList.Add("C,E,10");
            testList.Add("A,C,25");
            testList.Add("D,C,10");
            testList.Add("A,X,10");

            return testList;
        }
    }
}
