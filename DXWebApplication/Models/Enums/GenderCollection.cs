using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public class GenderCollection
    {
        public static Dictionary<string, int> Gender //{ { "Female", 0 }, { "Male",1 } } to binding enum values to controls (comboBox)
        {
            get
            {
                var FontSize = new Dictionary<string, int>();
                foreach (var text in Enum.GetNames(typeof(Gender)))//to get an array of string names of the Gender enum values (["Female", "Male"])
                {
                    FontSize.Add(text, (int)Enum.Parse(typeof(Gender), text));//to convert each name back to its corresponding Gender enum value (0 or 1).
                }
                return FontSize;
            }
        }
    }
    public enum Gender
    {
        Female = 1,
        Male = 2
    }
}