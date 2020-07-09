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
      Console.WriteLine("Postcode or Stop point ID? (P or S)");
      switch (Console.ReadLine())
      {
        case "S":
          Console.WriteLine("Please enter your desired stop point ID:");
          var stopCode = Console.ReadLine();
          Console.WriteLine(" ");
          PrintBusesFromStopCode(stopCode);
          break;
        case "P":
          Console.WriteLine("Please enter your desired postcode");
          var postcode = Console.ReadLine();
          Console.WriteLine(" ");
          foreach (var sc in GetTwoClosestBusStopsToPostcode(postcode))
          {
            PrintBusesFromStopCode(sc);
          }
          break;
        default:
          Console.WriteLine("Please enter S or P");
          break;
      }
    }

    private static List<String> GetTwoClosestBusStopsToPostcode(string postcodeString)
    {
      IRestResponse response = LookupPostcodeLocation(postcodeString);
      var postcodeApiResponse = JsonConvert.DeserializeObject<PostcodeApiResponse>(response.Content);
      var postcode = postcodeApiResponse.Result;
      return null;
    }

    private static IRestResponse LookupPostcodeLocation(string postcode)
    {
      var requestUrl = "http://api.postcodes.io";
      var client = new RestClient(requestUrl);
      var request = new RestRequest($"postcodes/{postcode}", DataFormat.Json);
      var response = client.Execute(request);
      return response;
    }

    private static void PrintBusesFromStopCode(string stopCode)
    {
      var response = GetListOfArrivalPredictionsForStopPoint(stopCode);
      var buses = GetNext5BusesFromApiResponse(response);
      Console.WriteLine($"Next 5 Buses at stop {stopCode}");
      foreach (var bus in buses)
      {
        Console.WriteLine(
          $"VehicleID: {bus.VehicleId}, ETA: {bus.ExpectedArrival.AddHours(1).ToString("HH:mm:ss tt")}");
      }

      Console.WriteLine(" ");
    }
    

    private static IEnumerable<BusArrivalPrediction> GetNext5BusesForStopPoint(string stopCode)
    {
      var requestUrl = "https://api.tfl.gov.uk";
      var client = new RestClient(requestUrl);
      var request = new RestRequest($"StopPoint/{stopCode}/Arrivals");
      var buses = client.Execute<List<BusArrivalPrediction>>(request).Data;
      IEnumerable<BusArrivalPrediction> orderedBuses = buses.OrderBy(bus => bus.ExpectedArrival).Take(5);
      return orderedBuses;
    }
    
  }
}
