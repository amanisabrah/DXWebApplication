using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public class KindOfWorkStatus
    {
        public static Dictionary<string, int> WorkStatus  
        {
            get
            {
                var FontSize = new Dictionary<string, int>();
                foreach (var text in Enum.GetNames(typeof(WorkStatus)))
                {
                    FontSize.Add(text, (int)Enum.Parse(typeof(WorkStatus), text));
                }
                return FontSize;
            }
        }

    }
    public enum WorkStatus
    {
        OnWork = 1,
        StoppedSalary = 2,
        ResignedFromWork = 3
    }
}