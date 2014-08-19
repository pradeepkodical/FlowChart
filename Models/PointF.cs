using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowChart.Models
{
    public class ChartPoint
    {
        public float X { get; set; }
        public float Y { get; set; }
        public ChartPoint()
        { 
        }
        public ChartPoint(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
