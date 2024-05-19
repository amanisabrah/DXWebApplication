using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public partial class ACC_EMP_Employee
    {
        public static void AddNew(ACC_EMP_Employee add, AccountingDbContext db, List<HRS_SAL_Salaries> salaryList = null, List<HRS_EMC_EmpContract> contractList=null)
        {
            add.ACC_EMP_EntryDate = DateTime.Now;
            db.ACC_EMP_Employee.Add(add);
            db.SaveChanges();
            var existingEntity = db.ACC_EMP_Employee.Find(add.ACC_EMP_ID);
            if (salaryList != null)
            {
                foreach (var salary in salaryList)
                {
                    salary.HRS_SAL_EMPID = existingEntity.ACC_EMP_ID;
                    if (salary.HRS_SAL_ID >= 10000000)
                    {
                        DXWebApplication.Models.HRS_SAL_Salaries.AddNew(salary, db);
                    }
                }
            }

            if (contractList != null)
            {
                foreach (var contract in contractList)
                {
                    contract.HRS_EMC_EmpID = existingEntity.ACC_EMP_ID;
                    if (contract.HRS_EMC_ID >= 10000000)
                    {
                        DXWebApplication.Models.HRS_EMC_EmpContract.AddNew(contract, db);
                    }
                    else
                    {
                        DXWebApplication.Models.HRS_EMC_EmpContract.Edit(contract, db);
                    }
                }
            }
        }
        public static void Edit(ACC_EMP_Employee edit, AccountingDbContext db, List<HRS_SAL_Salaries> salaryList = null, List<HRS_EMC_EmpContract> contractList = null)
        {
            edit.ACC_EMP_UpdatedDate = DateTime.Now;
            var existingEntity = db.ACC_EMP_Employee.Find(edit.ACC_EMP_ID);
            if (existingEntity != null)
            {
                existingEntity.ACC_EMP_Number = edit.ACC_EMP_Number;
                existingEntity.ACC_EMP_Name = edit.ACC_EMP_Name;
                existingEntity.ACC_EMP_Name2 = edit.ACC_EMP_Name2;
                existingEntity.ACC_EMP_Address = edit.ACC_EMP_Address;
                existingEntity.ACC_EMP_MartialStatus = edit.ACC_EMP_MartialStatus;
                existingEntity.ACC_EMP_DateofBirth = edit.ACC_EMP_DateofBirth;
                existingEntity.ACC_EMP_PlaceofBirth = edit.ACC_EMP_PlaceofBirth;
                existingEntity.ACC_EMP_documentNum = edit.ACC_EMP_documentNum;
                existingEntity.ACC_EMP_Gender = edit.ACC_EMP_Gender;
                existingEntity.ACC_EMP_JoinDate = edit.ACC_EMP_JoinDate;
                existingEntity.ACC_EMP_EntryDate = edit.ACC_EMP_EntryDate;
                existingEntity.ACC_EMP_UpdatedDate = edit.ACC_EMP_UpdatedDate;
                existingEntity.ACC_EMP_JOBID = edit.ACC_EMP_JOBID;
                existingEntity.ACC_EMP_WSTID = edit.ACC_EMP_WSTID;

                db.SaveChanges();
            }
            if (salaryList != null)
            {
                foreach (var salary in salaryList)
                {
                    salary.HRS_SAL_EMPID = existingEntity.ACC_EMP_ID;
                    if (salary.HRS_SAL_ID >= 10000000)
                    {
                        DXWebApplication.Models.HRS_SAL_Salaries.AddNew(salary, db);
                    }
                    else
                    {
                        DXWebApplication.Models.HRS_SAL_Salaries.Edit(salary, db);
                    }
                }
            }

            if (contractList != null)
            {
                foreach (var contract in contractList)
                {
                    contract.HRS_EMC_EmpID = existingEntity.ACC_EMP_ID;
                    if (contract.HRS_EMC_ID >= 1000000)
                    {
                        DXWebApplication.Models.HRS_EMC_EmpContract.AddNew(contract, db);
                    }
                    else
                    {
                        DXWebApplication.Models.HRS_EMC_EmpContract.Edit(contract, db);
                    }
                }
            }
        }
        public static void Delete(ACC_EMP_Employee delete, AccountingDbContext db)
        {
            var existingEntity = db.ACC_EMP_Employee.Find(delete.ACC_EMP_ID);
            if (existingEntity != null)
            {
                existingEntity.ACC_EMP_DeleteDate = DateTime.Now;
                existingEntity.ACC_EMP_IsDelete = true;
                db.Entry(existingEntity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}