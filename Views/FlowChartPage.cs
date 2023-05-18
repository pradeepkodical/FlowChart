using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using FlowChart.Models;

namespace FlowChart.Views
{
    public partial class FlowChartPage : UserControl
    {
        private Point LastDown;
        public event Action<MouseEventArgs> ZoomMouse;
        public event Action<MouseEventArgs> ScrollMouse;
        public event Action<float, float> DragMouse;

        public FlowChartModel Model { get; set; }
        public BaseComponent SelectedComponent { get; set; }
        public float ZoomFactor { get; set; }
        private BaseComponent Component;

        public FlowChartPage()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
                        
            txtEditor.KeyDown += new KeyEventHandler(txtEditor_KeyDown);
            txtEditor.Leave += new EventHandler(txtEditor_Leave);
            MouseClick += new MouseEventHandler(FlowChartPage_MouseClick);
        }

        void FlowChartPage_MouseClick(object sender, MouseEventArgs e)
        {
            txtEditor_Leave(sender, e);
        }

        void txtEditor_Leave(object sender, EventArgs e)
        {
            if (Component != null)
            {
                Component.Text = txtEditor.Text;
            }
            txtEditor.Visible = false;
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
                Component = component;
                txtEditor.Visible = true;
                txtEditor.Left = (int)((component.CenterPoint.X - txtEditor.Width / 2) * ZoomFactor);
                txtEditor.Top = (int)((component.CenterPoint.Y - txtEditor.Height) * ZoomFactor);
                txtEditor.Text = Component.Text;
                txtEditor.Focus();
                txtEditor.SelectAll();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Focus();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            LastDown = e.Location;
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) != Keys.None && e.Button == MouseButtons.Left && DragMouse != null)
            {
                DragMouse?.Invoke(e.X - LastDown.X, e.Y - LastDown.Y);
                return;
            }
            base.OnMouseMove(e);
            Invalidate();
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) != Keys.None)
            {
                ZoomMouse?.Invoke(e);
            }
            else
            {
                ScrollMouse?.Invoke(e);
            }
            base.OnMouseWheel(e);
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush =
                new LinearGradientBrush(e.ClipRectangle, Color.White, Color.WhiteSmoke, 90.0f))
            {
                e.Graphics.FillRectangle(brush, e.ClipRectangle);
            }

            for (int i = 0; i < Width; i += 50)
            {
                for (int j = 0; j < Height; j += 50)
                {
                    e.Graphics.DrawRectangle(Pens.LightGray, i, j, 50, 50);
                }
            }
            e.Graphics.DrawString("FlowChart: pradeep@kiproshamerica.com", Font, Brushes.LightGray, 10, Height - 20);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                var mx = new Matrix(ZoomFactor, 0, 0, ZoomFactor, 0, 0);
                mx.Translate(AutoScrollPosition.X * (1.0f / ZoomFactor), AutoScrollPosition.Y * (1.0f / ZoomFactor));

                e.Graphics.Transform = mx;
                if (Model.Items != null)
                {
                    Model.Items.ForEach(x =>
                    {
                        x.View.Draw(e.Graphics);
                    });
                    SelectedComponent?.View?.Draw(e.Graphics);
                }
            }
            catch
            { 
                //Ignore any Paint error.
            }
        }
    }
}
