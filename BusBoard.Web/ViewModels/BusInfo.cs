using System;
using System.Collections.Generic;
using System.Dynamic;
using BusBoard.Api;
using BusBoard.Web.Models;

namespace BusBoard.Web.ViewModels
{
  public class BusInfo
  {
    public BusInfo(string postCode, List<StopInfo> stopInfoList)
    {
      PostCode = postCode;
      StopInfoList = stopInfoList;
    }

    public List<StopInfo> StopInfoList { get; set; }
    

    public string PostCode { get; set; }
    

  }
}