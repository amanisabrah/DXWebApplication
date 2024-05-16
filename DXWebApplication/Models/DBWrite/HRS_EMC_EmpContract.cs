using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public partial class HRS_EMC_EmpContract
    {
        public static void AddNew(HRS_EMC_EmpContract add, AccountingDbContext db)
        {
            add.HRS_EMC_Entrydate = DateTime.Now;
            db.HRS_EMC_EmpContract.Add(add);
            db.SaveChanges();
        }
        public static void Edit(HRS_EMC_EmpContract edit, AccountingDbContext db)
        {
            edit.HRS_EMC_Updatedate = DateTime.Now;
            var existingEntity = db.HRS_EMC_EmpContract.Find(edit.HRS_EMC_ID);
            if (existingEntity != null)
            {
                existingEntity.HRS_EMC_Number = edit.HRS_EMC_Number;
                existingEntity.HRS_EMC_Startdate = edit.HRS_EMC_Startdate;
                existingEntity.HRS_EMC_Enddate = edit.HRS_EMC_Enddate;
                existingEntity.HRS_EMC_Action = edit.HRS_EMC_Action;
                existingEntity.HRS_EMC_Updatedate = edit.HRS_EMC_Updatedate;
                existingEntity.HRS_EMC_Desc = edit.HRS_EMC_Desc;
                existingEntity.HRS_EMC_Issuedate = edit.HRS_EMC_Issuedate;
                existingEntity.HRS_EMC_IssueNum = edit.HRS_EMC_IssueNum;
                existingEntity.HRS_EMC_EmpID = edit.HRS_EMC_EmpID;

                db.SaveChanges();
            }
        }
        public static void Delete(int contID, AccountingDbContext db)
        {
            var contract = db.HRS_EMC_EmpContract.Find(contID);
            if (contract != null)
            {
                contract.HRS_EMC_DeleteDate = DateTime.Now;
                contract.HRS_EMC_IsDeleted = true;
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();

            }
        }

    }
}