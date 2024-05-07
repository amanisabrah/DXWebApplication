using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public partial class JOB_JOBS
    {

        public static void AddNew(JOB_JOBS add, AccountingDbContext db)
        {
            add.JOB_EntryDate = DateTime.Now;
            db.JOB_JOBS.Add(add);
            db.SaveChanges();
        }

        public static void Edit(JOB_JOBS edit, AccountingDbContext db)
        {
            edit.JOB_UpdateDate = DateTime.Now;
            var existingEntity = db.JOB_JOBS.Find(edit.JOB_ID);
            if (existingEntity != null)
            {
                existingEntity.JOB_Name = edit.JOB_Name;
                existingEntity.JOB_Name2 = edit.JOB_Name2;
                existingEntity.JOB_Shortcut = edit.JOB_Shortcut;
                existingEntity.JOB_Number = edit.JOB_Number;
                existingEntity.JOB_Gender = edit.JOB_Gender;
                existingEntity.JOB_EntryDate = edit.JOB_EntryDate;
                existingEntity.JOB_UpdateDate = edit.JOB_UpdateDate;
                existingEntity.JOB_File = edit.JOB_File;
                existingEntity.JOB_FileName = edit.JOB_FileName;
            

                db.SaveChanges();
            }
        }
        public static void Delete(JOB_JOBS delete, AccountingDbContext db)
        {
            var existingEntity = db.JOB_JOBS.Find(delete.JOB_ID);
            if (existingEntity != null)
            {
                existingEntity.JOB_DeleteDate = DateTime.Now;
                existingEntity.JOB_IsDeleted = true;
                db.Entry(existingEntity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        
    }
}
