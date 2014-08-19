using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Models;
using FlowChart.Entities;
using FlowChart.Utility;

namespace FlowChart.Visitors
{
    public class LinePointMovedVisitor:BaseVisitor
    {
        private BaseLineComponent lineCmp;
        private FlowChartPoint linePoint;
        public LinePointMovedVisitor(BaseLineComponent lineCmp, FlowChartPoint linePoint)
        {
            this.lineCmp = lineCmp;
            this.linePoint = linePoint;
        }
        public override void Visit(RectangleComponent cmp)
        {
            LinePointMoved(cmp);
        }
        public override void Visit(RhombusComponent cmp)
        {
            LinePointMoved(cmp);
        }
        public override void Visit(RoundComponent cmp)
        {
            LinePointMoved(cmp);
        }
        public override void Visit(DatabaseComponent cmp)
        {
            LinePointMoved(cmp);
        }        
        
        void LinePointMoved(BaseBoxComponent cmp)
        {   
            foreach (FlowChartPoint edgePoint in cmp.EdgePoints)
            {
                float d = GraphicsUtil.Distance(edgePoint, linePoint);
                cmp.IsSelected = (d < cmp.View.ViewFactory.EdgeBoxWidth * 5);
                if (d < cmp.View.ViewFactory.EdgeBoxWidth * 2)
                {                    
                    linePoint.X = edgePoint.X;
                    linePoint.Y = edgePoint.Y;
                    if (lineCmp.StartPoint == linePoint)
                    {
                        lineCmp.ConnectionStart = cmp;
                        lineCmp.ConnectionStartPointIndex = cmp.EdgePoints.IndexOf(edgePoint);
                    }
                    else if (lineCmp.EndPoint == linePoint)
                    {
                        lineCmp.ConnectionEnd = cmp;
                        lineCmp.ConnectionEndPointIndex = cmp.EdgePoints.IndexOf(edgePoint);
                    }
                    return;
                }
            }            
        }
    }
}
