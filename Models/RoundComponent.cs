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
    public class RoundComponent:BaseBoxComponent
    {
        #region Constructor
        public RoundComponent()
        {            
            this.ImageIndex = 1;
            for (int i = 0; i < 8; i++)
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
            float aradius = Math.Abs(TopLeftCorner.MakeRectangleFTill(BottomRightCorner).Width)/2;
            float bradius = Math.Abs(TopLeftCorner.MakeRectangleFTill(BottomRightCorner).Height) / 2;
            FlowChartPoint centerPoint = TopLeftCorner.CloneAndAdd(BottomRightCorner);
            centerPoint.X /= 2;
            centerPoint.Y /= 2;
            float angle = -(float)Math.PI / 2.0f;
            this.EdgePoints.ForEach(x => {
                x.X = centerPoint.X + aradius * (float)Math.Cos(angle);
                x.Y = centerPoint.Y + bradius * (float)Math.Sin(angle);
                angle += (float)Math.PI / 4.0f;
            });
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
