using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Models;
using FlowChart.Controller;
using FlowChart.Entities;

namespace FlowChart.Visitors
{
    public class ObjectModifyVisitor:BaseVisitor
    {
        private FlowChartModel model;
        public ObjectModifyVisitor(FlowChartModel model)
        {
            this.model = model;
        }

        public override void Visit(LineComponent cmp)
        {
            VisitComponent(cmp);
        }
        public override void Visit(CurvedLineComponent cmp)
        {
            VisitComponent(cmp);
        }        

        public override void Visit(RectangleComponent cmp)
        {
            VisitComponent(cmp);
        }
        public override void Visit(RhombusComponent cmp)
        {
            VisitComponent(cmp);
        }
        public override void Visit(RoundComponent cmp)
        {
            VisitComponent(cmp);
        }
        public override void Visit(DatabaseComponent cmp)
        {
            VisitComponent(cmp);
        }

        public void VisitComponent(BaseLineComponent cmp)
        {
            cmp.LinePointMovedTo += new Action<BaseLineComponent, FlowChartPoint>(LinePointMovedTo);
        }
        public void VisitComponent(BaseBoxComponent cmp)
        {
            cmp.BoxComponentMoving += new Action<BaseBoxComponent>(BoxComponentMoving);
            cmp.BoxComponentMoved += new Action<BaseBoxComponent>(BoxComponentMoved);            
        }

        void LinePointMovedTo(BaseLineComponent lineCmp, FlowChartPoint point)
        {
            if (lineCmp.StartPoint == point)
            {
                lineCmp.ConnectionStart = null;
            }
            else if (lineCmp.EndPoint == point)
            {
                lineCmp.ConnectionEnd = null;
            }

            foreach (BaseComponent drawableCmp in model.Items)
            {
                drawableCmp.Accept(new LinePointMovedVisitor(lineCmp, point));                
            }
        }

        void BoxComponentMoving(BaseBoxComponent obj)
        {
            foreach (BaseComponent drawableCmp in model.Items)
            {
                drawableCmp.Accept(new BoxMoveVisitor(obj, false));
            }            
        }
        void BoxComponentMoved(BaseBoxComponent obj)
        {
            foreach (BaseComponent drawableCmp in model.Items)
            {
                drawableCmp.Accept(new BoxMoveVisitor(obj, true));
            }
        }        
    }
}
