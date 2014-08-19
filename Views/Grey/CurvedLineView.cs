using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart.Models;
using FlowChart.Utility;

namespace FlowChart.Views.Grey
{
    public class GreyCurvedLineView : CurvedLineView
    {
        public override void Draw(Graphics g)
        {
            if (this.lineComponent.IsSelected)
            {
                g.DrawLine(ViewFactory.BoundingBoxPen, this.lineComponent.StartPoint.X, this.lineComponent.StartPoint.Y, this.lineComponent.ControlPoint1.X, this.lineComponent.ControlPoint1.Y);
                g.DrawLine(ViewFactory.BoundingBoxPen, this.lineComponent.EndPoint.X, this.lineComponent.EndPoint.Y, this.lineComponent.ControlPoint2.X, this.lineComponent.ControlPoint2.Y);
            }
            if (this.lineComponent.ParentMoving)
            {
                g.DrawBezier(ViewFactory.LinePen,
                    this.lineComponent.StartPoint.MakePointF(),
                    this.lineComponent.ControlPoint1.MakePointF(),
                    this.lineComponent.ControlPoint2.MakePointF(),
                    this.lineComponent.EndPoint.MakePointF());
            }
            else
            {
                g.DrawLines(ViewFactory.LinePen, this.lineComponent.points.ToArray());
                /*DrawPoint(g, new FlowChartPoint(points[points.Count/2].X,points[points.Count/2].Y), 
                    GraphicsSettings.LabelPen, GraphicsSettings.EdgeBoxWidth);*/
            }

            DrawArrow(g, this.lineComponent.EndPoint, this.lineComponent.ControlPoint2,
                ViewFactory.ArrowBrush,
                ViewFactory.ArrowPen,
                ViewFactory.ArrowLength);

            if (this.lineComponent.IsSelected)
            {
                DrawPoint(g, this.lineComponent.ControlPoint1, ViewFactory.EdgePen, ViewFactory.EdgeBoxWidth);
                DrawPoint(g, this.lineComponent.ControlPoint2, ViewFactory.EdgePen, ViewFactory.EdgeBoxWidth);
            }
            base.Draw(g);
        }
    }
}
