using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public class WorkstatusReport
    {
        //These are public properties of the WorkstatusReport class.
        public string WSR_Name { get; set; }
        public string WSR_Shorcut { get; set; }
        public string WSR_Status { get; set; }
       public static List<WorkstatusReport> Get() //It retrieves a list of WST_WorkStatus objects using Get
        {
            var model = new List<WorkstatusReport>();
            List<WST_WorkStatus> workStatus = WST_WorkStatus.Get();
            foreach (var item in workStatus)
            {
                string enumName = Enum.GetName(typeof(WorkStatus), item.WST_KindOfWorkStatus);
                var report = new WorkstatusReport
                {
                    WSR_Name = item.WST_Name,
                    WSR_Shorcut = item.WST_Shortcut,
                    WSR_Status = enumName
                };
                model.Add(report);
            }
            return model;
        }
    }
}