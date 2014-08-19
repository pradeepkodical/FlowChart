using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Factory;
using System.Drawing;

namespace FlowChart.Views.Grey
{
    public class GreyViewFactory : ViewAbstractFactory
    {
        static readonly Pen boundingBoxPen = new Pen(Color.Black, 1);
        static readonly Pen linePen = new Pen(Color.Black, 2);

        static readonly Pen selectedPen = new Pen(Color.Blue, 2);

        static readonly Pen borderPen = new Pen(Color.Black, 1);
        static readonly Pen textPen = new Pen(Color.Black, 2);
        static readonly Pen labelPen = new Pen(Color.Black, 2);
        static readonly Pen edgePen = new Pen(Color.Black, 1);
        static readonly Pen resizePen = new Pen(Color.Gray, 2);

        static readonly Brush arrowBrush = Brushes.Black;
        static readonly Pen arrowPen = new Pen(Color.Black, 1);

        static readonly float edgeBoxWidth = 8.0f;
        static readonly float resizeBoxWidth = 16.0f;
        static readonly float arrowLength = 20.0f;


        public override Pen BoundingBoxPen { get { return boundingBoxPen; } }
        public override Pen LinePen { get { return linePen; } }

        public override Pen SelectedPen { get { return selectedPen; } }

        public override Pen BorderPen { get { return borderPen; } }
        public override Pen TextPen { get { return textPen; } }
        public override Pen LabelPen { get { return labelPen; } }
        public override Pen EdgePen { get { return edgePen; } }
        public override Pen ResizePen { get { return resizePen; } }

        public override Brush ArrowBrush { get { return arrowBrush; } }
        public override Pen ArrowPen { get { return arrowPen; } }

        public override float EdgeBoxWidth { get { return edgeBoxWidth; } }
        public override float ResizeBoxWidth { get { return resizeBoxWidth; } }
        public override float ArrowLength { get { return arrowLength; } }

        public override Brush LabelBrush { get { return Brushes.Black; } }

        public override Color GradStartColor { get { return Color.White; } }
        public override Color GradEndColor { get { return Color.Gray; } }

        public override StraightLineView CreateStraightLineView()
        {
            return new GreyStraightLineView { ViewFactory = this };
        }

        public override CurvedLineView CreateCurvedLineView()
        {
            return new GreyCurvedLineView { ViewFactory = this };
        }

        public override DatabaseView CreateDatabaseView()
        {
            return new GreyDatabaseView { ViewFactory = this };
        }

        public override RectangleView CreateRectangleView()
        {
            return new GreyRectangleView { ViewFactory = this };
        }

        public override RhombusView CreateRhombusView()
        {
            return new GreyRhombusView { ViewFactory = this };
        }

        public override RoundView CreateRoundView()
        {
            return new GreyRoundView { ViewFactory = this };
        }
    }
}
