using System;
using System.Collections.Generic;
using System.Linq;
using BusBoard.Api;
using Newtonsoft.Json;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    public class TflApi
    {
        public static IEnumerable<BusArrivalPrediction> GetArrivalPredictionsAtBusStop(string stopCode)
        {
            var requestUrl = "https://api.tfl.gov.uk";
            var client = new RestClient(requestUrl);
            var request = new RestRequest($"StopPoint/{stopCode}/Arrivals");
            var buses = client.Execute<List<BusArrivalPrediction>>(request).Data;
            return buses.OrderBy(bus => bus.ExpectedArrival).Take(5);
        }
        
        public static IEnumerable<BusStop> GetBusStopsNearPostcode(PostcodeLocation postcodeLocation)
        {
            var requestUrl = "https://api.tfl.gov.uk";
            var client = new RestClient(requestUrl);
            var request = new RestRequest($"StopPoint?stopTypes=NaptanPublicBusCoachTram&radius=200&useStopPointHierarchy=false&returnLines=false&lat={postcodeLocation.Latitude}&lon={postcodeLocation.Longitude}");
            var response = client.Execute(request);
            var busStopApiResponse = JsonConvert.DeserializeObject<BusStopApiResponse>(response.Content);
            var nearbyStops = busStopApiResponse.StopPoints;
            return nearbyStops.OrderBy(stop => stop.Distance).Take(2);
        }

        public static IEnumerable<Disruption> GetDisruptionsAtBusStop(string stopCode)
        {
            var requestUrl = "https://api.tfl.gov.uk";
            var client = new RestClient(requestUrl);
            var request = new RestRequest($"StopPoint/{stopCode}/Disruption");
            var response = client.Execute(request);
            var disruptionApiResponse = JsonConvert.DeserializeObject<List<Disruption>>(response.Content);
            Console.WriteLine(disruptionApiResponse);
            foreach(var i in disruptionApiResponse)
                Console.WriteLine(i.Description);
            return disruptionApiResponse;
        }
        
        
        
    }
}