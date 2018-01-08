using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricityApplication.Models
{
   
        public class JCSJ
        {
            public DateTime Time { get; set; }
            public int TimeYear { get; set; }
            public int TimeMonth { get; set; }
            public int TimeDay { get; set; }
            public int TimeHour { get; set; }
            public string TagName { get; set; }
            public string Tnumber { get; set; }
            public string hou { get; set; }
            public double PV { get; set; }
            public double Avgpv { get; set; }
        }
    }
