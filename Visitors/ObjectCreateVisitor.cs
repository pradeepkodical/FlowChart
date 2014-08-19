using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Models;
using FlowChart.Controller;
using FlowChart.Entities;

namespace FlowChart.Visitors
{
    public class ObjectCreateVisitor:BaseVisitor
    {
        private FlowChartModel model;
        private FlowChartComponent component;
        public ObjectCreateVisitor(FlowChartModel model, FlowChartComponent component)
        {
            this.model = model;
            this.component = component;
        }
        public override void Visit(CurvedLineComponent cmp)
        {
            HookToParentBox(cmp);
        }
        public override void Visit(LineComponent cmp)
        {
            HookToParentBox(cmp);
        }

        private void HookToParentBox(BaseLineComponent c)
        {
            FlowChartReference fRef1 = component.ConnectionIds.Find(y => y.Name == "Start");
            if (fRef1 != null)
            {
                c.ConnectionStart = (BaseBoxComponent)this.model.Items.Find(y => y.ID == fRef1.ID);
            }
            FlowChartReference fRef2 = component.ConnectionIds.Find(y => y.Name == "End");
            if (fRef2 != null)
            {
                c.ConnectionEnd = (BaseBoxComponent)this.model.Items.Find(y => y.ID == fRef2.ID);
            }
            
            c.ConnectionStartPointIndex = fRef1.Key1;
            c.ConnectionEndPointIndex = fRef2.Key1;            
        }
    }
}
