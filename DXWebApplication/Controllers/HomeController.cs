using DevExpress.Pdf.Native.BouncyCastle.Asn1.Ocsp;
using DevExpress.Pdf.Native.BouncyCastle.Ocsp;
using DevExpress.Web;
using DevExpress.Web.Internal.XmlProcessor;
using DevExpress.Web.Mvc;
using DXWebApplication.Models;
using DXWebApplication.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics.Contracts;
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
        #region everything else
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
        #endregion
        ///////////////////////////////////////////////////////////////////////////

        #region Employee
        #region EmployeeGridView
        [HttpGet]
        public ActionResult Employee()
        {
            return View("Employee");
        }

        [HttpGet]
        public ActionResult PartialEmpGridView()
        {
            List<ACC_EMP_Employee> emps = ACC_EMP_Employee.Get(_accountingDbContext);
            return PartialView("_PartialEmpGridView", emps);
        }

        [HttpPost]
        public ActionResult PartialEmpGridView(string Command, int? ACC_EMP_ID = null)
        {
            List<ACC_EMP_Employee> emps = ACC_EMP_Employee.Get(_accountingDbContext);
            switch (Command)
            {
                case "ADDNEWROW":
                    var emp = new ACC_EMP_Employee();
                    Session["SalaryList"] = new List<HRS_SAL_Salaries>();
                    ViewBag.emp = emp;

                    break;
                case "STARTEDIT":
                    emp = emps.Where(x => x.ACC_EMP_ID == ACC_EMP_ID).FirstOrDefault();
                    ViewBag.emp = emp;
                    Session["SalaryList"] = HRS_SAL_Salaries.GetByEmpId(ACC_EMP_ID??0, _accountingDbContext);
                    break;
                case "CANCELEDIT":
                    Session["SalaryList"] = null;
                    break;
            }

            return PartialView("_PartialEmpGridView", emps);
        }
        #endregion
        #region EmployeeEditForm

        //foreach (var workStatus in updateValues.Insert)
        //    {
        //        if (updateValues.IsValid(workStatus))
        //            InsertWorkStatus(workStatus, updateValues);
        //        else
        //            updateValues.SetErrorText(workStatus, "Name1 validation errors");
        //    }

        [HttpPost]
        public ActionResult PartialEmpGridViewAddNew(ACC_EMP_Employee employee, string Command)
        {
            List<HRS_SAL_Salaries> salaryList = Session["SalaryList"] as List<HRS_SAL_Salaries>;

            if (ACC_EMP_Employee.IsValid(employee, ModelState, salaryList))
            {

                ACC_EMP_Employee.AddNew(employee, _accountingDbContext, salaryList);

            }
            else
            {

                ViewBag.emp = employee;

                var validationErrors = ModelState.Values.Where(E => E.Errors.Count > 0)
                .SelectMany(E => E.Errors)
                .Select(E => E.ErrorMessage)
                .ToList();
                ViewBag.ValidationErrors = string.Join(", ", validationErrors);

            }

            List<ACC_EMP_Employee> emps = ACC_EMP_Employee.Get(_accountingDbContext);
            return PartialView("_PartialEmpGridView", emps);
        }

        [HttpPost]

        public ActionResult PartialEmpGridViewEdit(ACC_EMP_Employee employee, string Command)
        {
            List<HRS_SAL_Salaries> salaryList = Session["SalaryList"] as List<HRS_SAL_Salaries>;

            if (ACC_EMP_Employee.IsValid(employee, ModelState, salaryList))
            {
                ACC_EMP_Employee.Edit(employee, _accountingDbContext, salaryList);
            }
            else
            {
                ViewBag.emp = employee;
                var validationErrors = ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToList();
                ViewBag.ValidationErrors = string.Join(", ", validationErrors); 
            }

            List<ACC_EMP_Employee> emps = ACC_EMP_Employee.Get(_accountingDbContext);
            return PartialView("_PartialEmpGridView", emps);
        }


        [HttpPost]
        public ActionResult PartialEmpGridViewDelete(ACC_EMP_Employee delete)
        {
            if (ModelState.IsValid)
            {
                ACC_EMP_Employee.Delete(delete, _accountingDbContext);
            }

            return PartialView("_PartialEmpGridView", ACC_EMP_Employee.Get(_accountingDbContext));
        }
        public ActionResult ViewEmpReport()
        {

            return View(new XtraReport3());
        }
        #endregion
        #endregion


        ////////////////////////////////////////////////////////////////////////////

        #region salary
        public ActionResult PartialSalGridView(int empid)
        {
            
            ViewBag.id = empid;
            List<HRS_SAL_Salaries> salary;
            if (Session["SalaryList"] == null)
            {
                salary = HRS_SAL_Salaries.GetByEmpId(empid, _accountingDbContext);
                Session["SalaryList"] = salary;//keep track of the original list of salaries before any modifications.
            }
            else
            {
                salary = Session["SalaryList"] as List<HRS_SAL_Salaries>;
            }
            return PartialView("_PartialSalGridView", salary);
        }

        public List<HRS_SAL_Salaries> Salaries { get; set; }

        [HttpPost]
        public ActionResult SalaryBatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<HRS_SAL_Salaries, int> updateValues, int id)
        {
            var SALID = 10000000;
            List<HRS_SAL_Salaries> salaryList = Session["SalaryList"] as List<HRS_SAL_Salaries>;// retrieves the list of salaries from the session
      
            foreach (var salary in salaryList)
            {
                if (salary.HRS_SAL_ID >= SALID)
                {
                    SALID = salary.HRS_SAL_ID + 1;
                }
            }// increament id to unduplicate it 

            foreach (var salary in updateValues.Insert)
            {
                if (updateValues.IsValid(salary))
                {
                    salary.HRS_SAL_ID = SALID++;
                    salaryList.Add(salary);
                }
                else
                {
                    updateValues.SetErrorText(salary, "salary ammount req");
                }
            }

            foreach (var salary in updateValues.Update)
            {
                if (updateValues.IsValid(salary))
                {
                    var existingSalary = salaryList.FirstOrDefault(x => x.HRS_SAL_ID == salary.HRS_SAL_ID);
                    if (existingSalary != null)
                    {
                        existingSalary.HRS_SAL_SalaryAmount = salary.HRS_SAL_SalaryAmount;
                        existingSalary.HRS_SAL_StartDate = salary.HRS_SAL_StartDate;
                        existingSalary.HRS_SAL_EndDate = salary.HRS_SAL_EndDate;
                        existingSalary.HRS_SAL_UpdateDate = DateTime.Now; 
                    }


                }
                else
                    updateValues.SetErrorText(salary, "salary ammount req");
            }

            foreach (var salaryID in updateValues.DeleteKeys)
            {
                DeleteSalary(salaryID, updateValues);
            }
     
            return PartialView("_PartialSalGridView", salaryList);
        }

        protected void InsertSalary(HRS_SAL_Salaries salary, MVCxGridViewBatchUpdateValues<HRS_SAL_Salaries, int> updateValues,int id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    salary.HRS_SAL_EMPID=id;
                    HRS_SAL_Salaries.AddNew(salary, _accountingDbContext);
                }
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(salary, e.Message);
            }
        }

        protected void UpdateSalary(HRS_SAL_Salaries salary, MVCxGridViewBatchUpdateValues<HRS_SAL_Salaries, int> updateValues,int id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    salary.HRS_SAL_EMPID = id;
                    HRS_SAL_Salaries.Edit(salary, _accountingDbContext);
                }

            }
            catch (Exception e)
            {
                updateValues.SetErrorText(salary, e.Message);
            }
        }

        protected void DeleteSalary(int salaryID, MVCxGridViewBatchUpdateValues<HRS_SAL_Salaries, int> updateValues)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HRS_SAL_Salaries.Delete(salaryID, _accountingDbContext);
                }
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(salaryID, e.Message);

            }
        }

        public ActionResult ViewSalReport()
        {

            return View(new XtraReport4());
        }

        #endregion


        /// //////////////////////////////////////////////////////////////
       
        
        [HttpGet]
        public ActionResult PartialEmpcontractEditForm(int empid) {

            List<HRS_EMC_EmpContract> contract = HRS_EMC_EmpContract.GetByEmpId(empid, _accountingDbContext);
            return PartialView("_PartialEmpcontractEditForm", contract);
        
        }


        [HttpPost]
        public ActionResult ContractBatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<HRS_EMC_EmpContract, int> updateValues)
        {
            foreach (var contract in updateValues.Insert)
            {
                if (updateValues.IsValid(contract))
                    InsertContract(contract, updateValues);
            }
            foreach (var contract in updateValues.Update)
            {
                if (updateValues.IsValid(contract))
                    UpdateContract(contract, updateValues);
            }
            foreach (var contractID in updateValues.DeleteKeys)
            {
                DeleteContract(contractID, updateValues);
            }
            return PartialView("_PartialEmpcontractEditForm", HRS_EMC_EmpContract.Get(_accountingDbContext));
        }


        protected void InsertContract(HRS_EMC_EmpContract contract, MVCxGridViewBatchUpdateValues<HRS_EMC_EmpContract, int> updateValues)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HRS_EMC_EmpContract.AddNew(contract, _accountingDbContext);

                }
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(contract, e.Message);
            }
        }

        protected void UpdateContract(HRS_EMC_EmpContract contract, MVCxGridViewBatchUpdateValues<HRS_EMC_EmpContract, int> updateValues)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HRS_EMC_EmpContract.Edit(contract, _accountingDbContext);
                }

            }
            catch (Exception e)
            {
                updateValues.SetErrorText(contract, e.Message);
            }
        }

        protected void DeleteContract(int contractID, MVCxGridViewBatchUpdateValues<HRS_EMC_EmpContract, int> updateValues)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HRS_EMC_EmpContract.Delete(contractID, _accountingDbContext);
                }
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(contractID, e.Message);

            }
        }
    }
}
