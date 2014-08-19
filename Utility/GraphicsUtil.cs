using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart.Entities;

namespace FlowChart.Utility
{
    internal class GraphicsUtil
    {
        #region Helpers       

        internal static float Distance(float x1, float y1, float x2, float y2)
        {
            float x = x2 - x1;
            float y = y2 - y1;
            return (float)Math.Sqrt((float)(x * x + y * y));
        }
        
        internal static float Distance(FlowChartPoint p1, float x, float y)
        {
            return Distance(p1.X, p1.Y, x, y);
        }

        internal static float Distance(FlowChartPoint p1, FlowChartPoint p2)
        {
            return Distance(p1, p2.X, p2.Y);
        }

        internal static bool HasPoint(FlowChartPoint[] points, float testx, float testy)
        {
            int i, j;
            bool c = false;
            for (i = 0, j = points.Length - 1; i < points.Length; j = i++)
            {
                if (((points[i].Y > testy) != (points[j].Y > testy)) &&
                 (testx < (points[j].X - points[i].X) * (testy - points[i].Y) / (points[j].Y - points[i].Y) + points[i].X))
                    c = !c;
            }
            return c;
        }

        internal static float DistanceToLine(FlowChartPoint StartPoint, FlowChartPoint EndPoint, int x, int y)
        {
            float A = x - StartPoint.X;
            float B = y - StartPoint.Y;
            float C = EndPoint.X - StartPoint.X;
            float D = EndPoint.Y - StartPoint.Y;

            return (float)(Math.Abs(A * D - C * B) / Math.Sqrt(C * C + D * D));
        }
        #endregion
    }
}
