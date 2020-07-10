using System.Collections.Generic;
using BusBoard.Api;

namespace BusBoard.Web.Models
{
    public class ArrivalsAtStop
    {
        public BusStop BusStop { get; set; }
        public List<BusArrivalPrediction> Arrivals { get; set; }

        public ArrivalsAtStop(BusStop stop, List<BusArrivalPrediction> arrivals)
        {
            BusStop = stop;
            Arrivals = arrivals;
        }
    }
}