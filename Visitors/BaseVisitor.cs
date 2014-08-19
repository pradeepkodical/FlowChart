using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Models;

namespace FlowChart.Visitors
{
    public class BaseVisitor
    {
        public virtual void Visit(CurvedLineComponent cmp)
        { 
        }
        public virtual void Visit(LineComponent cmp)
        {
        }
        public virtual void Visit(RectangleComponent cmp)
        {
        }
        public virtual void Visit(RhombusComponent cmp)
        {
        }
        public virtual void Visit(RoundComponent cmp)
        {
        }
        public virtual void Visit(DatabaseComponent cmp)
        {
        }        
    }
}
