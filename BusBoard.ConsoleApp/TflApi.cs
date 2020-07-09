using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    public class TflApi
    {
        public IEnumerable<BusStop> GetTwoClosestBusStopsToPostcode(string postcodeString)
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
        
        public IEnumerable<BusArrivalPrediction> GetListOfArrivalPredictionsForStopPoint(string stopCode)
        {
            var requestUrl = "https://api.tfl.gov.uk";
            var client = new RestClient(requestUrl);
            var request = new RestRequest($"StopPoint/{stopCode}/Arrivals", DataFormat.Json);
            var buses = client.Execute<List<BusArrivalPrediction>>(request).Data;
            IEnumerable<BusArrivalPrediction> nextFiveBuses = buses.OrderBy(bus => bus.ExpectedArrival).Take(5);
            return nextFiveBuses;
        }
        
        private List<BusStop> GetBusStopsNearPostcode(PostcodeLocation postcodeLocation)
        {
            var requestUrl = "https://api.tfl.gov.uk";
            var client = new RestClient(requestUrl);
            var request = new RestRequest($"StopPoint?stopTypes=NaptanPublicBusCoachTram&radius=200&useStopPointHierarchy=false&returnLines=false&lat={postcodeLocation.Latitude}&lon={postcodeLocation.Longitude}", DataFormat.Json);
            var response = client.Execute(request);
            var busStopApiResponse = JsonConvert.DeserializeObject<BusStopApiResponse>(response.Content);
            return busStopApiResponse.StopPoints;
        }
    }
}