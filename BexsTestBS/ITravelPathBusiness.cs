using BexsTestDomain;
using System.Collections.Generic;

namespace BexsTestBS
{
    public interface ITravelPathBusiness
    {
        TravelPath FindBestPath(string strLine, IEnumerable<Airport> lstAirport);
        TravelPath FindBestPath(string origin, string destination, IEnumerable<Airport> lstAirport);
        string GenerateOutput(TravelPath path);
    }
}