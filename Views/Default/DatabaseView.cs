using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart.Models;
using FlowChart.Utility;
using System.Drawing.Drawing2D;

namespace FlowChart.Views.Default
{
    public class DefaultDatabaseView : DatabaseView
    {
        public override void Draw(Graphics g)
        {
            RectangleF rect = rectComponent.TopLeftCorner.CloneAndAdd(0, offSet).MakeRectangleFTill(rectComponent.BottomRightCorner.CloneAndAdd(0, -offSet));
            RectangleF upperRect = rectComponent.TopLeftCorner.MakeRectangleFTill(rectComponent.TopLeftCorner.CloneAndAdd(rectComponent.Width, 2 * offSet));
            RectangleF lowerRect = rectComponent.BottomRightCorner.CloneAndAdd(-rectComponent.Width, -2 * offSet).MakeRectangleFTill(rectComponent.BottomRightCorner);

            using (LinearGradientBrush brush =
                new LinearGradientBrush(rect, ViewFactory.GradStartColor, ViewFactory.GradEndColor, 90.0f))
            {
                g.FillRectangle(brush, rect);
                g.DrawRectangle(ViewFactory.BorderPen, rect.X, rect.Y, rect.Width, rect.Height);
            }


            using (LinearGradientBrush brush =
                new LinearGradientBrush(upperRect, ViewFactory.GradStartColor, ViewFactory.GradEndColor, 90.0f))
            {
                g.FillEllipse(brush, upperRect);
                g.DrawEllipse(ViewFactory.BorderPen, upperRect.X, upperRect.Y, upperRect.Width, upperRect.Height);
            }

            using (LinearGradientBrush brush =
                new LinearGradientBrush(lowerRect, ViewFactory.GradStartColor, ViewFactory.GradEndColor, 90.0f))
            {
                g.FillEllipse(brush, lowerRect);
                g.DrawArc(ViewFactory.BorderPen, lowerRect, 0, 180);
            }
            base.Draw(g);
        }
    }
}
