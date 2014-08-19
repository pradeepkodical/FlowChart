using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart.Entities;
using FlowChart.Utility;
using FlowChart.Factory;

namespace FlowChart.Views
{
    public abstract class BaseView
    {
        public ViewAbstractFactory ViewFactory { get; set; }
        public Font Font { get; set; }        

        public abstract void Draw(Graphics g);

        protected void DrawText(Graphics g, Font font, string text, Brush brush, Rectangle rect)
        {
            StringFormat sf = new StringFormat(StringFormat.GenericDefault);
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(text, font, brush, rect, sf);
        }

        protected void DrawText(Graphics g, Font font, string text, Brush brush, FlowChartPoint point)
        {
            StringFormat sf = new StringFormat(StringFormat.GenericDefault);
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(text, font, brush, point.X, point.Y, sf);
        }
        protected void Draw4Arrows(Graphics g, FlowChartPoint p, float length)
        {
            /*   ^
             * <-|->
             *   v
             */

            DrawArrow(g, p.CloneAndAdd(0, -length), p, Brushes.YellowGreen, Pens.Red, length);
            DrawArrow(g, p.CloneAndAdd(0, length), p, Brushes.YellowGreen, Pens.Red, length);
            DrawArrow(g, p.CloneAndAdd(-length, 0), p, Brushes.YellowGreen, Pens.Red, length);
            DrawArrow(g, p.CloneAndAdd(length, 0), p, Brushes.YellowGreen, Pens.Red, length);
        }

        protected void DrawPoint(Graphics g, FlowChartPoint p, Pen pen, float width, bool fill)
        {
            if (fill)
            {
                g.FillRectangle(Brushes.AliceBlue, p.X - width / 2, p.Y - width / 2, width, width);
            }
            g.DrawRectangle(pen, p.X - width / 2, p.Y - width / 2, width, width);
        }

        protected void DrawPoint(Graphics g, FlowChartPoint p, Pen pen, float width)
        {
            DrawPoint(g, p, pen, width, false);
        }

        protected void DrawResizePoint(Graphics g, FlowChartPoint p, Pen pen, float width)
        {
            Draw4Arrows(g, p, 10);
        }

        protected void DrawBoundingBox(Graphics g,
            FlowChartPoint startPoint,
            FlowChartPoint endPoint)
        {
            g.DrawPolygon(ViewFactory.BoundingBoxPen, startPoint.MakePointFArrayWith(endPoint));
        }
        protected void DrawBoundingBox(Graphics g,
            FlowChartPoint p1,
            FlowChartPoint p2,
            FlowChartPoint p3,
            FlowChartPoint p4)
        {
            g.DrawPolygon(
                ViewFactory.BoundingBoxPen,
                new PointF[] 
                { 
                    p1.MakePointF(), 
                    p2.MakePointF(), 
                    p3.MakePointF(), 
                    p4.MakePointF()
                });
        }

        protected void DrawArrow(Graphics g, FlowChartPoint dst, FlowChartPoint src, Brush b, Pen p, float length)
        {
            float angle = (float)Math.Atan2(dst.Y - src.Y, dst.X - src.X);
            float x1 = dst.X - (float)(0.5 * length * Math.Cos(angle + Math.PI / 6.0f));
            float y1 = dst.Y - (float)(0.5 * length * Math.Sin(angle + Math.PI / 6.0f));

            float x2 = dst.X - (float)(0.5 * length * Math.Cos(angle - Math.PI / 6.0f));
            float y2 = dst.Y - (float)(0.5 * length * Math.Sin(angle - Math.PI / 6.0f));

            g.FillPolygon(b, new PointF[] { 
                new PointF(dst.X, dst.Y),
                new PointF(x1, y1),
                new PointF(x2, y2)
            });
            g.DrawPolygon(p, new PointF[] { 
                new PointF(dst.X, dst.Y),
                new PointF(x1, y1),
                new PointF(x2, y2)
            });
        }
    }
}
