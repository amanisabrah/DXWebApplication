using DevExpress.Web;
using DevExpress.Web.Internal.XmlProcessor;
using DevExpress.Web.Mvc;
using DXWebApplication.Models;
using DXWebApplication.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;


namespace DXWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly AccountingDbContext _accountingDbContext;

        public HomeController()
        {
            _accountingDbContext = new AccountingDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Jobs()
        {
            int currentYear = DateTime.Now.Year;
            var filter = new Job_Filter()
            {
                //JOB_FilterEntryDate = new DateTime(currentYear, 1, 1),
                //JOB_FilterDeleteDate = new DateTime(currentYear, 12, 29),
                JOB_FilterGender = (int)Gender.Male
            };
            ViewBag.Filter = filter;
            Session["FileByte"] = null;
            Session["FileName"] = null;
            return View("Jobs");
        }

        [HttpGet]
        public ActionResult PartialJobsGridView(DateTime? entryDate, DateTime? deleteDate, int? Gender)
        {
            List<JOB_JOBS> jobs = JOB_JOBS.GetWithFilter(_accountingDbContext, entryDate, deleteDate, Gender);
            return PartialView("_PartialJobsGridView", jobs);
        }

        [HttpPost]
        public ActionResult PartialJobsGridView(string Command, int? JOB_ID = null, DateTime? FilterStartDate = null, DateTime? FilterEndDate = null, int? FilterGender = null)//= null indicates that if no value is provided for JOB_ID, it will default to null.
        {
            List<JOB_JOBS> jobs = JOB_JOBS.GetWithFilter(_accountingDbContext, FilterStartDate, FilterEndDate, FilterGender);
            JOB_JOBS job = null;
            switch (Command)
            {
                case "ADDNEWROW":
                     job = new JOB_JOBS();
                    ViewBag.job = job;
                    break;
                case "STARTEDIT":
                    job = jobs.Where(x => x.JOB_ID == JOB_ID).FirstOrDefault();
                    ViewBag.job = job;
                    Session["FileByte"] = job.JOB_File;
                    Session["FileName"] = job.JOB_FileName;
                    break;
                case "CANCELEDIT":
                    Session["FileByte"] = null;
                    Session["FileName"] = null;
                    break;

            }

            return PartialView("_PartialJobsGridView", jobs);
        }

        [HttpPost]
        public ActionResult PartialJobsGridViewAddNew(JOB_JOBS add, string Command, int? JOB_ID = null, DateTime? FilterStartDate = null, DateTime? FilterEndDate = null, int? FilterGender = null)
        {
            List<JOB_JOBS> jobs = JOB_JOBS.GetWithFilter(_accountingDbContext, FilterStartDate, FilterEndDate, FilterGender);

            if (string.IsNullOrEmpty(add.JOB_Name))
            {
                ModelState.AddModelError("JOB_Name", "First Name Is Required");
            }
            if (string.IsNullOrEmpty(add.JOB_Name2))
            {
                ModelState.AddModelError("JOB_Name2", "Secound Namw Is Required");
            }

            if (jobs.Any(j => j.JOB_Number == add.JOB_Number && j.JOB_ID != add.JOB_ID))
            {
                ModelState.AddModelError("JOB_Number", "Job number already exists.");
            }
            if (add.JOB_Number > 100 )
            {
                ModelState.AddModelError("JOB_Number", "number between 1 And 500");

            }

            if (ModelState.IsValid)
            {
                add.JOB_File = (byte[])Session["FileByte"];
                add.JOB_FileName = (string)Session["FileName"];
                Session["FileByte"] = null;
                Session["FileName"] = null;
                JOB_JOBS.AddNew(add, _accountingDbContext);

            }

            return PartialView("_PartialJobsGridView", jobs);
        }

        [HttpPost]

        public ActionResult PartialJobsGridViewEdit(JOB_JOBS edit, string Command, int? JOB_ID = null, DateTime? FilterStartDate = null, DateTime? FilterEndDate = null, int? FilterGender = null)
        {
            List<JOB_JOBS> jobs = JOB_JOBS.GetWithFilter(_accountingDbContext, FilterStartDate, FilterEndDate, FilterGender);

            if (ModelState.IsValid)
            {
                edit.JOB_File = (byte[])Session["FileByte"];
                edit.JOB_FileName = (string)Session["FileName"];
                Session["FileByte"] = null;
                Session["FileName"] = null;
                JOB_JOBS.Edit(edit, _accountingDbContext);
            }

            return PartialView("_PartialJobsGridView", jobs);
        }

        [HttpPost]
        public ActionResult PartialJobsGridViewDelete(JOB_JOBS delete, DateTime? FilterStartDate = null, DateTime? FilterEndDate = null, int? FilterGender = null)
        {
            if (ModelState.IsValid)
            {
                JOB_JOBS.Delete(delete, _accountingDbContext);
            }
            return PartialView("_PartialJobsGridView", JOB_JOBS.GetWithFilter(_accountingDbContext, FilterStartDate, FilterEndDate, FilterGender));
        }

        public ActionResult MultiSelectionImageUpload()
        {
            UploadControlExtension.GetUploadedFiles("ucMultiSelection", null, FileUploadCompleteMultiSelect);
            return null;
        }
        public void FileUploadCompleteMultiSelect(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)//It is called when a file upload is completed using the UploadControl.
        {
            Session["FileByte"] = e.UploadedFile.FileBytes;
            Session["FileName"] = e.UploadedFile.FileName;
            e.CallbackData = e.UploadedFile.FileName;
        }

        public ActionResult DownloadFile()
        {
            byte[] fileBytes = (byte[])Session["FileByte"];
            string fileName = (string)Session["FileName"];

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public ActionResult DownloadFileWithID(int JOBID)
        {
            var job = _accountingDbContext.JOB_JOBS.Find(JOBID);
            byte[] fileBytes = job.JOB_File;
            string fileName = job.JOB_FileName;

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        
        public ActionResult ViewReport()
        {
         
            return View(new XtraReport1());
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        public ActionResult WorkStatus()
        {
            return View("WorkStatus");
        }

        [HttpGet]
        public ActionResult PartialWorkStatusGridView()
        {
            List<WST_WorkStatus> workStatus = WST_WorkStatus.Get(_accountingDbContext);
            return PartialView("_PartialWorkStatusGridView", workStatus);
        }

        [HttpPost]
        public ActionResult PartialWorkStatusGridView(string Command)
        {
            List<WST_WorkStatus> workStatus = WST_WorkStatus.Get(_accountingDbContext);
            return PartialView("_PartialWorkStatusGridView", workStatus);
        }



        [HttpPost]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<WST_WorkStatus, int> updateValues)
        {
            foreach (var workStatus in updateValues.Insert)
            {
                if (updateValues.IsValid(workStatus))
                    InsertWorkStatus(workStatus, updateValues);
                else
                    updateValues.SetErrorText(workStatus, "Name1 validation errors");
            }
            foreach (var workStatus in updateValues.Update)
            {
                if (updateValues.IsValid(workStatus))
                    UpdateWorkStatus(workStatus, updateValues);
                else
                    updateValues.SetErrorText(workStatus, "Name2 validation errors");
            }
            foreach (var workStatusID in updateValues.DeleteKeys)
            {
                DeleteWorkStatus(workStatusID, updateValues);
            }
            return PartialView("_PartialWorkStatusGridView", WST_WorkStatus.Get(_accountingDbContext));
        }


        protected void InsertWorkStatus(WST_WorkStatus workStatus, MVCxGridViewBatchUpdateValues<WST_WorkStatus, int> updateValues)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    WST_WorkStatus.AddNew(workStatus, _accountingDbContext);
                }
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(workStatus, e.Message);
            }
        }

        protected void UpdateWorkStatus(WST_WorkStatus workStatus, MVCxGridViewBatchUpdateValues<WST_WorkStatus, int> updateValues)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    WST_WorkStatus.Edit(workStatus, _accountingDbContext);
                }

            }
            catch (Exception e)
            {
                updateValues.SetErrorText(workStatus, e.Message);
            }
        }

        protected void DeleteWorkStatus(int workStatusID, MVCxGridViewBatchUpdateValues<WST_WorkStatus, int> updateValues)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    WST_WorkStatus.Delete(workStatusID, _accountingDbContext);
                }
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(workStatusID, e.Message);

            }
        }

        public ActionResult ViewBatchReport()
        {

            return View(new XtraReport2());
        }
    }
}
