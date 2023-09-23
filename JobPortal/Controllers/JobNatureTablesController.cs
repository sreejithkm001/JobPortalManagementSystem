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
    public class JobNatureTablesController : Controller
    {
        private JobPortalDbEntities db = new JobPortalDbEntities();
        /// <summary>
        /// GET: JobNatureTables
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(db.JobNatureTables.ToList());
        }

        /// <summary>
        /// GET: JobNatureTables/Create
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            return View( new JobNatureTable());
        }

        /// <summary>
        /// POST: JobNatureTables/Create
        /// </summary>
        /// <param name="jobNatureTable"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobNatureID,JobNature")] JobNatureTable jobNatureTable)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                if (ModelState.IsValid)
                {
                    db.JobNatureTables.Add(jobNatureTable);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(jobNatureTable);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// GET:Edit JobNatureTables
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
                JobNatureTable jobNatureTable = db.JobNatureTables.Find(id);
                if (jobNatureTable == null)
                {
                    return HttpNotFound();
                }
                return View(jobNatureTable);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        /// <summary>
        /// POST:Edit JobNatureTables
        /// </summary>
        /// <param name="jobNatureTable"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobNatureID,JobNature")] JobNatureTable jobNatureTable)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                if (ModelState.IsValid)
                {
                    db.Entry(jobNatureTable).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(jobNatureTable);
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
