using System;
using System.Collections.Generic;
using System.Dynamic;
using BusBoard.Api;
using BusBoard.Web.Models;

namespace BusBoard.Web.ViewModels
{
  public class BusInfo
  {
    public BusInfo(string postCode, List<ArrivalsAtStop> arrivalsAtStop, List<DisruptionsAtStop> disruptionsAtStop)
    {
      PostCode = postCode;
      ArrivalsAtStop = arrivalsAtStop;
      DisruptionsAtStop = disruptionsAtStop;
    }

    public List<ArrivalsAtStop> ArrivalsAtStop { get; set; }
    
    public List<DisruptionsAtStop> DisruptionsAtStop { get; set; }

    public string PostCode { get; set; }

  }
}