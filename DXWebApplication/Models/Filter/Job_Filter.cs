using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public class Job_Filter
    {
        public Nullable<System.DateTime> JOB_FilterEntryDate { get; set; }
        public Nullable<System.DateTime> JOB_FilterDeleteDate { get; set; }
        public int JOB_FilterGender { get; set; }

    }
}