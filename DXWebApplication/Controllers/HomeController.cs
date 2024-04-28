﻿using DevExpress.Web;
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
            int currentYear = DateTime.Now.Year;
            var filter = new Job_Filter()
            {
                //JOB_FilterEntryDate = new DateTime(currentYear, 1, 1),
                //JOB_FilterDeleteDate = new DateTime(currentYear, 12, 29),
                JOB_FilterGender = (int)Gender.Male
            };
            ViewBag.Filter = filter;
            return View();
        }


        [HttpGet]
        public ActionResult PartialJobsGridView(DateTime? entryDate, DateTime? deleteDate, int? Gender)
        {
            List<JOB_JOBS> jobs = JOB_JOBS.Get(_accountingDbContext);
            jobs = jobs.Where(x => (x.JOB_EntryDate >= entryDate || entryDate == null) &&
                            (x.JOB_EntryDate < deleteDate || deleteDate == null) &&
                            (x.JOB_Gender == Gender || Gender == null)).ToList();

            return PartialView("_PartialJobsGridView", jobs);
        }
        [HttpPost]
        public ActionResult PartialJobsGridView(string Command, int? JOB_ID = null, DateTime? FilterStartDate = null, DateTime? FilterEndDate = null, int? FilterGender = null)//= null indicates that if no value is provided for JOB_ID, it will default to null.
        {
            List<JOB_JOBS> jobs = JOB_JOBS.Get(_accountingDbContext);

            switch (Command)
            {
                case "STARTEDIT":
                    var job = jobs.Where(x => x.JOB_ID == JOB_ID).FirstOrDefault();
                    ViewBag.job = job;
                    break;
            }
            jobs = jobs.Where(x => (x.JOB_EntryDate >= FilterStartDate || FilterStartDate == null) &&
                             (x.JOB_EntryDate < FilterEndDate || FilterEndDate == null) &&
                             (x.JOB_Gender == FilterGender || FilterGender == null)).ToList();

            return PartialView("_PartialJobsGridView", jobs);
        }

        [HttpPost]
        public ActionResult PartialJobsGridViewAddNew(JOB_JOBS add, string Command, int? JOB_ID = null, DateTime? FilterStartDate = null, DateTime? FilterEndDate = null, int? FilterGender = null)
        {
            List<JOB_JOBS> jobs = JOB_JOBS.Get(_accountingDbContext);

            if (string.IsNullOrEmpty(add.JOB_Name))
            {
                ModelState.AddModelError("JOB_Name", "First Name Is Required");
            }
            if (string.IsNullOrEmpty(add.JOB_Name2))
            {
                ModelState.AddModelError("JOB_Name2", "Secound Namw Is Required");
            }

            if (add.JOB_Number > 100)
            {
                ModelState.AddModelError("JOB_Number", "number between 1 And 500");

            }

            if (ModelState.IsValid)
            {
                JOB_JOBS.AddNew(add, _accountingDbContext);
                jobs = jobs.Where(x => (x.JOB_EntryDate >= FilterStartDate || FilterStartDate == null) &&
                         (x.JOB_EntryDate < FilterEndDate || FilterEndDate == null) &&
                         (x.JOB_Gender == FilterGender || FilterGender == null)).ToList();
            }

            return PartialView("_PartialJobsGridView", jobs);
        }

        [HttpPost]

        public ActionResult PartialJobsGridViewEdit(JOB_JOBS edit, string Command, int? JOB_ID = null, DateTime? FilterStartDate = null, DateTime? FilterEndDate = null, int? FilterGender = null)
        {
            List<JOB_JOBS> jobs = JOB_JOBS.Get(_accountingDbContext);

            if (ModelState.IsValid)
            {
                JOB_JOBS.Edit(edit, _accountingDbContext);

                jobs = jobs.Where(x => (x.JOB_EntryDate >= FilterStartDate || FilterStartDate == null) &&
                          (x.JOB_EntryDate < FilterEndDate || FilterEndDate == null) &&
                          (x.JOB_Gender == FilterGender || FilterGender == null)).ToList();
            }

            return PartialView("_PartialJobsGridView", jobs);
        }

        [HttpPost]
        public ActionResult PartialJobsGridViewDelete(JOB_JOBS delete)
        {
            if (ModelState.IsValid)
            {
                JOB_JOBS.Delete(delete, _accountingDbContext);
            }
            return PartialView("_PartialJobsGridView", JOB_JOBS.Get(_accountingDbContext));
        }

    }
}