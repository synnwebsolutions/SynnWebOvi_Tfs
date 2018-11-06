using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class JsonLineCahrtItem
    {
        public string Label;
        public int Y;
    }

    public class JsonChartData
    {
        public string ChartTitle { get; set; }
        public string ChartData { get; set; }
    }
}