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
    public class DatabaseView: BaseBoxView
    {
        protected float offSet = 16;
        protected DatabaseComponent rectComponent;        
        public void SetComponent(DatabaseComponent rectComponent)
        {
            this.rectComponent = rectComponent;
            this.Component = rectComponent;
            this.rectComponent.View = this;
        }
    }
}
