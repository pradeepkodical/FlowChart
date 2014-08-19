using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using FlowChart.Entities;
using FlowChart.Utility;
using FlowChart.Visitors;
using FlowChart.Views;

namespace FlowChart.Models
{
    public abstract class BaseComponent
    {
        #region Events
        
        public event Action<BaseComponent> Changed;
        protected void OnChanged()
        {
            if (this.Changed != null)
            {
                this.Changed(this);
            }
        }
        #endregion

        #region Abstract        
        public abstract bool HitTest(int x, int y);
        public abstract FlowChartComponent GetComponent();
        public abstract void SetComponent(FlowChartComponent component);
        public abstract void Move(System.Windows.Forms.MouseEventArgs e);
        public abstract void Init();        
        public abstract void Accept(BaseVisitor visitor);
        #endregion

        #region Properties

        [DescriptionAttribute("Unique ID of the item")]
        public string UniqueID { get{return ID;} }
        private string _text;
        [DescriptionAttribute("Text Description of the element")]
        public string Text 
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    this.OnChanged();
                }
            }
        }
        [Browsable(false)]
        public FlowChartPoint CenterPoint { get; private set; }        
        [Browsable(false)]
        public FlowChartPoint LastHitPoint { get; private set; }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        [Browsable(false)]
        public string ID { get; set; }
        [Browsable(false)]
        public int SortOrder { get; set; }
        [Browsable(false)]
        public int ImageIndex { get; set; }
        [Browsable(false)]
        public FlowChartPoint SelectedPoint { get; set; }        
        private bool _IsSelected;
        [Browsable(false)]
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                if (!_IsSelected)
                {
                    this.SelectedPoint = null;
                }                
            }
        }

        [Browsable(false)]
        public BaseView View { get; set; }
        #endregion

        #region Constructor
        public BaseComponent()
        {            
            this.MouseState = Entities.MouseState.None;
            this.LastHitPoint = new FlowChartPoint();
            this.CenterPoint = new FlowChartPoint();
            this.ID = Util.GetUniqueID();
        }
        #endregion

        #region Virtual
        
        public virtual void MouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (this.SelectedPoint != null)
            {
                this.SelectedPoint.X = e.X;
                this.SelectedPoint.Y = e.Y;
            }
        }

        public virtual void MouseDown(System.Windows.Forms.MouseEventArgs e)
        {

        }

        public virtual void MouseUp(System.Windows.Forms.MouseEventArgs e)
        {

        }
        #endregion        
    }
}
