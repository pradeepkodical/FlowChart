using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart.Models;
using FlowChart.Utility;

namespace FlowChart.Views.Grey
{
    public class GreyStraightLineView : StraightLineView
    {
        public override void Draw(Graphics g)
        {
            g.DrawLine(ViewFactory.LinePen,
                this.lineComponent.StartPoint.X,
                this.lineComponent.StartPoint.Y,
                this.lineComponent.EndPoint.X,
                this.lineComponent.EndPoint.Y);
            DrawArrow(g, this.lineComponent.EndPoint, this.lineComponent.StartPoint,
                ViewFactory.ArrowBrush,
                ViewFactory.ArrowPen,
                ViewFactory.ArrowLength);
            base.Draw(g);
        }
    }
}
