using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BexsTestBS
{
    public class WriteDestinationFile : IWriteDestinationFile
    {
        public void WriteNewRoute(String origin, String destination, int cost, String filePath)
        {
            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine($"{origin},{destination},{cost}");
            writer.Close();
        }

        public void UpdateRoutes(String filePath , IEnumerable<string> fileList)
        {
            StreamWriter writer = new StreamWriter(filePath, false);
            foreach (string item in fileList)
            {
                writer.WriteLine(item);
            }
            writer.Close();
        }
        
    }
}
