using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlowChart.Models;
using FlowChart.Controller;
using System.IO;
using FlowChart.Utility;
using FlowChart.Entities;
using FlowChart.Tools;
using System.Drawing;
using FlowChart.Persistance;
using FlowChart.Views;
using FlowChart.Views.Default;
using FlowChart.Views.Grey;

namespace FlowChart
{
    public partial class FormMain : Form
    {
        private FlowChartController controller;
        public FormMain()
        {
            InitializeComponent();
            controller = new FlowChartController(flowChartPage1, new GreyViewFactory());
            flowChartContainerBindingSource.DataSource = controller.GetUndoBuffers();
            controller.ViewFactory = new DefaultViewFactory();
            panel1.Resize+=new EventHandler(panel1_Resize);            

            
            controller.Resize += new Action(controller_Resize);
            controller.Selected += new Action<BaseComponent>(controller_OnSelected);
            controller.Changed += new Action<BaseComponent>(controller_OnChange);
            controller.OnFlowChartChange += new Action(controller_OnFlowChartChange);
            
            propertyGrid1.SelectedGridItemChanged += new SelectedGridItemChangedEventHandler(propertyGrid1_SelectedGridItemChanged);
            propertyGrid1.Leave += new EventHandler(propertyGrid1_Leave);
            undoGrid.CellContentClick += new DataGridViewCellEventHandler(undoGrid_CellContentClick);
            lstAllObjects.SelectedIndexChanged += new EventHandler(lstAllObjects_SelectedIndexChanged);

            this.flowChartPage1.Width = 601;
            this.flowChartPage1.Height = 851;
        }
        
        void lstAllObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAllObjects.SelectedItems.Count > 0)
            {
                controller.SelectedComponent = lstAllObjects.SelectedItems[0].Tag as BaseComponent;
            }
        }

        void undoGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0 && e.RowIndex < undoGrid.Rows.Count)
            {
                FlowChartContainer fc = undoGrid.Rows[e.RowIndex].DataBoundItem as FlowChartContainer;
                if (fc != null)
                {
                    controller.Load(fc);
                }
            }
        }

        void controller_OnFlowChartChange()
        {
            lstAllObjects.Items.Clear();
            
            controller.Model.Items.ForEach(x=>{
                ListViewItem lv = new ListViewItem(x.Text) { Tag = x, ImageIndex =x.ImageIndex };
                lstAllObjects.Items.Add(lv);
            });
            
            flowChartContainerBindingSource.ResetBindings(false);
        }

        void propertyGrid1_Leave(object sender, EventArgs e)
        {
            controller.PropertyChanged();
        }

        void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            flowChartPage1.Invalidate();            
        }

        void controller_OnChange(BaseComponent obj)
        {
            if (obj != null && obj.SelectedPoint != null)
            {
                txtStatusText.Text = string.Format("{0} - {1}", obj.SelectedPoint.X, obj.SelectedPoint.Y);
            }
            propertyGrid1.SelectedObject = obj;
        }

        void controller_OnSelected(BaseComponent obj)
        {
            propertyGrid1.SelectedObject = obj;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1_Resize(sender, e);
        }

        private void btnBox_Click(object sender, EventArgs e)
        {
            controller.Add(new RectangleComponent() {
                Text = "Process",  
                TopLeftCorner = new FlowChartPoint(50, 50), 
                BottomRightCorner = new FlowChartPoint(100, 100) });
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            controller.Add(new RoundComponent()
            { 
                Text = "Terminator", 
                TopLeftCorner = new FlowChartPoint(50, 50), 
                BottomRightCorner = new FlowChartPoint(100, 100) 
            });
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            controller.Add(new LineComponent()
            {
                Text = "Connector", 
                StartPoint = new FlowChartPoint(100, 100), 
                EndPoint = new FlowChartPoint(200, 200) 
            });
        }

        private void btnCurvedLine_Click(object sender, EventArgs e)
        {
            controller.Add(new CurvedLineComponent()
            {
                Text="Connector", 
                StartPoint = new FlowChartPoint(200, 200), 
                EndPoint = new FlowChartPoint(200, 100), 
                ControlPoint2 = new FlowChartPoint(100, 100), 
                ControlPoint1 = new FlowChartPoint(100, 200) 
            });
        }

        private void btnRhombus_Click(object sender, EventArgs e)
        {
            controller.Add(new RhombusComponent()
            {
                Text = "Decision",
                TopLeftCorner = new FlowChartPoint(50, 50),
                BottomRightCorner = new FlowChartPoint(100, 100)
            });
        }

        private void btnDatabase_Click(object sender, EventArgs e)
        {
            controller.Add(new DatabaseComponent()
            {
                Text = "Database",
                TopLeftCorner = new FlowChartPoint(50, 50),
                BottomRightCorner = new FlowChartPoint(100, 100)
            });
        } 
        
        private void panel1_Resize(object sender, EventArgs e)
        {
            float offSet = 10;
            controller.ResetView(
                offSet, 
                panel1.Height - flowChartPage1.Height - offSet, 
                offSet, 
                panel1.Width - flowChartPage1.Width - offSet);

            controller_Resize();
        }

        void controller_Resize()
        {
            int left = (int)((panel1.Width - flowChartPage1.Width) / 2.0f);
            if (left > 4)
            {
                flowChartPage1.Left = left;
            }            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            new FileStorage(saveFileDialog1.FileName).Save(controller.Model);            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            controller.Load(new FileStorage(openFileDialog1.FileName).Load());
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            saveFileDialog2.ShowDialog();
        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {            
            new ImageStorage(saveFileDialog2.FileName).Save(controller.Model);           
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            controller.ZoomFactor*=1.5f;            
            panel1_Resize(sender, e);
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            controller.ZoomFactor /= 1.5f;            
            panel1_Resize(sender, e);
        }

        private void btnFit_Click(object sender, EventArgs e)
        {
            controller.ZoomFactor = 1.0f;
            panel1_Resize(sender, e);
        }

        private void ddlTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.ViewFactory = new GreyViewFactory();
        }               
    }
}
