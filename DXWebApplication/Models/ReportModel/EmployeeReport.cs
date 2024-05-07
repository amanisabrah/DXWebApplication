using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public class EmployeeReport
    {
        public string ACC_EMP_Name { get; set; }
        public Nullable<int> ACC_EMP_JOBID { get; set; }
        public Nullable<int> ACC_EMP_WSTID { get; set; }
        public Nullable<int> ACC_EMP_documentNum { get; set; }


        public static List<EmployeeReport> Get() //It retrieves a list of WST_WorkStatus objects using Get
        {
            var model = new List<EmployeeReport>();
            List<EmployeeReport> emp = EmployeeReport.Get();
            foreach (var item in emp)
            {

                var report = new EmployeeReport
                {
                    ACC_EMP_Name = item.ACC_EMP_Name,
                    ACC_EMP_JOBID = item.ACC_EMP_JOBID,
                    ACC_EMP_WSTID = item.ACC_EMP_WSTID,
                    ACC_EMP_documentNum=item.ACC_EMP_documentNum
                };
                model.Add(report);
            }

            return model;
        }
    }
}