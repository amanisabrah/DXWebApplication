using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public partial class DEP_Departments
    {
        public static void AddNew(DEP_Departments add, AccountingDbContext db)
        {
            add.DEP_EntryDate = DateTime.Now;
            db.DEP_Departments.Add(add);
            db.SaveChanges();
        }

        public static void Edit(DEP_Departments edit, AccountingDbContext db)
        {
            edit.DEP_UpdateDate = DateTime.Now;
            var existingEntity = db.DEP_Departments.Find(edit.DEP_ID);
            if (existingEntity != null)
            {
                existingEntity.DEP_Name = edit.DEP_Name;
                existingEntity.DEP_Name2 = edit.DEP_Name2;
                existingEntity.DEP_Number = edit.DEP_Number;
                existingEntity.DEP_Shortcut = edit.DEP_Shortcut;
                existingEntity.DEP_Email = edit.DEP_Email;
                existingEntity.DEP_Phone = edit.DEP_Phone;
                db.SaveChanges();
            }
        }

        public static void Move(int deptID, int? newParentID, AccountingDbContext db)
        {
            var department = db.DEP_Departments.Find(deptID);
            if (department != null)
            {
                department.DEP_ParentID = newParentID;
                db.SaveChanges();
            }
        }

        public static void Delete(DEP_Departments delete, AccountingDbContext db)
        {
            var existingEntity = db.DEP_Departments.Find(delete.DEP_ID);
            if (existingEntity != null)
            {
                existingEntity.DEP_DeleteDate = DateTime.Now;
                existingEntity.DEP_IsDelete = true;
                db.Entry(existingEntity).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
    }
}