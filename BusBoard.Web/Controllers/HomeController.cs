using System;
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
      Response.AddHeader("Refresh", "5");
      return View();
    }

    [HttpGet]
    public ActionResult BusInfo(PostcodeSelection selection)
    {
      Response.AddHeader("Refresh", "30");
      // Add some properties to the BusInfo view model with the data you want to render on the page.
      // Write code here to populate the view model with info from the APIs.
      // Then modify the view (in Views/Home/BusInfo.cshtml) to render upcoming buses.
      var stopInfo = new Dictionary<BusStop, List<BusArrivalPrediction>>();
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
      foreach (var stop in busStops)
      {
        var buses = TflApi.GetListOfArrivalPredictionsForStopPoint(stop.NaptanId).ToList();
        stopInfo.Add(stop, buses);
      }
      var info = new BusInfo(selection.Postcode, stopInfo);
      
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