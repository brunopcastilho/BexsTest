using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BexsTestDomain
{
    [Serializable]
    public class Airport 
    {        
        public Airport(String name)
        {
            this.name = name;
        }

        public String name { get; set; }
        public Dictionary<Airport, int> destination = new Dictionary<Airport, int>();

        public void AddDestination(Airport airport, int cost)
        {
            if (!destination.ContainsKey(airport))
            {
                destination.Add(airport, cost);
            } else
            {
                throw new Exception($"Aeroporto {this.name} já contém uma definição para o destino {airport.name}");
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine ($"Aeroporto - {this.name}");
            foreach(var dest in this.destination)
            {
                builder.AppendLine($"  Destino {dest.Key.name} - Custo {dest.Value} ");
            }
            return builder.ToString();
        }

        public string ToStringExpanded(int indentation)
        {
            StringBuilder builder = new StringBuilder();


            builder.AppendLine(this.ToString());
            builder.AppendLine($"Detalhes:");
            
            foreach (var dest in this.destination)
            {
                builder.AppendLine($"{new String('-', indentation)}{dest.Key.ToStringExpanded(indentation+1)}");
            }

            return builder.ToString();
        }

        public IEnumerable<TravelPath> FindAllPaths(string destinationName , TravelPath path)
        {
            if (path == null)
            {
                path = new TravelPath();
                path.addPath(this, 0);
            }
            List<TravelPath> lstResult  = new List<TravelPath>();

            TravelPath originalPath = path.DeepCopy();

            foreach (Airport item in destination.Keys)
            {
                path = originalPath.DeepCopy();
                bool insertList = path.addPath(item, destination[item]);

                if (item.name == destinationName)
                {
                    lstResult.Add(path);
                }
                if (insertList) {
                    lstResult.AddRange(item.FindAllPaths(destinationName, path));
                }
            }


            return lstResult;

            
        }
        

    }
}
