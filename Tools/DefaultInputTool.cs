using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Controller;
using FlowChart.Models;
using FlowChart.Entities;
using FlowChart.Utility;
using FlowChart.Visitors;
using System.Windows.Forms;
using FlowChart.Views;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FlowChart.Tools
{
    internal class DefaultInputTool:BaseInputTool
    {
        private float maxTop = 0;
        private float minTop = 0;
        private float maxLeft = 0;
        private float minLeft = 0;

        private float zoom = 1;
        private readonly float zoomLimit = 25;
        private readonly int offSet = 25;
        private PointF lastPoint = new PointF();

        public override float ZoomFactor
        {
            get
            {
                return base.ZoomFactor;
            }
            set
            {
                base.ZoomFactor = value;
                if (this.View != null)
                {
                    float delta = (601 * ZoomFactor - this.View.Width);
                    Zoom(delta, (int)lastPoint.X, (int)lastPoint.Y);
                    this.View.Invalidate();
                    /*if (this.Resize != null)
                    {
                        this.Resize();
                    }*/
                }
            }
        }
        public ScrollableControl View
        {
            get;
            private set;
        }

        public FlowChartModel Model
        {
            get;
            set;
        }

        internal DefaultInputTool(FlowChartModel model, FlowChartPage view)
        {
            this.Model = model;
            this.View = view;

            View.MouseDown += new System.Windows.Forms.MouseEventHandler(View_MouseDown);
            View.MouseUp += new System.Windows.Forms.MouseEventHandler(View_MouseUp);
            View.MouseMove += new System.Windows.Forms.MouseEventHandler(View_MouseMove);            
            View.KeyDown += new System.Windows.Forms.KeyEventHandler(View_KeyDown);
            View.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(View_MouseDoubleClick);            
            View.MouseWheel += new MouseEventHandler(View_MouseWheel);
            view.DragMouse += new Action<float, float>(view_DragMouse);
        }

        private void Zoom(float delta, int focusX, int focusY)
        {
            float oX = View.Left + focusX;
            float oY = View.Top + focusY;

            float nWidth = View.Width + delta;
            float nHeight = View.Height + delta;

            float newX = (focusX * nWidth) / View.Width;
            float newY = (focusY * nHeight) / View.Height;

            View.Width = (int)Math.Round(nWidth);
            View.Height = (int)Math.Round(nHeight);

            View.Left = (int)Math.Round(oX - newX);
            View.Top = (int)Math.Round(oY - newY);
        }

        internal void ResetView(float maxTop, float minTop, float maxLeft, float minLeft)
        {
            this.maxTop = maxTop;
            this.minTop = minTop;
            this.maxLeft = maxLeft;
            this.minLeft = minLeft;
        }

        void view_DragMouse(float x, float y)
        {
            float nTop = View.Top + y;
            float nLeft = View.Left + x;

            if (nLeft > minLeft && nLeft <= maxLeft)
            {
                View.Left += (int)x;
            }
            if (nTop > minTop && nTop <= maxTop)
            {
                View.Top += (int)y;
            }
        }

        void View_MouseWheel(object sender, MouseEventArgs e)
        {
            MouseEventArgs p = BacktrackMouse(e);

            if ((Control.ModifierKeys & Keys.Control) != Keys.None)
            {                
                float f = 1.1f;
                if ((e.Delta / 120) < 0) f = 1 / f;

                this.lastPoint.X = p.X;
                this.lastPoint.Y = p.Y;
            
                this.OnZoom(f, p.X, p.Y);
            }
            else
            {
                float nTop = View.Top + e.Delta / 10;
                if (nTop > minTop && nTop <= maxTop)
                {
                    View.Top += (int)e.Delta / 10;
                }
                this.OnScroll(e.Delta/10, p.X, p.Y);
            }
        }
        
        void View_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.MouseDoubleClick(BacktrackMouse(e));
        }
        void View_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.KeyDown(e);
        }
        void View_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.MouseMove(BacktrackMouse(e));
        }

        void View_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.MouseUp(BacktrackMouse(e));
        }

        void View_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.MouseDown(BacktrackMouse(e));
        }        

        private void SaveInstance()
        {
            System.Windows.Forms.Clipboard.SetText(Util.GetJSONString(SelectedComponent.GetComponent()));
        }
        private void LoadInstance()
        {
            try
            {
                FlowChartComponent fc = Util.ConvertFromJSON<FlowChartComponent>(System.Windows.Forms.Clipboard.GetText());
                BaseComponent c = (BaseComponent)Activator.CreateInstance(Type.GetType(fc.Type));
                c.SetComponent(fc);
                c.ID = Util.GetUniqueID();
                c.Accept(new ObjectCreateVisitor(Model, fc));                
                this.OnAdd(c);                                
            }
            catch
            { 
            }
        }

        protected override void KeyDown(System.Windows.Forms.KeyEventArgs e)
        {            
            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {
                if (SelectedComponent != null)
                {
                    this.OnDelete(SelectedComponent);
                    this.View.Invalidate();                    
                }
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.C && e.Control)
            {
                if (SelectedComponent != null)
                {
                    SaveInstance();
                }
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.V && e.Control)
            {
                LoadInstance();
            }
        }
        protected override void MouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (SelectedComponent != null)
            {                
                if (SelectedComponent.HitTest(e.X, e.Y))
                {
                    if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
                    {
                        SaveInstance();
                        LoadInstance();
                        BaseComponent addedCmp = SelectedComponent;
                        if (addedCmp.HitTest(e.X, e.Y))
                        {
                            OnSelect(addedCmp);
                            addedCmp.MouseDown(e);
                        }
                        return;
                    }
                    else
                    {
                        SelectedComponent.MouseDown(e);
                        return;
                    }
                }
                else
                {
                    OnSelect(null);
                }
            }

            foreach (BaseComponent x in Model.Items)
            {
                x.MouseState = Entities.MouseState.None;
                x.IsSelected = false;
                if (x.HitTest(e.X, e.Y))
                {
                    OnSelect(x);
                    x.MouseDown(e);
                    break;
                }
                
            }            
            this.View.Invalidate();
        }
        protected override void MouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (SelectedComponent != null)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {                    
                    if (SelectedComponent.MouseState == MouseState.Resize)
                    {
                        SelectedComponent.MouseMove(e);
                        this.View.Invalidate();
                    }
                    else if (SelectedComponent.MouseState == MouseState.Move)
                    {
                        SelectedComponent.Move(e);
                        this.View.Invalidate();
                    }                    
                }
            }            
        }

        protected override void MouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            if (SelectedComponent != null)
            {
                SelectedComponent.MouseUp(e);
                this.View.Invalidate();
            }   
        }

        protected override void MouseDoubleClick(System.Windows.Forms.MouseEventArgs e)
        {
            if (SelectedComponent != null)
            {
                MouseEventArgs p = BacktrackMouse(e);
                this.OnDblClick(p.X, p.Y);                
            }   
        }

        protected MouseEventArgs BacktrackMouse(MouseEventArgs e)
        {
            float zoom = this.ZoomFactor;
            Matrix mx = new Matrix(zoom, 0, 0, zoom, 0, 0);
            mx.Translate(View.AutoScrollPosition.X * (1.0f / zoom), View.AutoScrollPosition.Y * (1.0f / zoom));
            mx.Invert();
            Point[] pa = new Point[] { new Point(e.X, e.Y) };
            mx.TransformPoints(pa);
            return new MouseEventArgs(e.Button, e.Clicks, pa[0].X, pa[0].Y, e.Delta);
        }
    }
}
