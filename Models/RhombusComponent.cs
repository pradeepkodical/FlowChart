using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using FlowChart.Entities;
using FlowChart.Utility;
using FlowChart.Views;

namespace FlowChart.Models
{
    public class RhombusComponent:BaseBoxComponent
    {
        private PointF[] coordinates = new PointF[4];
        #region Constructor
        public RhombusComponent()
        {
            this.ImageIndex = 2;
            for (int i = 0; i < 4; i++)
            {
                this.EdgePoints.Add(new FlowChartPoint());
            }
        }
        #endregion

        #region Virtual
        public override void Accept(Visitors.BaseVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        public override void RecomputeEdgePoints()
        {
            this.EdgePoints[0].X = TopLeftCorner.X + this.Width / 2;
            this.EdgePoints[0].Y = TopLeftCorner.Y;

            this.EdgePoints[1].X = BottomRightCorner.X;
            this.EdgePoints[1].Y = TopLeftCorner.Y + this.Height / 2;

            this.EdgePoints[2].X = TopLeftCorner.X + this.Width / 2;
            this.EdgePoints[2].Y = BottomRightCorner.Y;

            this.EdgePoints[3].X = TopLeftCorner.X;
            this.EdgePoints[3].Y = TopLeftCorner.Y + this.Height / 2;
        }

        public override FlowChartComponent GetComponent()
        {
            FlowChartComponent component = new FlowChartComponent();
            component.ID = this.ID;
            component.Text = Text;
            component.Type = this.GetType().FullName;
            component.Points.Add(TopLeftCorner);
            component.Points.Add(BottomRightCorner);            
            return component;
        }

        public override void SetComponent(FlowChartComponent component)
        {
            ID = component.ID;
            Text = component.Text;
            TopLeftCorner = component.Points[0];
            BottomRightCorner = component.Points[1];
        }

        #endregion
    }
}
