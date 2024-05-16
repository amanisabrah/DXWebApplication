using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public class SalaryReport
    {
        public int HRS_SAL_ID { get; set; }
        public decimal HRS_SAL_SalaryAmount { get; set; }
        public System.DateTime HRS_SAL_StartDate { get; set; }
        public Nullable<System.DateTime> HRS_SAL_EndDate { get; set; }
        public string HRS_SAL_EmpName { get; set; }
        public Nullable<System.DateTime> HRS_SAL_JoinDate { get; set; }
        public static List<SalaryReport> Get()
        {
            var model = new List<SalaryReport>();
            List<HRS_SAL_Salaries> sal = HRS_SAL_Salaries.Get();
            foreach (var item in sal)
            {
                var report = new SalaryReport
                {
                    HRS_SAL_ID = item. HRS_SAL_ID,
                    HRS_SAL_SalaryAmount = item.HRS_SAL_SalaryAmount,
                    HRS_SAL_StartDate = item.HRS_SAL_StartDate,
                    HRS_SAL_EndDate = item.HRS_SAL_EndDate,
                    HRS_SAL_EmpName =item.ACC_EMP_Employee.ACC_EMP_Name,
                    HRS_SAL_JoinDate=item.ACC_EMP_Employee.ACC_EMP_JoinDate
                };
                model.Add(report);
            }
            return model;
        }
    }
}