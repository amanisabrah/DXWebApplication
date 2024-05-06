using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public partial class WST_WorkStatus
    {
        public static List<WST_WorkStatus> Get(AccountingDbContext _dbContext = null)
        {
            if (_dbContext == null)
            {
                _dbContext = new AccountingDbContext();
            }
            return _dbContext.WST_WorkStatus.Where(j => j.WST_ID != 0).ToList();
        }
    }
}