using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Models;

namespace FlowChart.Visitors
{
    public class BoxMoveVisitor:BaseVisitor
    {
        private BaseBoxComponent box;
        private bool recompute;
        public BoxMoveVisitor(BaseBoxComponent box, bool recompute)
        {
            this.box = box;
            this.recompute = recompute;
        }
        public override void Visit(LineComponent cmp)
        {
            BoxMoved(cmp);
        }
        public override void Visit(CurvedLineComponent cmp)
        {
            BoxMoved(cmp);
        }        
        void BoxMoved(BaseLineComponent lineCmp)
        {
            if (lineCmp.ConnectionStart == box || lineCmp.ConnectionEnd == box)
            {
                lineCmp.OnParentMoved(box, recompute);
            }            
        }
    }
}
