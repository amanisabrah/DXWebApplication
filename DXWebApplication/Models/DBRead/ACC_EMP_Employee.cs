using DevExpress.CodeParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXWebApplication.Models
{
    public partial class ACC_EMP_Employee
    {
        public static List<ACC_EMP_Employee> Get(AccountingDbContext _dbContext = null)
        {
            if (_dbContext == null)
            {
                _dbContext = new AccountingDbContext();
            }
            return _dbContext.ACC_EMP_Employee.Where(j => j.ACC_EMP_IsDelete == false).ToList();
        }
        //public static List<ACC_EMP_Employee> GetWithFilter(AccountingDbContext _dbContext, DateTime? entryDate, DateTime? deleteDate, int? Gender)
        //{
        //    return Get(_dbContext).Where(x => (x.JOB_EntryDate >= entryDate || entryDate == null) &&
        //                     (x.JOB_EntryDate < deleteDate || deleteDate == null) &&
        //                     (x.JOB_Gender == Gender || Gender == null)).ToList();
        //}

        public static bool IsValid(ACC_EMP_Employee employee, ModelStateDictionary ModelState, List<HRS_SAL_Salaries> salaryList)
        {
            ModelState.Clear();
            List<ACC_EMP_Employee> emps = ACC_EMP_Employee.Get();

            if (string.IsNullOrEmpty(employee.ACC_EMP_Name))
            {
                ModelState.AddModelError("ACC_EMP_Name", "First Name Is Required");
            }
            if (string.IsNullOrEmpty(employee.ACC_EMP_Name2))
            {
                ModelState.AddModelError("ACC_EMP_Name2", " Name(another Languge) Is Required");
            }
            if (emps.Any(j => j.ACC_EMP_Number == employee.ACC_EMP_Number && 
                j.ACC_EMP_ID != employee.ACC_EMP_ID) || employee.ACC_EMP_Number == 0)
            {
                ModelState.AddModelError("ACC_EMP_Number", " number already exists.");
            }
            if (employee.ACC_EMP_Number < 1 || employee.ACC_EMP_Number > 500)
            {
                ModelState.AddModelError("ACC_EMP_Number", "Number must be between 1 and 500");
            }
            if (emps.Any(j => j.ACC_EMP_documentNum == employee.ACC_EMP_documentNum && 
                j.ACC_EMP_ID != employee.ACC_EMP_ID))
            {
                ModelState.AddModelError("ACC_EMP_documentNum", " fill document number correct");
            }
            if (employee.ACC_EMP_Gender == null)
            {
                ModelState.AddModelError("ACC_EMP_Gender", "Gender is required.");
            }
            if (employee.ACC_EMP_JOBID == null)
            {
                ModelState.AddModelError("ACC_EMP_JOBID", "Job name is required.");
            }
            if (employee.ACC_EMP_WSTID == null)
            {
                ModelState.AddModelError("ACC_EMP_WSTID", "work status is required.");
            }
            if (employee.ACC_EMP_MartialStatus == null)
            {
                ModelState.AddModelError("ACC_EMP_MartialStatus", "Martial Status is required.");
            }
            if (string.IsNullOrEmpty(employee.ACC_EMP_Address))
            {
                ModelState.AddModelError("ACC_EMP_Address", "Address Is Required");
            }
            if (string.IsNullOrEmpty(employee.ACC_EMP_PlaceofBirth))
            {
                ModelState.AddModelError("ACC_EMP_PlaceofBirth", "Place of Birth Is Required");
            }
            DateTime now = DateTime.Now;
            if (employee.ACC_EMP_DateofBirth != DateTime.MinValue)
            {
                int age = now.Year - employee.ACC_EMP_DateofBirth.Year;
                if (now.Month < employee.ACC_EMP_DateofBirth.Month ||
                    (now.Month == employee.ACC_EMP_DateofBirth.Month && now.Day < employee.ACC_EMP_DateofBirth.Day))
                {
                    age--;
                }
                if (age <= 20)
                {
                    ModelState.AddModelError("ACC_EMP_DateofBirth", "Age must be greater than 20.");
                }
            }
            else
                ModelState.AddModelError("ACC_EMP_DateofBirth", "Date of birth req.");
            if (now.Year > employee.ACC_EMP_JoinDate.Year)
            {
                ModelState.AddModelError("ACC_EMP_JoinDate", "Join Year should be the current year or a future year.");
            }


            foreach (var salary in salaryList)
            {
                var maxId = salaryList.Max(s => s.HRS_SAL_ID);

                for (int i = 0; i < salaryList.Count; i++)
                {
                    if (salary.HRS_SAL_ID != salaryList[i].HRS_SAL_ID && salary.HRS_SAL_StartDate <= salaryList[i].HRS_SAL_StartDate)
                    {
                            ModelState.AddModelError("HRS_SAL_StartDate", "Start date must be greater than all previous start dates.");
                           
                    }
                    break;
                }

            }



            return ModelState.IsValid;
        }
    }
}