using System;
using System.Collections.Generic;
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
            Console.WriteLine("Please enter your desired postcode");
            var postcode = Console.ReadLine();
            Console.WriteLine(" ");
            var postcodeLocation = PostcodeApi.GetPostcodeLocation(postcode);
            foreach (var sc in TflApi.GetBusStopsNearPostcode(postcodeLocation))
            {
                var buses = TflApi.GetListOfArrivalPredictionsForStopPoint(sc.NaptanId);
                PrintBusesFromStopCode(buses, sc);
            }
        }

        private static void PrintBusesFromStopCode(IEnumerable<BusArrivalPrediction>buses, BusStop busStop)
        {
            Console.WriteLine($"Next 5 Buses at stop {busStop.NaptanId} - {busStop.CommonName}");
            foreach (var bus in buses)
            {
                Console.WriteLine(
                    $"VehicleID: {bus.VehicleId}, ETA: {bus.ExpectedArrival.AddHours(1).ToString("HH:mm:ss tt")}");
            }
            Console.WriteLine(" ");
        }
    
    
    
    }
}