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
    public class GreyRhombusView : RhombusView
    {
        public override void Draw(Graphics g)
        {
            coordinates[0].X = this.rectComponent.TopLeftCorner.X + this.rectComponent.Width / 2;
            coordinates[0].Y = this.rectComponent.TopLeftCorner.Y;

            coordinates[1].X = this.rectComponent.TopLeftCorner.X + this.rectComponent.Width;
            coordinates[1].Y = this.rectComponent.TopLeftCorner.Y + this.rectComponent.Height / 2;

            coordinates[2].X = this.rectComponent.TopLeftCorner.X + this.rectComponent.Width / 2;
            coordinates[2].Y = this.rectComponent.TopLeftCorner.Y + this.rectComponent.Height;

            coordinates[3].X = this.rectComponent.TopLeftCorner.X;
            coordinates[3].Y = this.rectComponent.TopLeftCorner.Y + this.rectComponent.Height / 2;

            RectangleF rect = this.rectComponent.TopLeftCorner.MakeRectangleFTill(this.rectComponent.BottomRightCorner);

            using (LinearGradientBrush brush =
                new LinearGradientBrush(rect, ViewFactory.GradStartColor, ViewFactory.GradEndColor, 90.0f))
            {
                g.FillPolygon(brush, coordinates);
                g.DrawPolygon(ViewFactory.BorderPen, coordinates);
                base.Draw(g);
            }
        }
    }
}
