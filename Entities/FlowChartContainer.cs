using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowChart.Entities
{
    public class FlowChartContainer
    {
        public string DisplayName { get; set; }
        public List<FlowChartComponent> Items { get; set; }
        public FlowChartContainer()
        {
            Items = new List<FlowChartComponent>();
        }        
    }
}
