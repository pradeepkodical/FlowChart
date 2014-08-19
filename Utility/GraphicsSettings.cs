using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlowChart.Utility
{
    class GraphicsSettings
    {
        public static readonly Pen BoundingBoxPen = new Pen(Color.Blue, 1);
        public static readonly Pen LinePen = new Pen(Color.DarkSlateBlue, 2);

        public static readonly Pen SelectedPen = new Pen(Color.OrangeRed, 2);

        public static readonly Pen BorderPen = new Pen(Color.Black, 2);
        public static readonly Pen TextPen = new Pen(Color.Black, 2);
        public static readonly Pen LabelPen = new Pen(Color.Red, 2);
        public static readonly Pen EdgePen = new Pen(Color.RoyalBlue, 1);
        public static readonly Pen ResizePen = new Pen(Color.SandyBrown, 2);

        public static float EdgeBoxWidth = 16.0f;
        public static float ResizeBoxWidth = 16.0f;
    }
}
