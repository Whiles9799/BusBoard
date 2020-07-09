using System;

namespace BusBoard.Api
{
    public class BusArrivalPrediction
    {
       public string VehicleId { get; set; }
       public string Bearing { get; set; }
       public DateTime ExpectedArrival { get; set; }
       public string LineName { get; set; }
       public string DestinationName { get; set; }
    }
    
}