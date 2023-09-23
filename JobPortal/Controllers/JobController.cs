using DatabaseLayer;
using JobPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace JobPortal.Controllers
{
    public class JobController : Controller
    {
        //creating a new instance of the class to work with a specific database or data source.
        private JobPortalDbEntities db = new JobPortalDbEntities();

        /// <summary>
        /// Job Posting(Get)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult PostJob()
        {
            //checks if the "UserTypeID" session variable is either null or empty. 
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                var job = new PostJobMV();
                // dropdown list with data from the "JobCategoryTables" table in the database. 
                ViewBag.JobCategoryID = new SelectList(
                    db.JobCategoryTables.ToList(),
                    "JobCategoryID",
                    "JobCategory",
                    "0");
                ViewBag.JobNatureID = new SelectList(
                     db.JobNatureTables.ToList(),
                    "JobNatureID",
                    "JobNature",
                    "0");
                return View(job);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// Job Posting(Post)  <summary>
        /// Job Posting(Post) 
        /// </summary>
        /// <param name="postJobMV"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostJob(PostJobMV postJobMV)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                int userid = 0;
                int companyid = 0;
                // to parse the string representation of the "UserID" from the session as an integer and store it in "userid".
                int.TryParse(Convert.ToString(Session["UserID"]), out userid);
                int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
                postJobMV.UserID = userid;
                postJobMV.CompanyID = companyid;
                if (ModelState.IsValid)
                {
                    var post = new PostJobTable();
                    post.UserID = postJobMV.UserID;
                    post.CompanyID = postJobMV.CompanyID;
                    post.JobCategoryID = postJobMV.JobCategoryID;
                    post.JobTitle = postJobMV.JobTitle;
                    post.JobDescription = postJobMV.JobDescription;
                    post.MinSalary = postJobMV.MinSalary;
                    post.MaxSalary = postJobMV.MaxSalary;
                    post.Location = postJobMV.Location;
                    post.Vacancy = postJobMV.Vacancy;
                    post.JobNatureID = postJobMV.JobNatureID;
                    post.PostDate = DateTime.Now;
                    post.ApplicationLastDate = postJobMV.ApplicationLastDate;
                    post.LastDate = postJobMV.ApplicationLastDate;
                    post.JobStatusID = 1;
                    post.WebUrl = postJobMV.WebUrl;
                    db.PostJobTables.Add(post);
                    db.SaveChanges();
                    return RedirectToAction("CompanyJobsList");
                }
                var job = new PostJobMV();
                ViewBag.JobCategoryID = new SelectList(
                    db.JobCategoryTables.ToList(),
                    "JobCategoryID",
                    "JobCategory",
                    "0");
                ViewBag.JobNatureID = new SelectList(
                     db.JobNatureTables.ToList(),
                    "JobNatureID",
                    "JobNature",
                    "0");
                return View(postJobMV);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// To Get Company Jobs List
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult CompanyJobsList()
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                int userid = 0;
                int companyid = 0;
                int.TryParse(Convert.ToString(Session["UserID"]), out userid);
                int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
                var allpost = db.PostJobTables.Where(c => c.CompanyID == companyid && c.UserID == userid).ToList();
                return View(allpost);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// To Get Pending Jobs List
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult AllCompanyPendingJobs()
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                int userid = 0;
                int companyid = 0;
                int.TryParse(Convert.ToString(Session["UserID"]), out userid);
                int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
                var allpost = db.PostJobTables.ToList();
                if (allpost.Count() > 0)
                {
                    allpost = allpost.OrderByDescending(o => o.PostJobID).ToList();
                }
                return View(allpost);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// To Get Job Requirements
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult AddRequirements(int? id)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                var details = db.JobRequirementDetailTables.Where(j => j.PostJobID == id).ToList();
                if (details.Count() > 0)
                {
                    details = details.OrderBy(r => r.JobRequirementID).ToList();
                }
                var requirements = new JobRequirementsMV();
                requirements.Details = details;
                requirements.PostJobID = (int)id;
                ViewBag.JobRequirementID = new SelectList(db.JobRequirementsTables.ToList(), "JobRequirementID", "JobRequirementTitle", "0");
                return View(requirements);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// For Adding Job Requirements(Post)
        /// </summary>
        /// <param name="jobRequirementsMV"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRequirements(JobRequirementsMV jobRequirementsMV)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                try
                {
                    var requiements = new JobRequirementDetailTable();
                    requiements.JobRequirementID = jobRequirementsMV.JobRequirementID;
                    requiements.JobRequirementDetails = jobRequirementsMV.JobRequirementDetails;
                    requiements.PostJobID = jobRequirementsMV.PostJobID;
                    db.JobRequirementDetailTables.Add(requiements);
                    db.SaveChanges();
                    return RedirectToAction("AddRequirements", new { id = requiements.PostJobID });
                }
                catch (Exception ex)
                {
                    var details = db.JobRequirementDetailTables.Where(j => j.PostJobID == jobRequirementsMV.PostJobID).ToList();
                    if (details.Count() > 0)
                    {
                        details = details.OrderBy(r => r.JobRequirementID).ToList();
                    }
                    jobRequirementsMV.Details = details;
                    ModelState.AddModelError("JobRequirementID", "Required!!!");
                }
                ViewBag.JobRequirementID = new SelectList(db.JobRequirementsTables.ToList(), "JobRequirementID", "JobRequirementTitle", jobRequirementsMV.JobRequirementID);
                return View(jobRequirementsMV);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// For Deleting Job Requirements
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult DeleteRequirements(int? id)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                var jobpostid = db.JobRequirementDetailTables.Find(id).PostJobID;
                var requirements = db.JobRequirementDetailTables.Find(id);
                db.Entry(requirements).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("AddRequirements", new { id = jobpostid });
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// For Deleting Posted Jobs from Job provider
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult DeleteJobPost(int? id)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                var jobpost = db.PostJobTables.Find(id);
                db.Entry(jobpost).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("CompanyJobsList");
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// For Deleting Posted Jobs from admin side 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult AdminDeleteJobPost(int? id)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                var jobpost = db.PostJobTables.Find(id);
                db.Entry(jobpost).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("AllCompanyPendingJobs");
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// To Approve Posted Job from Admin side
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult ApprovedPost(int? id)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                var jobpost = db.PostJobTables.Find(id);
                jobpost.JobStatusID = 2;
                db.Entry(jobpost).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllCompanyPendingJobs");
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// To Cancel Posted Job from Admin side
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult CancelledPost(int? id)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                var jobpost = db.PostJobTables.Find(id);
                jobpost.JobStatusID = 3;
                db.Entry(jobpost).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllCompanyPendingJobs");
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// To view posted job details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult JobDetails(int? id)
        {
            try
            {
                var getpostjob = db.PostJobTables.Find(id);
                var postjob = new PostJobDetailMV();
                postjob.PostJobID = getpostjob.PostJobID;
                postjob.Company = getpostjob.CompanyTable.CompanyName;
                postjob.JobCategory = getpostjob.JobCategoryTable.JobCategory;
                postjob.JobTitle = getpostjob.JobTitle;
                postjob.JobDescription = getpostjob.JobDescription;
                postjob.MinSalary = getpostjob.MinSalary;
                postjob.MaxSalary = getpostjob.MaxSalary;
                postjob.Location = getpostjob.Location;
                postjob.Vacancy = getpostjob.Vacancy;
                postjob.JobNature = getpostjob.JobNatureTable.JobNature;
                postjob.PostDate = getpostjob.PostDate;
                postjob.ApplicationLastDate = getpostjob.ApplicationLastDate;
                postjob.WebUrl = getpostjob.WebUrl;
                getpostjob.JobRequirementDetailTables = getpostjob.JobRequirementDetailTables.OrderBy(d => d.JobRequirementID).ToList();
                int jobrequirementid = 0;
                var jobrequirements = new JobRequirementsTableMV();

                foreach (var detail in getpostjob.JobRequirementDetailTables)
                {
                    var jobrequirementsdetails = new JobRequirementDetailMV();
                    if (jobrequirementid == 0)
                    {
                        jobrequirements.JobRequirementID = detail.JobRequirementID;
                        jobrequirements.JobRequirementTitle = detail.JobRequirementsTable.JobRequirementTitle;
                        jobrequirementsdetails.JobRequirementID = detail.JobRequirementID;
                        jobrequirementsdetails.JobRequirementDetails = detail.JobRequirementDetails;
                        jobrequirements.Details.Add(jobrequirementsdetails);
                        jobrequirementid = detail.JobRequirementID;
                    }
                    else if (jobrequirementid == detail.JobRequirementID)
                    {
                        jobrequirementsdetails.JobRequirementID = detail.JobRequirementID;
                        jobrequirementsdetails.JobRequirementDetails = detail.JobRequirementDetails;
                        jobrequirements.Details.Add(jobrequirementsdetails);
                        jobrequirementid = detail.JobRequirementID;
                    }
                    else if (jobrequirementid != detail.JobRequirementID)
                    {
                        postjob.Requirements.Add(jobrequirements);
                        jobrequirements = new JobRequirementsTableMV();
                        jobrequirements.JobRequirementID = detail.JobRequirementID;
                        jobrequirements.JobRequirementTitle = detail.JobRequirementsTable.JobRequirementTitle;
                        jobrequirementsdetails.JobRequirementID = detail.JobRequirementID;
                        jobrequirementsdetails.JobRequirementDetails = detail.JobRequirementDetails;
                        jobrequirements.Details.Add(jobrequirementsdetails);
                        jobrequirementid = detail.JobRequirementID;
                    }

                }
                postjob.Requirements.Add(jobrequirements);

                return View(postjob);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// To get admin approved jobs through filter
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult FilterJob()
        {

            try
            {
                var obj = new FilterJobMV();
                var date = DateTime.Now.Date;
                var result = db.PostJobTables.Where(r => r.ApplicationLastDate >= date && r.JobStatusID == 2).ToList();
                obj.Result = result;
                ViewBag.JobCategoryID = new SelectList(
                   db.JobCategoryTables.ToList(),
                   "JobCategoryID",
                   "JobCategory",
                   "0");
                ViewBag.JobNatureID = new SelectList(
                     db.JobNatureTables.ToList(),
                    "JobNatureID",
                    "JobNature",
                    "0");
                return View(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// To filter admin approved jobs 
        /// </summary>
        /// <param name="filterJobMV"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FilterJob(FilterJobMV filterJobMV)
        {
            try
            {
                var date = DateTime.Now.Date;
                var result = db.PostJobTables.Where(r => r.ApplicationLastDate >= date && r.JobStatusID == 2 && (r.JobCategoryID == filterJobMV.JobCategoryID || r.JobNatureID == filterJobMV.JobNatureID)).ToList();
                filterJobMV.Result = result;
                ViewBag.JobCategoryID = new SelectList(
                   db.JobCategoryTables.ToList(),
                   "JobCategoryID",
                   "JobCategory",
                   filterJobMV.JobCategoryID);
                ViewBag.JobNatureID = new SelectList(
                     db.JobNatureTables.ToList(),
                    "JobNatureID",
                    "JobNature",
                    filterJobMV.JobNatureID);
                return View(filterJobMV);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }
    }
}