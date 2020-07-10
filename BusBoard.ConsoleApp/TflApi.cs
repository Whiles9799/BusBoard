﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    public class TflApi
    {
        public IEnumerable<BusArrivalPrediction> GetListOfArrivalPredictionsForStopPoint(string stopCode)
        {
            var requestUrl = "https://api.tfl.gov.uk";
            var client = new RestClient(requestUrl);
            var request = new RestRequest($"StopPoint/{stopCode}/Arrivals");
            var buses = client.Execute<List<BusArrivalPrediction>>(request).Data;
            return buses.OrderBy(bus => bus.ExpectedArrival).Take(5);
        }
        
        public IEnumerable<BusStop> GetBusStopsNearPostcode(PostcodeLocation postcodeLocation)
        {
            var requestUrl = "https://api.tfl.gov.uk";
            var client = new RestClient(requestUrl);
            var request = new RestRequest($"StopPoint?stopTypes=NaptanPublicBusCoachTram&radius=200&useStopPointHierarchy=false&returnLines=false&lat={postcodeLocation.Latitude}&lon={postcodeLocation.Longitude}");
            var response = client.Execute(request);
            var busStopApiResponse = JsonConvert.DeserializeObject<BusStopApiResponse>(response.Content);
            var nearbyStops = busStopApiResponse.StopPoints;
            return nearbyStops.OrderBy(stop => stop.Distance).Take(2);
        }
        
        
        
    }
}