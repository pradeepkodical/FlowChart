using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart.Factory;

namespace FlowChart.Visitors
{
    class ViewDirectorVisitor:BaseVisitor
    {
        private Font font;
        private ViewAbstractFactory ViewFactory;
        public ViewDirectorVisitor(ViewAbstractFactory ViewFactory, Font font)
        {
            this.ViewFactory = ViewFactory;
            this.font = font;
        }
        public override void Visit(Models.CurvedLineComponent cmp)
        {
            this.ViewFactory.CreateCurvedLineView().SetComponent(cmp);
            cmp.View.Font = font;
        }
        public override void Visit(Models.DatabaseComponent cmp)
        {
            this.ViewFactory.CreateDatabaseView().SetComponent(cmp);
            cmp.View.Font = font;
        }
        public override void Visit(Models.LineComponent cmp)
        {
            this.ViewFactory.CreateStraightLineView().SetComponent(cmp);
            cmp.View.Font = font;
        }
        public override void Visit(Models.RectangleComponent cmp)
        {
            this.ViewFactory.CreateRectangleView().SetComponent(cmp);
            cmp.View.Font = font;
        }
        public override void Visit(Models.RhombusComponent cmp)
        {
            this.ViewFactory.CreateRhombusView().SetComponent(cmp);
            cmp.View.Font = font;
        }
        public override void Visit(Models.RoundComponent cmp)
        {
            this.ViewFactory.CreateRoundView().SetComponent(cmp);
            cmp.View.Font = font;
        }
    }
}
