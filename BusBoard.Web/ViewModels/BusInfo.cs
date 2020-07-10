using System;
using System.Collections.Generic;
using System.Dynamic;
using BusBoard.Api;

namespace BusBoard.Web.ViewModels
{
  public class BusInfo
  {
    public BusInfo(string postCode, Dictionary<BusStop,List<BusArrivalPrediction>> buses)
    {
      PostCode = postCode;
      IncomingBusesToStop = buses;
    }

    public Dictionary<BusStop,List<BusArrivalPrediction>> IncomingBusesToStop { get; set; }

    public string PostCode { get; set; }

  }
}