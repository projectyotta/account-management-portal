using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMC.DAL;
using CMC.Models;
using System.Data.Entity.Infrastructure;


namespace CMC.Controllers
{
    public class ContractController : Controller
    {
        private CMCContext db = new CMCContext();

        // GET: Contract
        public ActionResult Index()
        {
            var contracts = db.Contracts.Include(c => c.Job);
            return View(contracts.ToList());
        }

        // GET: Contract/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        [Authorize]
        public ActionResult Create()
        {
            PopulateJobsDropDownList();
            return View();
        }
        //create httppost 

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractID,Name,JobID")]Contract contract)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Contracts.Add(contract);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error 
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }
            PopulateJobsDropDownList(contract.JobID);
            return View(contract);
        }


        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            PopulateJobsDropDownList(contract.JobID);
            return View(contract);
        }



        //edit httppost
        [Authorize]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contractToUpdate = db.Contracts.Find(id);
            if (TryUpdateModel(contractToUpdate, "",
               new string[] { "Name", "JobID" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateJobsDropDownList(contractToUpdate.JobID);
            return View(contractToUpdate);
        }

        private void PopulateJobsDropDownList(object selectedJob = null)
        {
            var jobsQuery = from d in db.Jobs
                            orderby d.Name
                            select d;
            ViewBag.JobID = new SelectList(jobsQuery, "JobID", "Name", selectedJob);
        }



        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        
        // POST: Contract/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contract contract = db.Contracts.Find(id);
            db.Contracts.Remove(contract);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
