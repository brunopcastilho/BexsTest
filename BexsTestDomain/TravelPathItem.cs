using System;
using System.Collections.Generic;
using System.Text;

namespace BexsTestDomain
{
    [Serializable]
    public class TravelPathItem
    {
        public TravelPathItem(Airport airport,int cost)
        {
            this.airport = airport;
            this.cost = cost;
        }
        public Airport airport { get; set; }
        public int cost { get; set; }
    }
}
