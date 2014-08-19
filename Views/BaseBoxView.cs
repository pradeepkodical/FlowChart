using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart.Models;
using FlowChart.Utility;

namespace FlowChart.Views
{
    public class BaseBoxView: BaseView
    {
        public BaseBoxComponent Component{get;set;}        
        public override void Draw(Graphics g)
        {
            DrawText(g, this.Font, this.Component.Text, ViewFactory.LabelBrush, this.Component.TopLeftCorner.MakeRectangleTill(this.Component.BottomRightCorner));

            if (this.Component.IsSelected)
            {
                DrawBoundingBox(g, this.Component.TopLeftCorner, this.Component.BottomRightCorner);

                DrawResizePoint(g, this.Component.TopLeftCorner, ViewFactory.ResizePen, ViewFactory.ResizeBoxWidth);
                DrawResizePoint(g, this.Component.BottomRightCorner, ViewFactory.ResizePen, ViewFactory.ResizeBoxWidth);

                if (this.Component.SelectedPoint != null)
                {
                    Draw4Arrows(g, this.Component.SelectedPoint, ViewFactory.ArrowLength);
                }
                this.Component.EdgePoints.ForEach(x =>
                {
                    DrawPoint(g, x, ViewFactory.EdgePen, ViewFactory.EdgeBoxWidth);
                });
            }
        }
    }
}
