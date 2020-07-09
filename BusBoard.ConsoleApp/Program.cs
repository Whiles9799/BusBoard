using System;
using BusBoard.Api;

namespace BusBoard.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      PromptUserAndGetInputFromConsole();
    }

    private static void PromptUserAndGetInputFromConsole()
    {
      Console.WriteLine("Postcode or Stop point ID? (P or S)");
      switch (Console.ReadLine().ToUpper())
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
          foreach (var sc in TflApi.GetTwoClosestBusStopsToPostcode(postcode))
          {
            PrintBusesFromStopCode(sc.NaptanId, sc.CommonName);
          }

          break;
        default:
          Console.WriteLine("Please enter S or P");
          break;
      }
    }

    private static void PrintBusesFromStopCode(string stopCode, string commonName)
    {
      var buses = TflApi.GetListOfArrivalPredictionsForStopPoint(stopCode);
      Console.WriteLine($"Next 5 Buses at stop {stopCode} - {commonName}");
      foreach (var bus in buses)
      {
        Console.WriteLine(
          $"VehicleID: {bus.VehicleId}, ETA: {bus.ExpectedArrival.AddHours(1).ToString("HH:mm:ss tt")}");
      }

      Console.WriteLine(" ");
    }
    
    
    
  }
}
