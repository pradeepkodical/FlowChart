using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FlowChart.Utility;
using FlowChart.Entities;
using FlowChart.Controller;
using System.Drawing;
using System.Drawing.Imaging;
using FlowChart.Models;

namespace FlowChart.Persistance
{
    internal class ImageStorage:BaseStorage
    {
        private string fileName;
        public ImageStorage(string fileName)
        {
            this.fileName = fileName;
        }
        internal override FlowChartContainer Load()
        {
            return null;
        }
        internal override void Save(FlowChartModel model)
        {
            float ZoomFactor = 4.0f;

            using (Bitmap bmp = new Bitmap((int)(601*ZoomFactor), (int)(851*ZoomFactor)))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    System.Drawing.Drawing2D.Matrix mx = new System.Drawing.Drawing2D.Matrix(ZoomFactor, 0, 0, ZoomFactor, 0, 0);                    
                    g.Transform = mx;

                    g.PageUnit = GraphicsUnit.Pixel;
                    g.Clear(Color.White);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    
                    model.Items.ForEach(x =>
                    {
                        x.View.Draw(g);
                    });

                    g.Save();

                    ImageFormat fm = ImageFormat.Bmp;
                    if (Path.GetExtension(fileName).ToLower() == ".png")
                    {
                        fm = System.Drawing.Imaging.ImageFormat.Png;
                    }
                    else if (Path.GetExtension(fileName).ToLower() == ".jpg")
                    {
                        fm = System.Drawing.Imaging.ImageFormat.Jpeg;
                    }
                    else if (Path.GetExtension(fileName).ToLower() == ".jpeg")
                    {
                        fm = System.Drawing.Imaging.ImageFormat.Jpeg;
                    }
                    else if (Path.GetExtension(fileName).ToLower() == ".gif")
                    {
                        fm = System.Drawing.Imaging.ImageFormat.Gif;
                    }
                    else if (Path.GetExtension(fileName).ToLower() == ".tiff")
                    {
                        fm = System.Drawing.Imaging.ImageFormat.Tiff;
                    }
                    bmp.Save(fileName, fm);
                }
            }
        }
    }
}
