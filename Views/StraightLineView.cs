using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart.Models;
using FlowChart.Utility;

namespace FlowChart.Views
{
    public class StraightLineView: BaseLineView
    {
        protected LineComponent lineComponent;        
        public void SetComponent(LineComponent lineComponent)
        {
            this.lineComponent = lineComponent;
            this.Component = lineComponent;
            this.lineComponent.View = this;
        }
    }
}
