using System;

namespace BusBoard.ConsoleApp
{
    public class BusArrivalPrediction
    {
       public string VehicleId { get; set; }
       public string Bearing { get; set; }
       public DateTime ExpectedArrival { get; set; }
       public int TimeToStation { get; set; }
       
    }
    
}