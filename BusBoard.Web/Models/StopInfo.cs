using System.Collections.Generic;
using BusBoard.Api;

namespace BusBoard.Web.Models
{
    public class StopInfo
    {
        public BusStop BusStop { get; set; }
        public List<BusArrivalPrediction> Arrivals { get; set; }
        
        public List<Disruption> Disruptions { get; set; }


        public StopInfo(BusStop stop, List<BusArrivalPrediction> arrivals, List<Disruption> disruptions)
        {
            BusStop = stop;
            Arrivals = arrivals;
            Disruptions = disruptions;
        }
    }
}