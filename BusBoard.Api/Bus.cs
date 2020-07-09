using System;

namespace BusBoard.Api
{
    public class Bus : IComparable
    {
       public string VehicleId { get; set; }
       public string Bearing { get; set; }
       public DateTime ExpectedArrival { get; set; }
       public int CompareTo(object obj)
       {
           if (obj == null) return 1;
           Bus other = obj as Bus;
           if (other != null) return this.ExpectedArrival.CompareTo(other.ExpectedArrival);
           throw new ArgumentException("Object is not a Bus");

       }
    }
    
}