using System;

namespace BusBoard.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      TflApi api = new TflApi();
      PromptUserAndGetInputFromConsole(api);
    }

    private static void PromptUserAndGetInputFromConsole(TflApi api)
    {
      Console.WriteLine("Postcode or Stop point ID? (P or S)");
      switch (Console.ReadLine().ToUpper())
      {
        case "S":
          Console.WriteLine("Please enter your desired stop point ID:");
          var stopCode = Console.ReadLine();
          Console.WriteLine(" ");
          PrintBusesFromStopCode(stopCode, "", api);
          break;
        case "P":
          Console.WriteLine("Please enter your desired postcode");
          var postcode = Console.ReadLine();
          Console.WriteLine(" ");
          foreach (var sc in api.GetTwoClosestBusStopsToPostcode(postcode))
          {
            PrintBusesFromStopCode(sc.NaptanId, sc.CommonName, api);
          }

          break;
        default:
          Console.WriteLine("Please enter S or P");
          break;
      }
    }

    private static void PrintBusesFromStopCode(string stopCode, string commonName, TflApi api)
    {
      var buses = api.GetListOfArrivalPredictionsForStopPoint(stopCode);
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

