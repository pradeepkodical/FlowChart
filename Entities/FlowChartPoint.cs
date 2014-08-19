using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlowChart.Entities
{
    public class FlowChartPoint
    {
        private float x;
        private float y;
        public event Action<float, float> OnChange;
        public float X 
        {
            get
            {
                return x;
            }
            set 
            {
                float old = x;
                x = value;
                if (OnChange != null)
                {
                    OnChange(old, Y);
                }
            } 
        }
        public float Y 
        {
            get
            {
                return y;
            }
            set
            {
                float old = y;
                y = value;
                if (OnChange != null)
                {
                    OnChange(X,old);
                }
            } 
        }
        public FlowChartPoint()
        {
        }
        public FlowChartPoint(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public FlowChartPoint Add(FlowChartPoint another)
        {
            this.X += another.X;
            this.Y += another.Y;
            return this;
        }

        public FlowChartPoint Subtract(FlowChartPoint another)
        {
            this.X -= another.X;
            this.Y -= another.Y;
            return this;
        }

        public FlowChartPoint CloneAndSubtract(FlowChartPoint another)
        {
            return new FlowChartPoint(X - another.X, Y - another.Y);
        }

        public FlowChartPoint CloneAndAdd(FlowChartPoint another)
        {
            return new FlowChartPoint(X + another.X, Y + another.Y);
        }

        public FlowChartPoint CloneAndAdd(int dX, int dY)
        {
            return new FlowChartPoint(X+dX, Y+dY);            
        }
        public FlowChartPoint CloneAndAdd(float dX, float dY)
        {
            return new FlowChartPoint(X + dX, Y + dY);
        }

        internal Rectangle MakeRectangleTill(FlowChartPoint endPoint)
        {
            return new Rectangle((int)X, (int)Y, (int)(endPoint.X - X), (int)(endPoint.Y - Y));
        }
        internal RectangleF MakeRectangleFTill(FlowChartPoint endPoint)
        {
            return new RectangleF(X, Y, endPoint.X - X, endPoint.Y - Y);
        }
        internal PointF MakePointF()
        {
            return new PointF(X, Y);
        }

        internal PointF[] MakePointFArrayWith(FlowChartPoint endPoint)
        {
            return new PointF[] { 
                new PointF(X, Y),
                new PointF(endPoint.X, Y),
                new PointF(endPoint.X, endPoint.Y),
                new PointF(X, endPoint.Y)
            };
        }
        internal FlowChartPoint[] MakeFlowChartPointArrayWith(FlowChartPoint endPoint)
        {
            return new FlowChartPoint[] { 
                new FlowChartPoint(X, Y),
                new FlowChartPoint(endPoint.X, Y),
                new FlowChartPoint(endPoint.X, endPoint.Y),
                new FlowChartPoint(X, endPoint.Y)
            };
        }
    }
}
