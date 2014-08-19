using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Models;

namespace FlowChart.Tools
{
    internal abstract class BaseInputTool
    {
        public event Action<float, float, float> Zoom;
        public event Action<float, float, float> Scroll;
        public event Action<BaseComponent> Add;
        public event Action<BaseComponent> Delete;
        public event Action<float, float> DblClick;
        public event Action<BaseComponent> Select;

        protected void OnSelect(BaseComponent obj)
        {
            if (this.Select != null)
            {
                this.Select(obj);
            }
        }

        protected void OnZoom(float zoom, float x , float y) 
        {
            if (this.Zoom != null)
            {
                this.Zoom(zoom,x,y);
            }
        }

        protected void OnScroll(float delta, float x, float y)
        {
            if (this.Scroll != null)
            {
                this.Scroll(delta, x, y);
            }
        }

        protected void OnAdd(BaseComponent obj)
        {
            if (this.Add != null)
            {
                this.Add(obj);
            }
        }

        protected void OnDelete(BaseComponent obj)
        {
            if (this.Delete != null)
            {
                this.Delete(obj);
            }
        }

        protected void OnDblClick(float x, float y)
        {
            if (this.DblClick != null)
            {
                this.DblClick(x, y);
            }
        }

        public BaseComponent SelectedComponent { get; set; }
        public virtual float ZoomFactor { get;set;}

        protected abstract void KeyDown(System.Windows.Forms.KeyEventArgs e);
        protected abstract void MouseMove(System.Windows.Forms.MouseEventArgs e);
        protected abstract void MouseUp(System.Windows.Forms.MouseEventArgs e);
        protected abstract void MouseDown(System.Windows.Forms.MouseEventArgs e);
        protected abstract void MouseDoubleClick(System.Windows.Forms.MouseEventArgs e);
    }
}
