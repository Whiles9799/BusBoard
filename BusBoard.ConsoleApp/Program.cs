using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Please enter your desired stop point ID:");
      var stopCode = Console.ReadLine();
      Console.WriteLine(" ");
      var response = GetListOfArrivalPredictionsForStopPoint(stopCode);
      var buses = GetNext5Buses(response);
      Console.WriteLine($"Next 5 Buses at stop {stopCode}");
      foreach (var bus in buses)
      {
        Console.WriteLine($"VehicleID: {bus.VehicleId}, ETA: {bus.ExpectedArrival.AddHours(1).ToString("HH:mm:ss tt")}");
      }
      Console.WriteLine(" ");
    }
    

    private static IRestResponse GetListOfArrivalPredictionsForStopPoint(string stopCode)
    {
      var requestUrl = "https://api.tfl.gov.uk";
      var client = new RestClient(requestUrl);
      var request = new RestRequest($"StopPoint/{stopCode}/Arrivals", DataFormat.Json);
      var response = client.Execute(request);
      return response;
    }

    private static List<Bus> GetNext5Buses(IRestResponse response)
    {
      var buses = JsonConvert.DeserializeObject<List<Bus>>(response.Content);
      buses.Sort();
      return buses.GetRange(0, 5);
    }
  }
}
