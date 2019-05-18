using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace BexsTestDomain
{
    [Serializable]
    public class TravelPath
    {

        public List<TravelPathItem> travelPath;

        public TravelPath()
        {
            this.travelPath = new List<TravelPathItem>();
        }

        public bool addPath(Airport airport, int cost)
        {
            if (!checkIfExists(airport))
            {
                this.travelPath.Add(new TravelPathItem(airport, cost));
                return true;
            }
            return false;
        }

        private bool checkIfExists(Airport airport)
        {
            foreach (var item in travelPath)
            {
                if (item.airport.name == airport.name)
                {
                    return true;
                }
            }
            return false;
        }

        public TravelPath DeepCopy()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (TravelPath)copy;
        }

        public int calculateCost()
        {
            int cost = 0;
            foreach (var item in this.travelPath)
            {
                cost += item.cost;
            }
            return cost;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Caminho ");
            foreach (var item in this.travelPath)
            {
                sb.Append($"{ item.airport.name} -> {item.cost} ");
            }
            sb.Append($"Custo Total - {this.calculateCost()}");

            return sb.ToString();
        }

        

    }
}
