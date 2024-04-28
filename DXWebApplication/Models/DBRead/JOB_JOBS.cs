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


    }
}