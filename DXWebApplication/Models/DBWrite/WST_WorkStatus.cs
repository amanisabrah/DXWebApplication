using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public partial class WST_WorkStatus
    {
        public static void AddNew(WST_WorkStatus add, AccountingDbContext db)
        {
            add.WST_EntryDate = DateTime.Now;
            db.WST_WorkStatus.Add(add);
            db.SaveChanges();
        }

        public static void Edit(WST_WorkStatus edit, AccountingDbContext db)
        {
            edit.WST_UpdateDate = DateTime.Now;
            var existingEntity = db.WST_WorkStatus.Find(edit.WST_ID);
            if (existingEntity != null)
            {
                existingEntity.WST_Name = edit.WST_Name;
                existingEntity.WST_Name2 = edit.WST_Name2;
                existingEntity.WST_Number = edit.WST_Number;
                existingEntity.WST_Shortcut = edit.WST_Shortcut;
                existingEntity.WST_KindOfWorkStatus = edit.WST_KindOfWorkStatus;
                existingEntity.WST_UpdateDate = edit.WST_UpdateDate;

                db.SaveChanges();
            }
        }

        public static void Delete(int workStatusID, AccountingDbContext dbContext)
        {
            var workStatus = dbContext.WST_WorkStatus.Find(workStatusID);
            if (workStatus != null)
            {
                dbContext.WST_WorkStatus.Remove(workStatus);
                dbContext.SaveChanges();
            }
        }



    }
}