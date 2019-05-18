using BexsTestDomain;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace BexsTestBS
{
    public class AirportBusiness : IAirportBusiness
    {
        IEnumerable<Airport> lstAirport;
        IReadDestinationFile destinationReader;

        IMemoryCache memoryCache;
        

        public AirportBusiness(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IEnumerable<Airport> GetAirportList()
        {
            return lstAirport;
        }

        public void Initialize(string filePath, IReadDestinationFile service)
        {
            destinationReader = service;
            lstAirport = memoryCache.GetOrCreate<IEnumerable<Airport>>("lstAirport", t => { return ReadAirportList(filePath); });
        }

        public void RefreshCache(string filePath, IReadDestinationFile service)
        {
            memoryCache.Remove("lstAirport");
            Initialize(filePath, service);
        }

        public IEnumerable<Airport> ReadAirportList(string filePath)
        {

            IEnumerable<Airport> lstAirport = ReadAirportList(destinationReader.ReadFile(filePath));
            return lstAirport;
        }

        

        public IEnumerable<Airport> ReadAirportList(IEnumerable<string> file)
        {
            List<Airport> lstAirport = new List<Airport>();

            foreach (var item in file)
            {

                String[] split = item.Split(",");
                String strAirpDestination = "";
                String strAirpOrigin = "";
                int cost;
                if (split.Length == 3 && int.TryParse(split[2], out cost))
                {
                    strAirpOrigin = split[0];
                    strAirpDestination = split[1];
                }
                else
                {
                    throw new Exception($"Formato da linha incorreto - Linha {item}");
                }


                Airport airportOrigin = lstAirport.Find(p => p.name == strAirpOrigin);
                Airport airportDestination = lstAirport.Find(p => p.name == strAirpDestination);

                if (airportDestination == null)
                {
                    airportDestination = new Airport(strAirpDestination);
                    lstAirport.Add(airportDestination);
                }


                if (airportOrigin != null)
                {
                    airportOrigin.AddDestination(airportDestination, cost);
                }
                else
                {
                    airportOrigin = new Airport(strAirpOrigin);
                    airportOrigin.AddDestination(airportDestination, cost);
                    lstAirport.Add(airportOrigin);
                }



            }

            return lstAirport;

        }

        public bool ContainsRoute(string origin, string destination, string filePath)
        {            
            Airport airportOrigin = ((List<Airport>)lstAirport).Find(p => p.name == origin);
            Airport airportDestination = ((List<Airport>)lstAirport).Find(p => p.name == destination);
            if (airportOrigin.destination.ContainsKey(airportDestination) )
            {
                return true;
            }
            return false;

        }

        public void WriteNewRoute(string origin, string destination, string cost, string filePath, IWriteDestinationFile writer)
        {
            int costParam = 0;
            if (int.TryParse(cost, out costParam))
            {
                if (ContainsRoute(origin, destination, filePath))
                {
                    List<string> fileList = destinationReader.ReadFile(filePath).ToList();
                    for (int i = 0; i < fileList.Count(); i++)
                    {
                        string item = fileList[i];
                        if (item.Contains($"{origin},{destination}"))
                        {
                            item = $"{origin},{destination},{cost}";
                            fileList[i] = item;
                        }
                    }
                    writer.UpdateRoutes(filePath, fileList);
                }
                else
                {
                    writer.WriteNewRoute(origin, destination, costParam, filePath);
                }
                RefreshCache(filePath, destinationReader);
            }
            else
            {
                throw new Exception("O Parâmetro custo deve ser numérico");
            }

        }


    }
}
