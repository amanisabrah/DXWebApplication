using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public partial class HRS_EMC_EmpContract
    {
        public static List<HRS_EMC_EmpContract> Get(AccountingDbContext _dbContext = null)
        {
            if (_dbContext == null)
            {
                _dbContext = new AccountingDbContext();
            }
            return _dbContext.HRS_EMC_EmpContract.Where(j => j.HRS_EMC_ID != 0).ToList();
        }

        public static List<HRS_EMC_EmpContract> GetByEmpId(int empid, AccountingDbContext _dbContext = null)
        {
            if (empid >= 0)

                return Get(_dbContext).Where(x => x.HRS_EMC_EmpID == empid).ToList();

            else

                return new List<HRS_EMC_EmpContract>();
        }



    }
}