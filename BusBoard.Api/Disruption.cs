using System;

namespace BusBoard.Api
{
    public class Disruption
    {
        public string Type { get; set; }
        public DateTime ToDate { get; set; }
        public string Description { get; set; }
        public string Appearance { get; set; }
    }
}