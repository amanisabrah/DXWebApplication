using DXWebApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly AccountingDbContext _accountingDbContext;

        public HomeController()
        {
            _accountingDbContext = new AccountingDbContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<JOB_JOBS> jobs = JOB_JOBS.Get(_accountingDbContext);
            return View(jobs);
        }

        [HttpPost]
        public ActionResult PartialJobsGridView()
        {
            List<JOB_JOBS> jobs = JOB_JOBS.Get(_accountingDbContext);
            return PartialView("_PartialJobsGridView",jobs);
        }

        [HttpPost]
        public ActionResult PartialJobsGridViewAddNew()
        {
            List<JOB_JOBS> jobs = JOB_JOBS.Get(_accountingDbContext);
            return PartialView("_PartialJobsGridView",jobs);
        }
    }
}
