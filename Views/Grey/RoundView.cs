using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart.Models;
using FlowChart.Utility;
using System.Drawing.Drawing2D;

namespace FlowChart.Views.Grey
{
    public class GreyRoundView : RoundView
    {
        public override void Draw(Graphics g)
        {
            RectangleF rect = this.rectComponent.TopLeftCorner.MakeRectangleFTill(this.rectComponent.BottomRightCorner);

            using (LinearGradientBrush brush =
                new LinearGradientBrush(rect, ViewFactory.GradStartColor, ViewFactory.GradEndColor, 90.0f))
            {
                g.FillEllipse(brush, rect);
                g.DrawEllipse(ViewFactory.BorderPen, rect.X, rect.Y, rect.Width, rect.Height);

                base.Draw(g);
            }  
        }
    }
}
