using System.Collections.Generic;

namespace BexsTestBS
{
    public interface IWriteDestinationFile
    {
        void WriteNewRoute(string origin, string destination, int cost, string filePath);
        void UpdateRoutes(string filePath, IEnumerable<string> fileList);
    }
}