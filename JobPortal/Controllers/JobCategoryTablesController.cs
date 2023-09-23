using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseLayer;

namespace JobPortal.Controllers
{
    public class JobCategoryTablesController : Controller
    {
        private JobPortalDbEntities db = new JobPortalDbEntities();

        /// <summary>
        /// GET: JobCategoryTables
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult Index()
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                return View(db.JobCategoryTables.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }
        /// <summary>
        /// GET: JobCategoryTables/Create
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult Create()
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                return View(new JobCategoryTable());
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// POST: JobCategoryTables/Create
        /// </summary>
        /// <param name="jobCategoryTable"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobCategoryTable jobCategoryTable)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                if (ModelState.IsValid)
                {
                    db.JobCategoryTables.Add(jobCategoryTable);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(jobCategoryTable);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// GET:Edit JobCategoryTables
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult Edit(int? id)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                JobCategoryTable jobCategoryTable = db.JobCategoryTables.Find(id);
                if (jobCategoryTable == null)
                {
                    return HttpNotFound();
                }
                return View(jobCategoryTable);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// POST: Edit JobCategoryTables
        /// </summary>
        /// <param name="jobCategoryTable"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( JobCategoryTable jobCategoryTable)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                if (ModelState.IsValid)
                {
                    db.Entry(jobCategoryTable).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(jobCategoryTable);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// to release unmanaged resources and perform cleanup operations when an object is no longer needed
        ///  this code dealing with objects that need cleanup.
        /// </summary>
        /// <param></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
