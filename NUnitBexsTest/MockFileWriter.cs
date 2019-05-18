using BexsTestBS;
using BexsTestDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitBexsTest
{
    class MockFileWriter : IWriteDestinationFile
    {
        public void UpdateRoutes(string filePath, IEnumerable<string> fileList)
        {
            
        }

        void IWriteDestinationFile.WriteNewRoute(string origin, string destination, int cost, string filePath)
        {
            
        }
    }
}
