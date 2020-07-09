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
          PrintBusesFromStopCode(stopCode, "");
          break;
        case "P":
          Console.WriteLine("Please enter your desired postcode");
          var postcode = Console.ReadLine();
          Console.WriteLine(" ");
          foreach (var sc in GetTwoClosestBusStopsToPostcode(postcode))
          {
            PrintBusesFromStopCode(sc.NaptanId, sc.CommonName);
          }
          break;
        default:
          Console.WriteLine("Please enter S or P");
          break;
      }
    }

    private static IEnumerable<BusStop> GetTwoClosestBusStopsToPostcode(string postcodeString)
    {
      var requestUrl = "http://api.postcodes.io";
      var client = new RestClient(requestUrl);
      var request = new RestRequest($"postcodes/{postcodeString}", DataFormat.Json);
      var response = client.Execute(request);
      var postcodeApiResponse = JsonConvert.DeserializeObject<PostcodeApiResponse>(response.Content);
      var postcode = postcodeApiResponse.Result;
      List<BusStop> nearbyStops = GetBusStopsNearPostcode(postcode);
      IEnumerable<BusStop> closestTwoStops = nearbyStops.OrderBy(stop => stop.Distance).Take(2);
      return closestTwoStops;
    }

    private static List<BusStop> GetBusStopsNearPostcode(PostcodeLocation postcodeLocation)
    {
      var requestUrl = "https://api.tfl.gov.uk";
      var client = new RestClient(requestUrl);
      var request = new RestRequest($"StopPoint?stopTypes=NaptanPublicBusCoachTram&radius=200&useStopPointHierarchy=false&returnLines=false&lat={postcodeLocation.Latitude}&lon={postcodeLocation.Longitude}", DataFormat.Json);
      var response = client.Execute(request);
      var busStopApiResponse = JsonConvert.DeserializeObject<BusStopApiResponse>(response.Content);
      return busStopApiResponse.StopPoints;
    }
    
    private static void PrintBusesFromStopCode(string stopCode, string commonName)
    {
      var response = GetListOfArrivalPredictionsForStopPoint(stopCode);
      var buses = GetNext5BusesFromApiResponse(response);
      Console.WriteLine($"Next 5 Buses at stop {stopCode} - {commonName}");
      foreach (var bus in buses)
      {
        Console.WriteLine(
          $"VehicleID: {bus.VehicleId}, ETA: {bus.ExpectedArrival.AddHours(1).ToString("HH:mm:ss tt")}");
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

    private static List<Bus> GetNext5BusesFromApiResponse(IRestResponse response)
    {
      var buses = JsonConvert.DeserializeObject<List<Bus>>(response.Content);
      buses.Sort();
      return buses.GetRange(0, 5);
    }
  }
}
