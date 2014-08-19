using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowChart.Entities
{
    public class FlowChartComponent
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public List<FlowChartReference> ConnectionIds { get; set; }
        public List<FlowChartPoint> Points { get; set; }
        public FlowChartComponent()
        {
            this.Points = new List<FlowChartPoint>();
            this.ConnectionIds = new List<FlowChartReference>();
        }
    }
}
