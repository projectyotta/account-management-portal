namespace CMC.Migrations
{
    using CMC.Models;
    using CMC.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;


    internal sealed class Configuration : DbMigrationsConfiguration<CMCContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true; 
            
        }

        protected override void Seed(CMCContext context)
        {

            var projects = new List<Project>
            {
                new Project{WON="1", Name="Sample Project 1",Description="Description about 1",StartDate=DateTime.Parse("04-05-2004"),EndDate=DateTime.Parse("09-05-2006"),Status=Convert.ToBoolean(1)},
                new Project{WON="2",Name="Sample Project 2",Description="Description about 2",StartDate=DateTime.Parse("04-05-2004"),EndDate=DateTime.Parse("09-05-2006"),Status=Convert.ToBoolean(0)},
                new Project{WON="3",Name="Sample Project 3",Description="Description about 3",StartDate=DateTime.Parse("04-05-2004"),EndDate=DateTime.Parse("09-05-2006"),Status=Convert.ToBoolean(1)},
                new Project{WON="4",Name="Sample Project 4",Description="Description about 4",StartDate=DateTime.Parse("09-05-2004"),EndDate=DateTime.Parse("09-02-2009"),Status=Convert.ToBoolean(1)},
                new Project{WON="5",Name="Sample Project 5",Description="Description about 5",StartDate=DateTime.Parse("09-05-2004"),EndDate=DateTime.Parse("09-02-2009"),Status=Convert.ToBoolean(0)},
                new Project{WON="6",Name="Sample Project 6",Description="Description about 6",StartDate=DateTime.Parse("09-05-2004"),EndDate=DateTime.Parse("09-02-2009"),Status=Convert.ToBoolean(0)},
                new Project{WON="7",Name="Sample Project 7",Description="Description about 7",StartDate=DateTime.Parse("01-05-2004"),EndDate=DateTime.Parse("09-05-2007"),Status=Convert.ToBoolean(1)},
                new Project{WON="8",Name="Sample Project 8",Description="Description about 8",StartDate=DateTime.Parse("01-05-2004"),EndDate=DateTime.Parse("09-05-2007"),Status=Convert.ToBoolean(1)}

            };

            projects.ForEach(s => context.Projects.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var employees = new List<Employee>
            {
                new Employee { FirstName="Saurabh",LastName="Rao",Email="s@gmail.com",PhoneNumber=9966551234,OfficeNumber=443322,Type=2,Status=Convert.ToBoolean(1)},
                new Employee { FirstName="Kurt",LastName="Cobain",Email="k@gmail.com",PhoneNumber=9966567892,OfficeNumber=115478,Type=1,Status=Convert.ToBoolean(0)},
                new Employee { FirstName="Amy",LastName="Lee",Email="a@gmail.com",PhoneNumber=9874598745,OfficeNumber=457125,Type=1,Status=Convert.ToBoolean(0)},
                new Employee { FirstName="Benjamin",LastName="Burley",Email="b@gmail.com",PhoneNumber=8741548974,OfficeNumber=541258,Type=2,Status=Convert.ToBoolean(1)},
                new Employee { FirstName="Lana",LastName="Rey",Email="l@gmail.com",PhoneNumber=8745124587,OfficeNumber=258741,Type=1,Status=Convert.ToBoolean(0)},
            };
            employees.ForEach(s => context.Employees.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var jobs = new List<Job>
            {
                new Job{Name="Job 1",EmployeeID=employees.Single(i => i.LastName=="Lee").ID},
                new Job{Name="Job 2",EmployeeID=employees.Single(i => i.LastName=="Rao").ID},
                new Job{Name="Job 3",EmployeeID=employees.Single(i => i.LastName=="Cobain").ID},
                new Job{Name="Job 4",EmployeeID=employees.Single(i => i.LastName=="Burley").ID},
                new Job{Name="Job 5",EmployeeID=employees.Single(i => i.LastName=="Rey").ID}
            };
            jobs.ForEach(s => context.Jobs.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var contracts = new List<Contract>
            {
                new Contract {ContractID=1050, Name="Contract 1",value=99,currency="USD",StartDate=DateTime.Parse("04-05-2004"),EndDate=DateTime.Parse("09-05-2004"),Status=Convert.ToBoolean(1),JobID=jobs.Single(s => s.Name=="Job 1").JobID,Employees=new List<Employee>() },
                new Contract {ContractID=4022,Name="Contract 2",value=123,currency="USD",StartDate=DateTime.Parse("04-05-2004"),EndDate=DateTime.Parse("09-05-2004"),Status=Convert.ToBoolean(0),JobID=jobs.Single(s => s.Name=="Job 2").JobID,Employees=new List<Employee>() },
                new Contract {ContractID=4041,Name="Contract 3",value=563,currency="USD",StartDate=DateTime.Parse("04-05-2004"),EndDate=DateTime.Parse("09-05-2004"),Status=Convert.ToBoolean(0),JobID=jobs.Single(s => s.Name=="Job 3").JobID,Employees=new List<Employee>() },
                new Contract {ContractID=1045,Name="Contract 4",value=213,currency="GBP",StartDate=DateTime.Parse("09-05-2004"),EndDate=DateTime.Parse("09-05-2006"),Status=Convert.ToBoolean(1),JobID=jobs.Single(s => s.Name=="Job 4").JobID,Employees=new List<Employee>() },
                new Contract {ContractID=3141,Name="Contract 5",value=967,currency="GBP",StartDate=DateTime.Parse("09-05-2004"),EndDate=DateTime.Parse("09-05-2006"),Status=Convert.ToBoolean(0),JobID=jobs.Single(s => s.Name=="Job 5").JobID,Employees=new List<Employee>() },
                new Contract {ContractID=2021,Name="Contract 6",value=542,currency="GBP",StartDate=DateTime.Parse("09-05-2004"),EndDate=DateTime.Parse("09-05-2006"),Status=Convert.ToBoolean(0),JobID=jobs.Single(s => s.Name=="Job 1").JobID,Employees=new List<Employee>() },
                new Contract {ContractID=2042,Name="Contract 7",value=912,currency="EUR",StartDate=DateTime.Parse("03-05-2004"),EndDate=DateTime.Parse("09-05-2004"),Status=Convert.ToBoolean(1),JobID=jobs.Single(s => s.Name=="Job 2").JobID,Employees=new List<Employee>() },
            };

            contracts.ForEach(s => context.Contracts.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            AddOrUpdateEmployee(context, "Contract 1", "Lee");
            AddOrUpdateEmployee(context, "Contract 2", "Rao");
            AddOrUpdateEmployee(context, "Contract 3", "Burley");
            AddOrUpdateEmployee(context, "Contract 4", "Rey");
            AddOrUpdateEmployee(context, "Contract 5", "Cobain");
            AddOrUpdateEmployee(context, "Contract 6", "Lee");
            AddOrUpdateEmployee(context, "Contract 7", "Rey");

            context.SaveChanges();

            var allocations = new List<Allocation>
            {
                new Allocation{ ProjectID=projects.Single(s => s.Name == "Sample Project 1").ID,ContractID=contracts.Single(c => c.Name=="Contract 1").ContractID},
                new Allocation{ ProjectID=projects.Single(s => s.Name == "Sample Project 1").ID,ContractID=contracts.Single(c => c.Name=="Contract 2").ContractID},
                new Allocation{ ProjectID=projects.Single(s => s.Name == "Sample Project 1").ID,ContractID=contracts.Single(c => c.Name=="Contract 3").ContractID},
                new Allocation{ ProjectID=projects.Single(s => s.Name == "Sample Project 2").ID,ContractID=contracts.Single(c => c.Name=="Contract 4").ContractID},
                new Allocation{ ProjectID=projects.Single(s => s.Name == "Sample Project 2").ID,ContractID=contracts.Single(c => c.Name=="Contract 5").ContractID},
                new Allocation{ ProjectID=projects.Single(s => s.Name == "Sample Project 2").ID,ContractID=contracts.Single(c => c.Name=="Contract 6").ContractID},
                new Allocation{ ProjectID=projects.Single(s => s.Name == "Sample Project 3").ID,ContractID=contracts.Single(c => c.Name=="Contract 1").ContractID},
                new Allocation{ ProjectID=projects.Single(s => s.Name == "Sample Project 3").ID,ContractID=contracts.Single(c => c.Name=="Contract 2").ContractID},
                new Allocation{ ProjectID=projects.Single(s => s.Name == "Sample Project 4").ID,ContractID=contracts.Single(c => c.Name=="Contract 1").ContractID},
                new Allocation{ ProjectID=projects.Single(s => s.Name == "Sample Project 5").ID,ContractID=contracts.Single(c => c.Name=="Contract 7").ContractID},
                new Allocation{ ProjectID=projects.Single(s => s.Name == "Sample Project 6").ID,ContractID=contracts.Single(c => c.Name=="Contract 3").ContractID},
                
            };
            foreach (Allocation e in allocations)
            {
                var allocationInDataBase = context.Allocations.Where(
                    s =>
                         s.Project.ID == e.ProjectID &&
                         s.Contract.ContractID == e.ContractID).SingleOrDefault();
                if (allocationInDataBase == null)
                {
                    context.Allocations.Add(e);
                }
            }
            context.SaveChanges();
        }


        void AddOrUpdateEmployee(CMCContext context, string con_name, string EmployeeName)
        {
            var crs = context.Contracts.SingleOrDefault(c => c.Name == con_name);
            var inst = crs.Employees.SingleOrDefault(i => i.LastName == EmployeeName);
            if (inst == null)
                crs.Employees.Add(context.Employees.Single(i => i.LastName == EmployeeName));
        }

        // I have no idea why I'm including these comment lines , but yeah . 
        //  This method will be called after migrating to the latest version.

        //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //  to avoid creating duplicate seed data. E.g.
        //
        //    context.People.AddOrUpdate(
        //      p => p.FullName,
        //      new Person { FullName = "Andrew Peters" },
        //      new Person { FullName = "Brice Lambson" },
        //      new Person { FullName = "Rowan Miller" }
        //    );
        //


    }
}
