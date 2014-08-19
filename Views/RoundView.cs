using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart.Models;
using FlowChart.Utility;
using System.Drawing.Drawing2D;

namespace FlowChart.Views
{
    public class RoundView: BaseBoxView
    {
        protected RoundComponent rectComponent;
        public void SetComponent(RoundComponent rectComponent)
        {
            this.rectComponent = rectComponent;
            this.Component = rectComponent;
            this.rectComponent.View = this;
        }
    }
}
