using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart.Models;
using FlowChart.Utility;

namespace FlowChart.Views
{
    public class BaseLineView: BaseView
    {
        public BaseLineComponent Component { get; set; }        
        public override void Draw(Graphics g)
        {
            DrawText(g, this.Font, this.Component.Text, ViewFactory.LabelBrush, this.Component.GetWeightedPoint());

            if (this.Component.IsSelected)
            {
                DrawResizePoint(g, this.Component.StartPoint, ViewFactory.ResizePen, ViewFactory.ResizeBoxWidth);
                DrawResizePoint(g, this.Component.EndPoint, ViewFactory.ResizePen, ViewFactory.ResizeBoxWidth);
                if (this.Component.SelectedPoint != null)
                {
                    Draw4Arrows(g, this.Component.SelectedPoint, ViewFactory.ArrowLength);
                }
            }
        }
    }
}
