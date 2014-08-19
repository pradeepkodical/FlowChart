using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart.Entities;
using FlowChart.Utility;
using FlowChart.Views;

namespace FlowChart.Models
{
    public class LineComponent:BaseLineComponent
    {
        #region Virtual
        public override void Init()
        {
            this.ImageIndex = 3;
            base.Init();
        }
        public override void Accept(Visitors.BaseVisitor visitor)
        {
            visitor.Visit(this);
        }        

        public override bool HitTest(int x, int y)
        {
            if (base.HitTest(x, y))
            {
                return true;
            }
            else
            {
                if (GraphicsUtil.HasPoint(StartPoint.MakeFlowChartPointArrayWith(EndPoint), x, y))
                {
                    if (GraphicsUtil.DistanceToLine(StartPoint, EndPoint, x, y) < View.ViewFactory.EdgeBoxWidth)
                    {
                        this.MouseState = Entities.MouseState.Move;
                        this.LastHitPoint.X = x;
                        this.LastHitPoint.Y = y;
                        this.SelectedPoint = this.LastHitPoint;
                        return true;
                    }
                }
                return false;
            }            
        }
        
        #endregion
    }
}
