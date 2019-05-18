using BexsTestDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BexsTestBS
{
    public class ValidationBusiness : IValidationBusiness
    {
        
        public ValidationBusiness()
        {
        }
        public void ValidateInput(string strLine, IEnumerable<Airport> lstAirport)
        {
            String[] split = strLine.Split('-');
            if (split.Length == 2)
            {
                Airport origin = ((List<Airport>)lstAirport).Find(t => t.name == split[0]);
                if (origin == null)
                {
                    throw new Exception($"O aeroporto {split[0]} não foi encontrado no arquivo de entrada");
                }
                Airport destination = ((List<Airport>)lstAirport).Find(t => t.name == split[1]);
                if (destination == null)
                {
                    throw new Exception($"O aeroporto {split[1]} não foi encontrado no arquivo de entrada");
                }
            }
            else
            {
                throw new Exception("O formato do parâmetro deve ser XXX-YYY");
            }
        }

        public void ValidateInput(string origin, string destination, IEnumerable<Airport> lstAirport)
        {

            Airport airportOrigin = ((List<Airport>)lstAirport).Find(t => t.name == origin);
            if (airportOrigin == null)
            {
                throw new Exception($"O aeroporto {origin} não foi encontrado no arquivo de entrada");
            }
            Airport airportDestination = ((List<Airport>)lstAirport).Find(t => t.name == destination);
            if (destination == null)
            {
                throw new Exception($"O aeroporto {destination} não foi encontrado no arquivo de entrada");
            }

        }

    }
}
