using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using FlowChart.Entities;
using FlowChart.Utility;
using FlowChart.Views;

namespace FlowChart.Models
{
    public class CurvedLineComponent:BaseLineComponent
    {
        #region Properties
        [TypeConverter(typeof(FlowChartPointConverter))]
        [DescriptionAttribute("Control point 1 for the bezier curve")]
        public FlowChartPoint ControlPoint1 { get; set; }
        
        [TypeConverter(typeof(FlowChartPointConverter))]
        [DescriptionAttribute("Control point 2 for the bezier curve")]
        public FlowChartPoint ControlPoint2 { get; set; }
        #endregion

        
        #region Private
        public List<PointF> points = new List<PointF>();
        private PointF Bezier(FlowChartPoint a, FlowChartPoint b, FlowChartPoint c, FlowChartPoint d, float t)
        {
            FlowChartPoint
                ab = new FlowChartPoint(),
                bc = new FlowChartPoint(),
                cd = new FlowChartPoint(),
                abbc = new FlowChartPoint(),
                bccd = new FlowChartPoint(),
                dest = new FlowChartPoint();
            Lerp( ab,  a,  b, t);           // point between a and b (green)
            Lerp( bc,  b,  c, t);           // point between b and c (green)
            Lerp( cd,  c,  d, t);           // point between c and d (green)
            Lerp( abbc,  ab,  bc, t);       // point between ab and bc (blue)
            Lerp( bccd,  bc,  cd, t);       // point between bc and cd (blue)
            Lerp( dest,  abbc,  bccd, t);   // point on the bezier-curve (black)
            return dest.MakePointF();
        }
        void Lerp(FlowChartPoint dest, FlowChartPoint a, FlowChartPoint b, float t)
        {
            dest.X = a.X + (b.X - a.X) * t;
            dest.Y = a.Y + (b.Y - a.Y) * t;
        }
        public override void RecomputePoints()
        {
            if (!ParentMoving)
            {
                points.Clear();
                for (float t = 0; t < 1; t += 0.001f)
                {
                    points.Add(Bezier(StartPoint, ControlPoint1, ControlPoint2, EndPoint, t));
                }
            }
            base.RecomputePoints();
        }
        #endregion

        #region Virtual
        public override void Init()
        {
            RecomputePoints();
            this.ImageIndex = 4;
            base.Init();
        }
        public override void Accept(Visitors.BaseVisitor visitor)
        {            
            visitor.Visit(this);            
        }
        
        public override FlowChartComponent GetComponent()
        {
            FlowChartComponent component = base.GetComponent();
            component.Points.Add(ControlPoint1);
            component.Points.Add(ControlPoint2);
            return component;
        }

        public override FlowChartPoint GetWeightedPoint()
        {
            FlowChartPoint pt = StartPoint.CloneAndAdd(EndPoint).CloneAndAdd(ControlPoint1).CloneAndAdd(ControlPoint2);
            pt.X = pt.X / 4;
            pt.Y = pt.Y / 4;
            return pt;
        }

        public override void SetComponent(FlowChartComponent component)
        {            
            ControlPoint1 = component.Points[2];
            ControlPoint2 = component.Points[3];
            base.SetComponent(component);
        }
        public override void OnParentMoved(BaseBoxComponent box, bool recompute)
        {
            if (this.ConnectionStart == box && this.ConnectionEnd == box)
            {
                this.ControlPoint1.Add(box.EdgePoints[this.ConnectionStartPointIndex].CloneAndSubtract(this.StartPoint));
                this.ControlPoint2.Add(box.EdgePoints[this.ConnectionStartPointIndex].CloneAndSubtract(this.StartPoint));
            }
            base.OnParentMoved(box, recompute);
        }        

        public override bool HitTest(int x, int y)
        {
            if (GraphicsUtil.Distance(ControlPoint1, (float)x, (float)y) < View.ViewFactory.EdgeBoxWidth / 2)
            {
                this.SelectedPoint = ControlPoint1;
                this.MouseState = Entities.MouseState.Resize;
                return true;
            }
            if (GraphicsUtil.Distance(ControlPoint2, (float)x, (float)y) < View.ViewFactory.EdgeBoxWidth / 2)
            {
                this.SelectedPoint = ControlPoint2;
                this.MouseState = Entities.MouseState.Resize;
                return true;
            }
            if (base.HitTest(x, y))
            {
                return true;
            }
            else
            {
                if (GraphicsUtil.HasPoint(new FlowChartPoint[] { StartPoint, ControlPoint1, ControlPoint2, EndPoint }, x, y) ||
                    GraphicsUtil.HasPoint(new FlowChartPoint[] { StartPoint, ControlPoint1, EndPoint, ControlPoint2 }, x, y))
                {
                    for (int i = 0; i < this.points.Count; i++)
                    {
                        if (GraphicsUtil.Distance(x, y, this.points[i].X, this.points[i].Y) < View.ViewFactory.EdgeBoxWidth / 2)
                        {
                            this.MouseState = Entities.MouseState.Move;
                            this.LastHitPoint.X = x;
                            this.LastHitPoint.Y = y;
                            this.SelectedPoint = this.LastHitPoint;
                            return true;
                        }
                    }                    
                }
                return false;
            }            
        }
        public override void MouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            RecomputePoints();
            base.MouseMove(e);            
        }
        public override void Move(System.Windows.Forms.MouseEventArgs e)
        {
            if (this.SelectedPoint != null)
            {
                ControlPoint1.X += e.X - this.SelectedPoint.X;
                ControlPoint1.Y += e.Y - this.SelectedPoint.Y;

                ControlPoint2.X += e.X - this.SelectedPoint.X;
                ControlPoint2.Y += e.Y - this.SelectedPoint.Y;

                base.Move(e);
                RecomputePoints();
            }
        }
        #endregion
    }
}
