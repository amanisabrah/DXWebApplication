using DevExpress.Web;
using DevExpress.Web.Mvc;
using DXWebApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
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

            switch (Command)
            {
                case "STARTEDIT":
                    var job = jobs.Where(x => x.JOB_ID == JOB_ID).FirstOrDefault();
                    ViewBag.job = job;
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

            if (add.JOB_Number > 100)
            {
                ModelState.AddModelError("JOB_Number", "number between 1 And 500");

            }

            if (ModelState.IsValid)
            {
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
                JOB_JOBS.Edit(edit, _accountingDbContext);
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

        [HttpPost]
        public ActionResult UploadControlUpload()
        {
            //string[] errors;
            //UploadedFile[] files = UploadControlExtension.GetUploadedFiles("UploadControl",
            //    MyUploadControlValidationSettings.Settings, out errors, (s, e) => { },
            //    UploadControl_FilesUploadComplete);
            return null;
        }

        public ActionResult MultiSelectionImageUpload()
        {
            UploadControlExtension.GetUploadedFiles("ucMultiSelection", null, FileUploadCompleteMultiSelect);
            return null;
        }
        public void FileUploadCompleteMultiSelect(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            //string uploadDirectory = UploadControlHelper.UploadDirectory;
            //string resultFileUrl = uploadDirectory + e.UploadedFile.FileName;
            //string resultFilePath = Request.MapPath(resultFileUrl);

            //e.UploadedFile.SaveAs(resultFilePath);

            //UploadingUtils.RemoveFileWithDelay(e.UploadedFile.FileName, resultFilePath, 5);

            //IUrlResolutionService urlResolver = sender as IUrlResolutionService;
            //if (urlResolver != null)
            //{
            //    string url = urlResolver.ResolveClientUrl(resultFileUrl);
            //    e.CallbackData = UploadControlHelper.GetCallbackData(e.UploadedFile, url);
            //}
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
            }
            foreach (var workStatus in updateValues.Update)
            {
                if (updateValues.IsValid(workStatus))
                    UpdateWorkStatus(workStatus, updateValues);
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
    }
}
