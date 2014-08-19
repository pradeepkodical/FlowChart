using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Factory;
using System.Drawing;

namespace FlowChart.Views.Default
{
    public class DefaultViewFactory : ViewAbstractFactory
    {
        static readonly Pen boundingBoxPen = new Pen(Color.Blue, 1);
        static readonly Pen linePen = new Pen(Color.DarkSlateBlue, 2);

        static readonly Pen selectedPen = new Pen(Color.OrangeRed, 2);

        static readonly Pen borderPen = new Pen(Color.Black, 1);
        static readonly Pen textPen = new Pen(Color.Black, 2);
        static readonly Pen labelPen = new Pen(Color.Red, 2);
        static readonly Pen edgePen = new Pen(Color.RoyalBlue, 1);
        static readonly Pen resizePen = new Pen(Color.SandyBrown, 2);

        static readonly Brush arrowBrush = Brushes.DodgerBlue;
        static readonly Pen arrowPen = new Pen(Color.LightSteelBlue, 1);

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

        public override Brush LabelBrush { get { return Brushes.IndianRed; } }

        public override Color GradStartColor { get { return Color.White; } }
        public override Color GradEndColor { get { return Color.Wheat; } }

        public override StraightLineView CreateStraightLineView()
        {
            return new DefaultStraightLineView { ViewFactory = this};
        }

        public override CurvedLineView CreateCurvedLineView()
        {
            return new DefaultCurvedLineView { ViewFactory = this };
        }

        public override DatabaseView CreateDatabaseView()
        {
            return new DefaultDatabaseView { ViewFactory = this };
        }

        public override RectangleView CreateRectangleView()
        {
            return new DefaultRectangleView { ViewFactory = this };
        }

        public override RhombusView CreateRhombusView()
        {
            return new DefaultRhombusView { ViewFactory = this };
        }

        public override RoundView CreateRoundView()
        {
            return new DefaultRoundView { ViewFactory = this };
        }
    }
}
