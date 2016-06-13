using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMC.DAL;
using CMC.Models;
using System.Data.Entity.Infrastructure;


namespace CMC.Controllers
{
    public class JobController : Controller
    {
        private CMCContext db = new CMCContext();

        // GET: Job
        public async Task<ActionResult> Index()
        {
            var jobs = db.Jobs.Include(j => j.Administrator);
            return View(await jobs.ToListAsync());
        }

        // GET: Job/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Job/Create
       [Authorize]
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName");
            return View();
        }

        // POST: Job/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "JobID,Name,EmployeeID")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", job.EmployeeID);
            return View(job);
        }

        // GET: Job/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", job.EmployeeID);
            return View(job);
        }

        // POST: Job/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, byte[] rowVersion)
        {
            string[] fieldsToBind = new string[] { "Name", "EmployeeID", "RowVersion" };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var jobToUpdate = await db.Jobs.FindAsync(id);
            if (jobToUpdate == null)
            {
                Job deletedJob = new Job();
                TryUpdateModel(deletedJob, fieldsToBind);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The department was deleted by another user.");
                ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", deletedJob.EmployeeID);
                return View(deletedJob);
            }

            if (TryUpdateModel(jobToUpdate, fieldsToBind))
            {
                try
                {
                    db.Entry(jobToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Job)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The department was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Job)databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name)
                            ModelState.AddModelError("Name", "Current value: "
                                + databaseValues.Name);
                       
                        if (databaseValues.EmployeeID != clientValues.EmployeeID)
                            ModelState.AddModelError("InstructorID", "Current value: "
                                + db.Employees.Find(databaseValues.EmployeeID).FirstName);
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                            + "was modified by another user after you got the original value. The "
                            + "edit operation was canceled and the current values in the database "
                            + "have been displayed. If you still want to edit this record, click "
                            + "the Save button again. Otherwise click the Back to List hyperlink.");
                        jobToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", jobToUpdate.EmployeeID);
            return View(jobToUpdate);
        }





























        // GET: Job/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(job);
        }




        // POST: Job/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Job job)
        {
            try
            {
                db.Entry(job).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new { concurrencyError = true, id = job.JobID });
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(job);
            }
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
