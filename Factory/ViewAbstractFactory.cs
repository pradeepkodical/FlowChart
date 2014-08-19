using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Views;
using System.Drawing;

namespace FlowChart.Factory
{
    public abstract class ViewAbstractFactory
    {
        public abstract Color GradStartColor { get; }
        public abstract Color GradEndColor { get; }

        public abstract Pen BoundingBoxPen { get; }
        public abstract Pen LinePen { get; }

        public abstract Pen SelectedPen { get; }

        public abstract Pen BorderPen { get; }
        public abstract Pen TextPen { get; }
        public abstract Pen LabelPen { get; }
        public abstract Pen EdgePen { get; }
        public abstract Pen ResizePen { get; }

        public abstract Brush LabelBrush { get; }
        public abstract Brush ArrowBrush { get; }
        public abstract Pen ArrowPen { get; }

        public abstract float EdgeBoxWidth { get; }
        public abstract float ResizeBoxWidth { get; }
        public abstract float ArrowLength { get; }

        public abstract StraightLineView CreateStraightLineView();
        public abstract CurvedLineView CreateCurvedLineView();
        public abstract DatabaseView CreateDatabaseView();
        public abstract RectangleView CreateRectangleView();

        public abstract RhombusView CreateRhombusView();
        public abstract RoundView CreateRoundView();
    }
}
