using System.Collections.Generic;
using System.Web.Mvc;
using BusBoard.Api;
using BusBoard.Web.Models;
using BusBoard.Web.ViewModels;

namespace BusBoard.Web.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      Response.AddHeader("Refresh", "30");
      return View();
    }

    [HttpGet]
    public ActionResult BusInfo(PostcodeSelection selection)
    {
      // Add some properties to the BusInfo view model with the data you want to render on the page.
      // Write code here to populate the view model with info from the APIs.
      // Then modify the view (in Views/Home/BusInfo.cshtml) to render upcoming buses.
      var stopInfo = new Dictionary<BusStop, List<BusArrivalPrediction>>();
      foreach (var stop in TflApi.GetTwoClosestBusStopsToPostcode(selection.Postcode))
      {
        var buses = new List<BusArrivalPrediction>();
        foreach (var bus in TflApi.GetListOfArrivalPredictionsForStopPoint(stop.NaptanId))
        {
          buses.Add(bus);
        }
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