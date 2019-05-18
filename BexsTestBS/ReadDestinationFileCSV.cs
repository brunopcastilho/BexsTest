using BexsTestDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BexsTestBS
{
    public class ReadDestinationFileCSV : IReadDestinationFile
    {

        public IEnumerable<string> ReadFile(string filePath)
        {
            List<string> lstString = new List<string>();
            StreamReader sr = new StreamReader(filePath);
            String line = sr.ReadLine();
            while (line != null && line != "")
            {
                lstString.Add(line);
                line = sr.ReadLine();
            }

            sr.Close();

            return lstString;

        }

    }
}
