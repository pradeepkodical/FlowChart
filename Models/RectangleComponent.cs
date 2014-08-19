using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using FlowChart.Entities;
using FlowChart.Utility;
using FlowChart.Views;

namespace FlowChart.Models
{
    public class RectangleComponent: BaseBoxComponent
    {
        #region Constructor
        public RectangleComponent()
        {
            this.ImageIndex = 0;
            for (int i = 0; i < 12; i++)
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
            int i = 0;
            //Top Side
            this.EdgePoints[i].X = TopLeftCorner.X + this.Width * 0.25f;
            this.EdgePoints[i++].Y = TopLeftCorner.Y;

            this.EdgePoints[i].X = TopLeftCorner.X + this.Width * 0.5f;
            this.EdgePoints[i++].Y = TopLeftCorner.Y;

            this.EdgePoints[i].X = TopLeftCorner.X + this.Width * 0.75f;
            this.EdgePoints[i++].Y = TopLeftCorner.Y;

            //Right Side
            this.EdgePoints[i].X = BottomRightCorner.X;
            this.EdgePoints[i++].Y = TopLeftCorner.Y + this.Height * 0.25f;

            this.EdgePoints[i].X = BottomRightCorner.X;
            this.EdgePoints[i++].Y = TopLeftCorner.Y + this.Height * 0.5f;

            this.EdgePoints[i].X = BottomRightCorner.X;
            this.EdgePoints[i++].Y = TopLeftCorner.Y + this.Height * 0.75f;

            //Bottom Side     
            this.EdgePoints[i].X = TopLeftCorner.X + this.Width * 0.25f;
            this.EdgePoints[i++].Y = BottomRightCorner.Y;

            this.EdgePoints[i].X = TopLeftCorner.X + this.Width * 0.5f;
            this.EdgePoints[i++].Y = BottomRightCorner.Y;

            this.EdgePoints[i].X = TopLeftCorner.X + this.Width * 0.75f;
            this.EdgePoints[i++].Y = BottomRightCorner.Y;

            //Left Side
            this.EdgePoints[i].X = TopLeftCorner.X;
            this.EdgePoints[i++].Y = TopLeftCorner.Y + this.Height * 0.25f;
            
            this.EdgePoints[i].X = TopLeftCorner.X;
            this.EdgePoints[i++].Y = TopLeftCorner.Y + this.Height * 0.5f;

            this.EdgePoints[i].X = TopLeftCorner.X;
            this.EdgePoints[i++].Y = TopLeftCorner.Y + this.Height * 0.75f;
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
