using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public class EmployeeReport
    {

        public int ACC_EMR_EMPID { get; set; }
        public string ACC_EMR_EmpName { get; set; }
        public string ACC_EMR_JOBName { get; set; }
        public string ACC_EMR_WSTName { get; set; }
        public Nullable<int> ACC_EMP_documentNum { get; set; }

        public static List<EmployeeReport> Get()
        {

            var model = new List<EmployeeReport>();
            List<ACC_EMP_Employee> emp = ACC_EMP_Employee.Get();
            foreach (var item in emp)
            {
                var report = new EmployeeReport
                {
                    ACC_EMR_EMPID= item.ACC_EMP_ID,
                    ACC_EMR_EmpName = item.ACC_EMP_Name,
                    ACC_EMR_JOBName = item.JOB_JOBS.JOB_Name,
                    ACC_EMR_WSTName = item.WST_WorkStatus.WST_Name,
                    ACC_EMP_documentNum = item.ACC_EMP_documentNum
                };
                model.Add(report);
            }
            return model;
        }



    }
}