using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusBoard.Api;
using BusBoard.ConsoleApp;
using BusBoard.Web.Models;
using BusBoard.Web.ViewModels;

namespace BusBoard.Web.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public ActionResult BusInfo(PostcodeSelection selection)
    {
      Response.AddHeader("Refresh", "30");
      IEnumerable<BusStop> busStops;
      try
      {
        var postcodeLocation = PostcodeApi.GetPostcodeLocation(selection.Postcode);
        busStops = TflApi.GetBusStopsNearPostcode(postcodeLocation);
      }
      catch
      {
        return View(new BusInfo(selection.Postcode, null));
      }
      List<StopInfo> arrivalsAtStopList = new List<StopInfo>();
      foreach (var stop in busStops)
      {
        var disruptions = TflApi.GetDisruptionsAtBusStop(stop.NaptanId).ToList();
        var arrivals = TflApi.GetArrivalPredictionsAtBusStop(stop.NaptanId).ToList();
        arrivalsAtStopList.Add(new StopInfo(stop, arrivals,disruptions)); 
      }
      var info = new BusInfo(selection.Postcode, arrivalsAtStopList);
      return View(info);
    }

    public ActionResult About()
    {
      ViewBag.Message = "Information about this site";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Contact us!";

      return View();
    }
  }
}