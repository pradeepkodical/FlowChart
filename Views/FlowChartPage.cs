using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using FlowChart.Models;

namespace FlowChart.Views
{
    public partial class FlowChartPage : UserControl
    {
        public Point LastDown;
        public event Action<MouseEventArgs> ZoomMouse;
        public event Action<MouseEventArgs> ScrollMouse;
        public event Action<float, float> DragMouse;

        public FlowChartModel Model { get; set; }
        public BaseComponent SelectedComponent { get; set; }
        public float ZoomFactor { get; set; }
        private BaseComponent component;

        public FlowChartPage()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
                        
            txtEditor.KeyDown += new KeyEventHandler(txtEditor_KeyDown);
            txtEditor.Leave += new EventHandler(txtEditor_Leave);
            this.MouseClick += new MouseEventHandler(FlowChartPage_MouseClick);
        }

        void FlowChartPage_MouseClick(object sender, MouseEventArgs e)
        {
            txtEditor_Leave(sender, e);
        }

        void txtEditor_Leave(object sender, EventArgs e)
        {
            if (this.component != null)
            {
                this.component.Text = this.txtEditor.Text;
            }
            this.txtEditor.Visible = false;
        }

        void txtEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtEditor_Leave(sender, e);
            }
        }

        public void ShowTextEditor(BaseComponent component)
        {
            if (component != null)
            {
                this.component = component;
                this.txtEditor.Visible = true;
                this.txtEditor.Left = (int)((component.CenterPoint.X - this.txtEditor.Width / 2) * ZoomFactor);
                this.txtEditor.Top = (int)((component.CenterPoint.Y - this.txtEditor.Height) * ZoomFactor);
                this.txtEditor.Text = this.component.Text;
                this.txtEditor.Focus();
                this.txtEditor.SelectAll();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.Focus();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            LastDown = e.Location;
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.None)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (this.DragMouse != null)
                    {
                        this.DragMouse(e.X - LastDown.X, e.Y - LastDown.Y);
                        return;
                    }                    
                }
            }
            base.OnMouseMove(e);
            this.Invalidate();
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.None)
            {
                if (this.ZoomMouse != null)
                {
                    this.ZoomMouse(e);
                }
            }
            else
            {
                if (this.ScrollMouse != null)
                {
                    this.ScrollMouse(e);
                }
            }
            base.OnMouseWheel(e);
            this.Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush =
                new LinearGradientBrush(e.ClipRectangle, Color.White, Color.WhiteSmoke, 90.0f))
            {
                e.Graphics.FillRectangle(brush, e.ClipRectangle);
            }

            for (int i = 0; i < this.Width; i += 50)
            {
                for (int j = 0; j < this.Height; j += 50)
                {
                    e.Graphics.DrawRectangle(Pens.LightGray, i, j, 50, 50);
                }
            }
            e.Graphics.DrawString("FlowChart: kpradeeprao@gmail.com, http://www.kiprosh.com", this.Font, Brushes.LightGray, 10, this.Height - 20);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                System.Drawing.Drawing2D.Matrix mx = new System.Drawing.Drawing2D.Matrix(ZoomFactor, 0, 0, ZoomFactor, 0, 0);
                mx.Translate(this.AutoScrollPosition.X * (1.0f / ZoomFactor), this.AutoScrollPosition.Y * (1.0f / ZoomFactor));

                e.Graphics.Transform = mx;
                if (Model.Items != null)
                {
                    Model.Items.ForEach(x =>
                    {
                        x.View.Draw(e.Graphics);
                    });
                    if (SelectedComponent != null)
                    {
                        SelectedComponent.View.Draw(e.Graphics);
                    }
                }
            }
            catch
            { 
            }
        }
    }
}
