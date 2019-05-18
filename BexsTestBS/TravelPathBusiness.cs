using BexsTestDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BexsTestBS
{
    public class TravelPathBusiness : ITravelPathBusiness
    {
        IValidationBusiness validation;
        public TravelPathBusiness(IValidationBusiness validation)
        {
            this.validation = validation;
        }
        public TravelPath FindBestPath(string origin, string destination, IEnumerable<Airport> lstAirport)
        {

            validation.ValidateInput(origin, destination, lstAirport);
            var result = ((List<Airport>)lstAirport).Find(t => t.name == origin).FindAllPaths(destination, null);
            if (result.Count() == 0)
            {
                throw new Exception("Não foi possível encontrar um caminho");
            }
            else
            {
                return result.OrderBy(p => p.calculateCost()).First();
            }
        }
        public TravelPath FindBestPath(string strLine, IEnumerable<Airport> lstAirport)
        {
            String[] split = strLine.Split('-');
            return FindBestPath(split[0], split[1],  lstAirport);
        }

        public String GenerateOutput(TravelPath path)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{path.ToString()}");

            return sb.ToString();
        }

    }
}
