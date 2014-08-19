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
    public abstract class BaseLineComponent:BaseComponent
    {
        #region Events
        public event Action<BaseLineComponent, FlowChartPoint> LinePointMovedTo;
        #endregion

        #region Properties
        [TypeConverter(typeof(FlowChartPointConverter))]
        [DescriptionAttribute("Start Point of the line")]
        public FlowChartPoint StartPoint { get; set; }

        [TypeConverter(typeof(FlowChartPointConverter))]
        [DescriptionAttribute("End Point of the line with Arrow")]
        public FlowChartPoint EndPoint { get; set; }

        [Browsable(false)]
        public BaseBoxComponent ConnectionStart { get; set; }
        [Browsable(false)]
        public BaseBoxComponent ConnectionEnd { get; set; }
        [Browsable(false)]
        public int ConnectionStartPointIndex { get; set; }
        [Browsable(false)]
        public int ConnectionEndPointIndex { get; set; }

        [Browsable(false)]
        public bool ParentMoving{ get; set; }
        #endregion

        #region Constructor
        public BaseLineComponent()            
        {
            this.SortOrder = 5;
        }

        public override void Init()
        {            
        }
        #endregion

        #region Virtual
        public virtual void RecomputePoints()
        { 
            FlowChartPoint pt = GetWeightedPoint();
            this.CenterPoint.X = pt.X;
            this.CenterPoint.Y = pt.Y;            
        }
        public virtual FlowChartPoint GetWeightedPoint()
        {
            return StartPoint.CloneAndAdd((EndPoint.X - StartPoint.X) / 2.0f, (EndPoint.Y - StartPoint.Y) / 2.0f);
        }

        public virtual void OnParentMoved(BaseBoxComponent box, bool recompute)
        {
            if (this.ConnectionStart == box)
            {
                this.StartPoint.X = box.EdgePoints[this.ConnectionStartPointIndex].X;
                this.StartPoint.Y = box.EdgePoints[this.ConnectionStartPointIndex].Y;
                this.ParentMoving = !recompute;
                this.RecomputePoints();
            }
            if (this.ConnectionEnd == box)
            {
                this.EndPoint.X = box.EdgePoints[this.ConnectionEndPointIndex].X;
                this.EndPoint.Y = box.EdgePoints[this.ConnectionEndPointIndex].Y;
                this.ParentMoving = !recompute;
                this.RecomputePoints();
            }
        }
        public override bool HitTest(int x, int y)
        {
            if (GraphicsUtil.Distance(StartPoint, (float)x, (float)y) < View.ViewFactory.EdgeBoxWidth / 2)
            {
                this.SelectedPoint = StartPoint;
                this.MouseState = Entities.MouseState.Resize;
                return true;
            }
            if (GraphicsUtil.Distance(EndPoint, (float)x, (float)y) < View.ViewFactory.EdgeBoxWidth / 2)
            {
                this.SelectedPoint = EndPoint;
                this.MouseState = Entities.MouseState.Resize;
                return true;
            }            
            return false;
        }

        public override FlowChartComponent GetComponent()
        {
            FlowChartComponent component = new FlowChartComponent();
            component.ID = this.ID;
            component.Text = Text;
            component.Type = this.GetType().FullName;
            component.Points.Add(StartPoint);
            component.Points.Add(EndPoint);

            FlowChartReference fcRef1 = new FlowChartReference
            {
                ID = ConnectionStart != null ? ConnectionStart.ID : string.Empty,
                Name = "Start"
            };
            FlowChartReference fcRef2 = new FlowChartReference
            {
                ID = ConnectionEnd != null ? ConnectionEnd.ID : string.Empty,
                Name = "End"
            };
            component.ConnectionIds.Add(fcRef1);
            component.ConnectionIds.Add(fcRef2);

            if (ConnectionStart != null)
            {
                 fcRef1.Key1 = ConnectionStartPointIndex;
            }
            if (ConnectionEnd != null)
            {
                fcRef2.Key1 = ConnectionEndPointIndex;
            }
            return component;
        }

        public override void SetComponent(FlowChartComponent component)
        {
            ID = component.ID;
            Text = component.Text;
            StartPoint = component.Points[0];
            EndPoint = component.Points[1];
        }
        
        public override void MouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            base.MouseUp(e);
            this.OnChanged();
        }
        public override void MouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (this.SelectedPoint != null)
            {
                this.SelectedPoint.X = e.X;
                this.SelectedPoint.Y = e.Y;
                if (this.LinePointMovedTo != null)
                {
                    this.LinePointMovedTo(this, this.SelectedPoint);
                }                
            }
        }

        public override void Move(System.Windows.Forms.MouseEventArgs e)
        {
            if (this.SelectedPoint != null)
            {
                StartPoint.X += e.X - this.SelectedPoint.X;
                StartPoint.Y += e.Y - this.SelectedPoint.Y;

                EndPoint.X += e.X - this.SelectedPoint.X;
                EndPoint.Y += e.Y - this.SelectedPoint.Y;
                
                this.SelectedPoint.X = e.X;
                this.SelectedPoint.Y = e.Y;                
            }            
        }

        #endregion
        
    }
}
