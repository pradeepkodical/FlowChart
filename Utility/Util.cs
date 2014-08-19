using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart.Models;
using System.Web.Script.Serialization;
using System.Drawing;

namespace FlowChart.Utility
{
    internal class Util
    {
        internal static string GetJSONString(object data)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(data);
        }
        internal static T ConvertFromJSON<T>(String json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }
        internal static string GetUniqueID()
        {
            return DateTime.Now.Ticks.ToString();
        }
    }
}
