using System.Collections.Generic;

namespace BusBoard.ConsoleApp
{
    public class BusStopApiResponse
    {
        public double[] CentrePoint { get; set; }
        public BusStop[] StopPoints { get; set; }
    }
}