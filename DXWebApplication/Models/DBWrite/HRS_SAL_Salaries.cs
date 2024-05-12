using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public partial class HRS_SAL_Salaries
    {

        public static void AddNew(HRS_SAL_Salaries add, AccountingDbContext db)
        {
            add.HRS_SAL_EntryDate = DateTime.Now;
            db.HRS_SAL_Salaries.Add(add);
            db.SaveChanges();

        }

        public static void Edit(HRS_SAL_Salaries edit, AccountingDbContext db)
        {
            edit.HRS_SAL_UpdateDate = DateTime.Now;
            var existingEntity = db.HRS_SAL_Salaries.Find(edit.HRS_SAL_ID);
            if (existingEntity != null)
            {
                existingEntity.HRS_SAL_SalaryAmount = edit.HRS_SAL_SalaryAmount;
                existingEntity.HRS_SAL_StartDate = edit.HRS_SAL_StartDate;
                existingEntity.HRS_SAL_EndDate = edit.HRS_SAL_EndDate;
                existingEntity.HRS_SAL_EntryDate = edit.HRS_SAL_EntryDate;
                existingEntity.HRS_SAL_UpdateDate = edit.HRS_SAL_UpdateDate;

                db.SaveChanges();
            }
        }

        public static void Delete(int salaryID, AccountingDbContext dbContext)
        {
            var salary = dbContext.HRS_SAL_Salaries.Find(salaryID);
            if (salary != null)
            {
                dbContext.HRS_SAL_Salaries.Remove(salary);
                dbContext.SaveChanges();
            }
        }


    }
}