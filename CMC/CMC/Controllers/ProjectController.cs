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
using PagedList;
using System.Data.Entity.Infrastructure;



namespace CMC.Controllers
{
    public class ProjectController : Controller
    {
        private CMCContext db = new CMCContext();

        // GET: Project
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //logic for sorting by name and paging 
            ViewBag.CurrentSort = sortOrder;
            //to sort by name
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //to sort by start date 
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            //to sort by end date 
            ViewBag.DateSortParm1 = sortOrder == "Date1" ? "date_desc1" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var projects = from s in db.Projects
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {

                projects = projects.Where(s => s.Name.Contains(searchString));
            }
            //switch cases for sorting by name, StartDate and EndDate
            switch (sortOrder)
            {
                case "name_desc":
                    projects = projects.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    projects = projects.OrderBy(s => s.StartDate);
                    break;
                case "date_desc":
                    projects = projects.OrderByDescending(s => s.StartDate);
                    break;
                case "Date1":
                    projects = projects.OrderBy(s => s.EndDate);
                    break;
                case "date_desc1":
                    projects = projects.OrderByDescending(s => s.EndDate);
                    break;
                default:
                    projects = projects.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);


            return View(projects.ToPagedList(pageNumber, pageSize));

        }


        // GET: Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        
        // GET: Project/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.



        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WON,Name,Description,StartDate,EndDate,Status")] Project project)
        {
            try
            {


                if (ModelState.IsValid)
                {
                


                    db.Projects.Add(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(project);
        }





















        // GET: Project/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projectToUpdate = db.Projects.Find(id);
            if (TryUpdateModel(projectToUpdate, "",
                new string[] { "WON", "Name", "Description", "StartDate", "EndDate", "Status" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /*dex*/)
                {
                    ModelState.AddModelError("", "Unable to save changes now , please try again later");
                }

            }
            return View(projectToUpdate);
        }


        // GET: Project/Delete/5
        [Authorize]
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Could not delete. Try deleting again";
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }


        // POST: Project/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Project project = db.Projects.Find(id);
                db.Projects.Remove(project);
                db.SaveChanges();

            }

            catch (RetryLimitExceededException /*dex*/)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
