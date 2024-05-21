using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public partial class DEP_Departments
    {
        public static List<DEP_Departments> Get(AccountingDbContext _dbContext = null)
        {
            if (_dbContext == null)
            {
                _dbContext = new AccountingDbContext();
            }
            return _dbContext.DEP_Departments.Where(j => j.DEP_IsDelete == false).ToList();
        }

    }
}