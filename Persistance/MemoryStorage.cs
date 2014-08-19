using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Entities;
using FlowChart.Utility;
using FlowChart.Controller;
using FlowChart.Models;

namespace FlowChart.Persistance
{
    internal class MemoryStorage : BaseStorage
    {
        private List<FlowChartContainer> UndoBuffers = new List<FlowChartContainer>();

        internal override FlowChartContainer Load()
        {
            return this.UndoBuffers[0];
        }
        internal FlowChartContainer LoadAt(int index)
        {
            return this.UndoBuffers[index];
        }
        internal override void Save(FlowChartModel model)
        {

            FlowChartContainer newContent = Util.ConvertFromJSON<FlowChartContainer>(Util.GetJSONString(GetContent(model)));
            FlowChartContainer container = UndoBuffers.LastOrDefault();
            if (container == null)
            {
                newContent.DisplayName = DateTime.Now.ToString("HH:mm:ss");
                UndoBuffers.Add(newContent);
            }
            else
            {
                string oDisplayName = container.DisplayName;
                container.DisplayName = "";
                newContent.DisplayName = "";

                if (Util.GetJSONString(newContent) != Util.GetJSONString(container))
                {
                    newContent.DisplayName = DateTime.Now.ToString("HH:mm:ss");
                    UndoBuffers.Add(newContent);
                    if (UndoBuffers.Count > 10)
                    {
                        UndoBuffers.RemoveAt(0);
                    }
                }
                container.DisplayName = oDisplayName;
            }            
        }

        internal List<FlowChartContainer> GetUndoBuffers()
        {
            return UndoBuffers;
        }
    }
}
