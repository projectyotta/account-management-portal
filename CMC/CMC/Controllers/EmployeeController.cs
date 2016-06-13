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
using CMC.ViewModels;
using System.Data.Entity.Infrastructure;



namespace CMC.Controllers
{
    public class EmployeeController : Controller
    {
        private CMCContext db = new CMCContext();



        // GET: Employee
        public ActionResult Index(int? id, int? ContractID)
        {
            var viewModel = new EmployeeIndexData();
            viewModel.Employees = db.Employees
                .Include(i => i.Contracts.Select(c => c.Job))
                .OrderBy(i => i.ID);

            if (id != null)
            {
                ViewBag.EmployeeID = id.Value;
                viewModel.Contracts = viewModel.Employees.Where(
                    i => i.ID == id.Value).Single().Contracts;
            }

            if (ContractID != null)
            {
                ViewBag.ContractID = ContractID.Value;

                //commented out next three lines because doing using explicit loading 
                // viewModel.Allocations = viewModel.Contracts.Where(
                //   x => x.ContractID == ContractID).Single().Allocations;
                //explicit loading 
                var selectedContract = viewModel.Contracts.Where(x => x.ContractID == ContractID).Single();
                db.Entry(selectedContract).Collection(x => x.Allocations).Load();
                foreach (Allocation allocation in selectedContract.Allocations)
                {
                    db.Entry(allocation).Reference(x => x.Project).Load();
                }

                viewModel.Allocations = selectedContract.Allocations;

            }

            return View(viewModel);
        }



        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           

            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }


        // GET: Employee/Create
        [Authorize]
        public ActionResult Create()
        {
            var employee = new Employee();
            employee.Contracts = new List<Contract>();
            PopulateAssignedContractData(employee);
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,FirstName,LastName,Email,PhoneNumber,OfficeNumber,Type,Status")] Employee employee, string[] selectedContracts)
        {

            if (selectedContracts != null)
            {
                employee.Contracts = new List<Contract>();
                foreach (var contract in selectedContracts)
                {
                    var contractToAdd = db.Contracts.Find(int.Parse(contract));
                    employee.Contracts.Add(contractToAdd);
                }
            }



            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedContractData(employee);
            return View(employee);

        }



        // GET: Employee/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees
                .Include(i => i.Contracts)
                .Where(i => i.ID == id)
                .Single();
            PopulateAssignedContractData(employee);

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }


        private void PopulateAssignedContractData(Employee employee)
        {
            var allContracts = db.Contracts;
            var employeeContracts = new HashSet<int>(employee.Contracts.Select(c => c.ContractID));
            var viewModel = new List<AssignedContractData>();
            foreach (var contract in allContracts)
            {
                viewModel.Add(new AssignedContractData
                {
                    ContractID = contract.ContractID,
                    Name = contract.Name,
                    Assigned = employeeContracts.Contains(contract.ContractID)
                });
            }
            ViewBag.Contracts = viewModel;
        }




        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedContracts)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employeeToUpdate = db.Employees
               .Include(i => i.Contracts)
               .Where(i => i.ID == id)
               .Single();

            if (TryUpdateModel(employeeToUpdate, "", new string[] { "EmployeeID", "FirstName", "LastName", "Email", "PhoneNumber", "OfficeNumber", "Type", "Status" }))
            {



                UpdateEmployeeContracts(selectedContracts, employeeToUpdate);

                db.SaveChanges();

                return RedirectToAction("Index");
           
            } 


                // to populate assigned contract data 
                PopulateAssignedContractData(employeeToUpdate);
                return View(employeeToUpdate);

            }
        







        private void UpdateEmployeeContracts(string[] selectedContracts, Employee employeeToUpdate)
        {
            if (selectedContracts == null)
            {
                employeeToUpdate.Contracts = new List<Contract>();
                return;
            }

            var selectedContractsHS = new HashSet<string>(selectedContracts);
            var employeeContracts = new HashSet<int>
                (employeeToUpdate.Contracts.Select(c => c.ContractID));
            foreach (var contract in db.Contracts)
            {
                if (selectedContractsHS.Contains(contract.ContractID.ToString()))
                {
                    if (!employeeContracts.Contains(contract.ContractID))
                    {
                        employeeToUpdate.Contracts.Add(contract);
                    }
                }
                else
                {
                    if (employeeContracts.Contains(contract.ContractID))
                    {
                        employeeToUpdate.Contracts.Remove(contract);
                    }
                }
            }
        }


        // GET: Employee/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]  
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees
                .Where(i => i.ID == id)
                .Single();

            db.Employees.Remove(employee); 

            var job = db.Jobs
                .Where(d => d.EmployeeID == id)
                .SingleOrDefault();
            if (job != null)
            {
                job.EmployeeID = null;
            }

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
