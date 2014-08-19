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
    public class DatabaseComponent:BaseBoxComponent
    {
        #region Constructor
        private float offSet = 16;
        public DatabaseComponent()
        {            
            this.ImageIndex = 5;
            for (int i = 0; i < 6; i++)
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
            this.EdgePoints[0].Y = TopLeftCorner.Y + offSet;

            this.EdgePoints[1].X = BottomRightCorner.X;
            this.EdgePoints[1].Y = TopLeftCorner.Y + this.Height / 3;

            this.EdgePoints[2].X = BottomRightCorner.X;
            this.EdgePoints[2].Y = TopLeftCorner.Y + 2 * this.Height / 3;

            this.EdgePoints[3].X = TopLeftCorner.X + this.Width / 2;
            this.EdgePoints[3].Y = BottomRightCorner.Y;

            this.EdgePoints[4].X = TopLeftCorner.X;
            this.EdgePoints[4].Y = TopLeftCorner.Y + this.Height / 3;

            this.EdgePoints[5].X = TopLeftCorner.X;
            this.EdgePoints[5].Y = TopLeftCorner.Y + 2 * this.Height / 3;
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
