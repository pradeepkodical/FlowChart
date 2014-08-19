using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FlowChart.Utility;
using FlowChart.Entities;
using FlowChart.Controller;
using FlowChart.Models;

namespace FlowChart.Persistance
{
    internal class FileStorage:BaseStorage
    {
        private string fileName;
        public FileStorage(string fileName)
        {
            this.fileName = fileName;
        }
        internal override FlowChartContainer Load()
        {
            try
            {
                string strContent = File.ReadAllText(fileName);
                return Util.ConvertFromJSON<FlowChartContainer>(strContent);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                throw ex;
            }
        }
        internal override void Save(FlowChartModel model)
        {
            string strContent = Util.GetJSONString(GetContent(model));            
            File.WriteAllText(fileName, strContent);
        }
    }
}
