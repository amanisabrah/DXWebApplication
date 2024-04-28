using DXWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace DXWebApplication.Models
{
    public partial class JOB_JOBS
    {
        public static List<JOB_JOBS> Get(AccountingDbContext _dbContext)
        {
            return _dbContext.JOB_JOBS.Where(j => j.JOB_IsDeleted == false).ToList();
        }
        public static List<JOB_JOBS> GetWithFilter(AccountingDbContext _dbContext, DateTime? entryDate, DateTime? deleteDate, int? Gender)
        {
            return Get(_dbContext).Where(x => (x.JOB_EntryDate >= entryDate || entryDate == null) &&
                             (x.JOB_EntryDate < deleteDate || deleteDate == null) &&
                             (x.JOB_Gender == Gender || Gender == null)).ToList();
        }

    }
}