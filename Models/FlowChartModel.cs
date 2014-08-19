using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowChart.Models
{
    public class FlowChartModel
    {
        public List<BaseComponent> Items { get; set; }
        public FlowChartModel()
        {
            this.Items = new List<BaseComponent>();
        }        
    }
}
