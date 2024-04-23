using DXWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.Models
{
    public partial class JOB_JOBS
    {
        public static List<JOB_JOBS> Get(AccountingDbContext _dbContext)
        {
            return _dbContext.JOB_JOBS.ToList();
        }

        public enum Gender
        {
            Female,
            Male
        }

    }
}