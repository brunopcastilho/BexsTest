using BexsTestDomain;
using System.Collections.Generic;

namespace BexsTestBS
{
    public interface IValidationBusiness
    {
        void ValidateInput(string strLine, IEnumerable<Airport> lstAirport);
        void ValidateInput(string origin, string destination, IEnumerable<Airport> lstAirport);
    }
}