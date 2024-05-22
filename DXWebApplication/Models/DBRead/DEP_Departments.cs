using DevExpress.CodeParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DXWebApplication.Models
{
    public partial class DEP_Departments
    {

        public string DEP_PhoneNum
        {
            get
            {
                if (!string.IsNullOrEmpty(DEP_Phone) && DEP_Phone.Contains("#")) 
                {

                    var ss = DEP_Phone.Split('#');
                    return $"(+{ss[0]}) {ss[1]}";
                }
                return DEP_Phone;
            }
            set
            {
                string phoneNumAfter = string.Concat(value.Where(char.IsDigit));
                phoneNumAfter = phoneNumAfter.Insert(3, "#");
                DEP_Phone = phoneNumAfter;
            }
        }
        public static List<DEP_Departments> Get(AccountingDbContext _dbContext = null)
        {
            if (_dbContext == null)
            {
                _dbContext = new AccountingDbContext();
            }
            return _dbContext.DEP_Departments.Where(j => j.DEP_IsDelete == false).ToList();
        }


        public static bool IsValid(DEP_Departments department, ModelStateDictionary ModelState)
        {
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (!emailRegex.IsMatch(department.DEP_Email))
            {
                ModelState.AddModelError("department.Email", "Invalid email format");
            }

            if (department.DEP_Phone != null)
            {
                var phoneNumber = department.DEP_Phone;
                string phoneNumAfter = string.Concat(phoneNumber.Where(char.IsDigit));
                if ((phoneNumAfter.Length <= 9|| phoneNumAfter.Length > 12))
                {
                    ModelState.AddModelError("department.DEP_Phone", "Phone number must be more than 9 and less than 12 digits");
                }
            }

            return ModelState.IsValid;
        }

    }
}