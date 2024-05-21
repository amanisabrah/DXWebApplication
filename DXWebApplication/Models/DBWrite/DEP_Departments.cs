using System;
using System.Collections.Generic;
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
                existingEntity.DEP_Name= edit.DEP_Name;
                existingEntity.DEP_Name2 = edit.DEP_Name2;
                existingEntity.DEP_Number = edit.DEP_Number;
                existingEntity.DEP_Shortcut = edit.DEP_Shortcut;
                existingEntity.DEP_Email = edit.DEP_Email;
                existingEntity.DEP_Phone = edit.DEP_Phone;
                db.SaveChanges();
            }
        }

        //public static void MovePost(int deptID, int? newParentID, AccountingDbContext db)
        //{

        //}

        public static void Delete(int departmentID, AccountingDbContext dbContext)
        {
            var department = dbContext.DEP_Departments.Find(departmentID);
            if (department != null)
            {
                dbContext.DEP_Departments.Remove(department);
                dbContext.SaveChanges();
            }
        }

    }
}