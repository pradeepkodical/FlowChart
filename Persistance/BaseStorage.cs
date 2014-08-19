using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Entities;
using FlowChart.Controller;
using FlowChart.Models;

namespace FlowChart.Persistance
{
    internal abstract class BaseStorage
    {
        internal abstract void Save(FlowChartModel model);
        internal abstract FlowChartContainer Load();
        internal virtual FlowChartContainer GetContent(FlowChartModel model)
        {
            FlowChartContainer container = new FlowChartContainer();
            model.Items.ForEach(x => container.Items.Add(x.GetComponent()));
            return container;
        }
    }
}
