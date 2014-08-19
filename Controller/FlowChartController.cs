using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Models;
using FlowChart.Entities;
using FlowChart.Utility;
using System.IO;
using FlowChart.Views;
using FlowChart.Visitors;
using System.Drawing;
using FlowChart.Tools;
using FlowChart.Persistance;
using FlowChart.Factory;

namespace FlowChart.Controller
{
    public class FlowChartController
    {        
        /// <summary>
        /// 
        /// </summary>
        public event Action Resize;
        /// <summary>
        /// On Flow Chart Component Selected
        /// </summary>
        public event Action<BaseComponent> Selected;
        /// <summary>
        /// On Flow Chart Component Moved
        /// </summary>
        public event Action<BaseComponent> Changed;
        /// <summary>
        /// 
        /// </summary>
        public event Action OnFlowChartChange;
        private bool isReseting;
               

        /// <summary>
        /// 
        /// </summary>
        public FlowChartModel Model
        {
            get;
            private set;
        }
        
        private BaseComponent selectedComponent;
        internal BaseComponent SelectedComponent 
        {
            get
            {
                return selectedComponent;
            }
            set
            {
                this.inputTool.SelectedComponent = value;
                this.view.SelectedComponent = value;

                if (value == this.selectedComponent)
                {
                    return;
                }

                if (this.selectedComponent != null)
                {
                    this.selectedComponent.IsSelected = false;
                }

                this.selectedComponent = value;

                if (selectedComponent != null)
                {
                    selectedComponent.IsSelected = true;
                }

                if (this.Selected != null)
                {
                    this.Selected(selectedComponent);
                }
                this.view.Invalidate();
            }
        }

        
        private float zoomFactor = 1.0f;
        internal float ZoomFactor
        {
            get
            {
                return zoomFactor;
            }
            set
            {
                if (value > 0.1 && value < 8)
                {
                    zoomFactor = value;
                    this.inputTool.ZoomFactor = zoomFactor;
                    this.view.ZoomFactor = zoomFactor;                    
                }
            }
        }        

        private FlowChartPage view;
        private DefaultInputTool inputTool;
        private MemoryStorage memoryStorage;

        private ViewAbstractFactory viewFactory;
        public ViewAbstractFactory ViewFactory 
        {
            get
            {
                return viewFactory;
            }
            set 
            {
                viewFactory = value;
                if (this.Model != null)
                {
                    this.Model.Items.FindAll(x => x.View != null).ForEach(x => x.View.ViewFactory = viewFactory);
                }
            }
        }

        public FlowChartController(FlowChartPage view, ViewAbstractFactory viewFactory)
        {
            this.view = view;
            this.ViewFactory = viewFactory;
            this.Model = new FlowChartModel();
            this.inputTool = new DefaultInputTool(Model, view);
            this.view.Model = Model;
            this.memoryStorage = new MemoryStorage();

            this.inputTool.Add += new Action<BaseComponent>(inputTool_Add);
            this.inputTool.DblClick += new Action<float, float>(inputTool_DblClick);
            this.inputTool.Delete += new Action<BaseComponent>(inputTool_Delete);
            this.inputTool.Scroll += new Action<float, float, float>(inputTool_Scroll);
            this.inputTool.Zoom += new Action<float, float, float>(inputTool_Zoom);
            this.inputTool.Select += new Action<BaseComponent>(inputTool_Select);

            this.ZoomFactor = 1.0f;            
        }        
        
        #region Input Handler
        
        internal void ResetView(float maxTop, float minTop, float maxLeft, float minLeft)
        {
            inputTool.ResetView(maxTop, minTop, maxLeft, minLeft);
        }

        void inputTool_Select(BaseComponent obj)
        {
            this.SelectedComponent = obj;
        }

        void inputTool_Zoom(float f, float x, float y)
        {
            this.ZoomFactor *= f;
        }

        void inputTool_Scroll(float delta, float x, float y)
        {
            
        }

        void inputTool_Delete(BaseComponent obj)
        {
            RemoveComponent(SelectedComponent);
            SelectedComponent = null;   
        }

        void inputTool_DblClick(float arg1, float arg2)
        {
            this.view.ShowTextEditor(this.SelectedComponent);
        }

        void inputTool_Add(BaseComponent cmp)
        {
            Add(cmp);
            SelectedComponent = cmp;
        }
        #endregion        

        internal void Component_Change(BaseComponent obj)
        {
            if (this.Changed != null)
            {
                this.Changed(obj);
            }
            CommitChange();
            this.view.Invalidate();
        }

        internal void Add(BaseComponent component)
        {
            component.Accept(new ObjectModifyVisitor(this.Model));
            component.Accept(new ViewDirectorVisitor(ViewFactory, this.view.Font));            
            component.Init();
            this.Model.Items.Add(component);
            this.Model.Items = this.Model.Items.OrderBy(x => x.SortOrder).ToList();
            this.view.Invalidate();
            component.Changed += new Action<BaseComponent>(Component_Change);            
            this.SelectedComponent = component;            
            CommitChange();
        }
        
        internal void RemoveComponent(BaseComponent component)
        {
            this.Model.Items.Remove(component);            
            CommitChange();
        }       

        #region Persist FlowChart
        
        internal void Load(FlowChartContainer container)
        {
            this.isReseting = true;
            this.SelectedComponent = null;
            this.Model.Items.Clear();
                        
            container.Items.ForEach(x => {
                BaseComponent c = (BaseComponent)Activator.CreateInstance(Type.GetType(x.Type));                
                c.SetComponent(x);                
                this.Add(c);
            });

            container.Items.ForEach(x =>
            {
                BaseComponent c = this.Model.Items.Find(y => y.ID == x.ID);
                c.Accept(new ObjectCreateVisitor(Model, x));                
            });            

            this.view.Invalidate();
            this.isReseting = false;
        }
        
        private void CommitChange()
        {            
            if (!isReseting)
            {
                memoryStorage.Save(Model);
                if (this.OnFlowChartChange != null)
                {
                    this.OnFlowChartChange();
                }                
            }
        }
        #endregion                     
        
        internal void PropertyChanged()
        {
            if (this.SelectedComponent != null)
            {
                this.SelectedComponent.IsSelected = false;                
                CommitChange();
            }
        }
                
        internal List<FlowChartContainer> GetUndoBuffers()
        {
            return memoryStorage.GetUndoBuffers();
        }        
    }
}
