using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using FlowChart.Entities;
using FlowChart.Utility;
using FlowChart.Views;

namespace FlowChart.Models
{
    public abstract class BaseBoxComponent:BaseComponent
    {
        #region Private Members
        private RectangleF inflatedBox = new RectangleF();
        #endregion

        #region Properties

        [TypeConverter(typeof(FlowChartPointConverter))]
        [DescriptionAttribute("Top Left Corner Point")]
        public FlowChartPoint TopLeftCorner { get; set; }

        [TypeConverter(typeof(FlowChartPointConverter))]
        [DescriptionAttribute("Bottom Right Corner Point")]
        public FlowChartPoint BottomRightCorner { get; set; }

        [Browsable(false)]
        public List<FlowChartPoint> EdgePoints { get; private set; }

        [Browsable(false)]
        public float Width { get { return BottomRightCorner.X - TopLeftCorner.X; } }

        [Browsable(false)]
        public float Height { get { return BottomRightCorner.Y - TopLeftCorner.Y; } }
                
        #endregion

        #region Events
        public event Action<BaseBoxComponent> BoxComponentMoving;
        public event Action<BaseBoxComponent> BoxComponentMoved;
        #endregion

        #region Constructor
        public BaseBoxComponent()
        {            
            this.EdgePoints = new List<FlowChartPoint>();            
            this.SortOrder = 2;            
        }

        private void Corner_OnChange(float oldX, float oldY)
        {
            this.UpdateBoundingBox();
            if (this.BoxComponentMoving != null)
            {                
                this.BoxComponentMoving(this);
            }
        }

        public override void Init()
        {
            this.TopLeftCorner.OnChange += new Action<float, float>(Corner_OnChange);
            this.BottomRightCorner.OnChange += new Action<float, float>(Corner_OnChange);
            this.UpdateBoundingBox();
        }
        #endregion

        #region Abstract
        public abstract void RecomputeEdgePoints();
        #endregion

        #region Virtual         

        public override bool HitTest(int x, int y)
        {
            if (GraphicsUtil.Distance(TopLeftCorner, (float)x, (float)y) < View.ViewFactory.EdgeBoxWidth / 2)
            {
                this.SelectedPoint = TopLeftCorner;
                this.MouseState = Entities.MouseState.Resize;
                return true;
            }
            if (GraphicsUtil.Distance(BottomRightCorner, (float)x, (float)y) < View.ViewFactory.EdgeBoxWidth / 2)
            {
                this.SelectedPoint = BottomRightCorner;
                this.MouseState = Entities.MouseState.Resize;
                return true;
            }

            if (HasPoint(x, y))
            {
                LastHitPoint.X = x;
                LastHitPoint.Y = y;
                this.SelectedPoint = LastHitPoint;
                this.MouseState = Entities.MouseState.Move;
                return true;
            }
            return false;
        }

        public override void MouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            if (this.BoxComponentMoved != null)
            {
                this.BoxComponentMoved(this);
            }
            base.MouseUp(e);
            this.OnChanged();
        }
        public override void MouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            base.MouseMove(e);            
            UpdateBoundingBox();
            if (this.BoxComponentMoving != null)
            {
                this.BoxComponentMoving(this);
            }            
        }
        public override void Move(System.Windows.Forms.MouseEventArgs e)
        {
            if (this.SelectedPoint != null)
            {
                float deltaX = e.X - this.SelectedPoint.X;
                float deltaY = e.Y - this.SelectedPoint.Y;

                TopLeftCorner.X += deltaX;
                TopLeftCorner.Y += deltaY;

                BottomRightCorner.X += deltaX;
                BottomRightCorner.Y += deltaY;

                this.SelectedPoint.X = e.X;
                this.SelectedPoint.Y = e.Y;
                UpdateBoundingBox();
                if (this.BoxComponentMoving != null)
                {
                    this.BoxComponentMoving(this);
                }                
            }
        }
        #endregion

        #region Public
        public void UpdateBoundingBox()
        {
            this.CenterPoint.X = TopLeftCorner.X + Width / 2;
            this.CenterPoint.Y = TopLeftCorner.Y + Height / 2;

            inflatedBox = new RectangleF(TopLeftCorner.X, TopLeftCorner.Y, Width, Height);
            inflatedBox.Inflate(View.ViewFactory.EdgeBoxWidth / 2, View.ViewFactory.EdgeBoxWidth / 2);
            RecomputeEdgePoints();
        }

        public bool HasPoint(float x1, float y1)
        {
            return inflatedBox.Contains(x1, y1);
        }
        #endregion        
    }
}
