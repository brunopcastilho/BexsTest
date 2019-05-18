using System.Collections.Generic;
using BexsTestDomain;

namespace BexsTestBS
{
    public interface IAirportBusiness
    {
        IEnumerable<Airport> ReadAirportList(IEnumerable<string> file);
        IEnumerable<Airport> ReadAirportList(string filePath);
        void Initialize(string filePath, IReadDestinationFile service);
        IEnumerable<Airport> GetAirportList();
        void RefreshCache(string filePath, IReadDestinationFile service);
        bool ContainsRoute(string origin, string destination, string filePath);
        void WriteNewRoute(string origin, string destination, string cost, string filePath, IWriteDestinationFile writer);

    }
}