using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    public class PostcodeApi
    {
        public static PostcodeLocation GetPostcodeLocation(string postcodeString)
        {
            var requestUrl = "http://api.postcodes.io";
            var client = new RestClient(requestUrl);
            var request = new RestRequest($"postcodes/{postcodeString}", DataFormat.Json);
            var response = client.Execute<PostcodeApiResponse>(request).Data;
            return response.Result;
        }
    }
}