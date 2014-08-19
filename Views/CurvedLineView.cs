using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart.Models;
using FlowChart.Utility;

namespace FlowChart.Views
{
    public class CurvedLineView: BaseLineView
    {
        protected CurvedLineComponent lineComponent;
        public void SetComponent(CurvedLineComponent lineComponent)
        {
            this.lineComponent = lineComponent;
            this.Component = lineComponent;
            this.lineComponent.View = this;
        }        
    }
}
