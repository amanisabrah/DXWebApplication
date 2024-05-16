using DevExpress.CodeParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public partial class HRS_SAL_Salaries
    {
        public static  List<HRS_SAL_Salaries> Get(AccountingDbContext _dbContext = null)
        {
            if (_dbContext == null)
            {
                _dbContext = new AccountingDbContext();
            }
            return _dbContext.HRS_SAL_Salaries.Where(j => j.HRS_SAL_ID != 0).ToList();
        }
        public static List<HRS_SAL_Salaries> GetByEmpId(int empid, AccountingDbContext _dbContext = null)
        {
            if (empid >= 0)
               return Get(_dbContext).Where(x => x.HRS_SAL_EMPID == empid).ToList();
            else
                return new List<HRS_SAL_Salaries>();
        }
    }
}