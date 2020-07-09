using System.Collections.Generic;

namespace BusBoard.Api
{
    public class BusStopApiResponse
    {
        public double[] CentrePoint { get; set; }
        public List<BusStop> StopPoints { get; set; }
    }
}