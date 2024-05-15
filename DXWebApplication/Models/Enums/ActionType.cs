using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public class ActionType
    {
        public static Dictionary<string, int> TypeAction 
        {
            get
            {
                var FontSize = new Dictionary<string, int>();
                foreach (var text in Enum.GetNames(typeof(Gender)))
                {
                    FontSize.Add(text, (int)Enum.Parse(typeof(Gender), text));
                }
                return FontSize;
            }
        }
    }
    public enum TypeAction
    {
        New = 1,
        Renew = 2
    }
}
