using System.Collections.Generic;
using BusBoard.Api;

namespace BusBoard.Web.Models
{
    public class DisruptionsAtStop
    {
        public BusStop BusStop { get; set; }
        public List<Disruption> Disruptions { get; set; }

        public DisruptionsAtStop(BusStop stop, List<Disruption> disruptions)
        {
            BusStop = stop;
            Disruptions = disruptions;
        }
    }
}