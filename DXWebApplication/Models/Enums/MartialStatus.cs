using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public class MartialStatus
    {
        public static Dictionary<string, int> Status //{ { "Single", 1 }, { "Married",2 }, { "separated ",3 }} 
        {
            get
            {
                var FontSize = new Dictionary<string, int>();
                foreach (var text in Enum.GetNames(typeof(Status)))
                {
                    FontSize.Add(text, (int)Enum.Parse(typeof(Status), text));
                }
                return FontSize;
            }
        }

    }

    public enum Status
    {
        Single = 1,
        Married = 2,
        separated = 3
    }


}
