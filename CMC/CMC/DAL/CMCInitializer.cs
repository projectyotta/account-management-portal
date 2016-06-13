using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CMC.Models;

namespace CMC.DAL
{
    public class CMCInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CMCContext>
    {
        protected override void Seed(CMCContext context)
        {
            var projects = new List<Project>
            {
                new Project{WON="123",Name="Sample Project 1",Description="Description about 1",StartDate=DateTime.Parse("04-05-2004"),EndDate=DateTime.Parse("09-05-2006"),Status=Convert.ToBoolean(1)},
                new Project{WON="1",Name="Sample Project 2",Description="Description about 2",StartDate=DateTime.Parse("04-05-2004"),EndDate=DateTime.Parse("09-05-2006"),Status=Convert.ToBoolean(0)},
                new Project{WON="2",Name="Sample Project 3",Description="Description about 3",StartDate=DateTime.Parse("04-05-2004"),EndDate=DateTime.Parse("09-05-2006"),Status=Convert.ToBoolean(1)},
                new Project{WON="3",Name="Sample Project 4",Description="Description about 4",StartDate=DateTime.Parse("09-05-2004"),EndDate=DateTime.Parse("09-02-2009"),Status=Convert.ToBoolean(1)},
                new Project{WON="345",Name="Sample Project 5",Description="Description about 5",StartDate=DateTime.Parse("09-05-2004"),EndDate=DateTime.Parse("09-02-2009"),Status=Convert.ToBoolean(0)},
                new Project{WON="1234",Name="Sample Project 6",Description="Description about 6",StartDate=DateTime.Parse("09-05-2004"),EndDate=DateTime.Parse("09-02-2009"),Status=Convert.ToBoolean(0)},
                new Project{WON="1237",Name="Sample Project 7",Description="Description about 7",StartDate=DateTime.Parse("01-05-2004"),EndDate=DateTime.Parse("09-05-2007"),Status=Convert.ToBoolean(1)},
                new Project{WON="1239",Name="Sample Project 8",Description="Description about 8",StartDate=DateTime.Parse("01-05-2004"),EndDate=DateTime.Parse("09-05-2007"),Status=Convert.ToBoolean(1)},

            
            };
            projects.ForEach(s => context.Projects.Add(s));
            context.SaveChanges();

            var contracts = new List<Contract>
            {
                new Contract {ContractID=1050,Name="Contract 1",value=99,currency="USD",StartDate=DateTime.Parse("04-05-2004"),EndDate=DateTime.Parse("09-05-2004"),Status=Convert.ToBoolean(1)},
                new Contract {ContractID=4022,Name="Contract 2",value=123,currency="USD",StartDate=DateTime.Parse("04-05-2004"),EndDate=DateTime.Parse("09-05-2004"),Status=Convert.ToBoolean(0)},
                new Contract {ContractID=4041,Name="Contract 3",value=563,currency="USD",StartDate=DateTime.Parse("04-05-2004"),EndDate=DateTime.Parse("09-05-2004"),Status=Convert.ToBoolean(0)},
                new Contract {ContractID=1045,Name="Contract 4",value=213,currency="GBP",StartDate=DateTime.Parse("09-05-2004"),EndDate=DateTime.Parse("09-05-2006"),Status=Convert.ToBoolean(1)},
                new Contract {ContractID=3141,Name="Contract 5",value=967,currency="GBP",StartDate=DateTime.Parse("09-05-2004"),EndDate=DateTime.Parse("09-05-2006"),Status=Convert.ToBoolean(0)},
                new Contract {ContractID=2021,Name="Contract 6",value=542,currency="GBP",StartDate=DateTime.Parse("09-05-2004"),EndDate=DateTime.Parse("09-05-2006"),Status=Convert.ToBoolean(0)},
                new Contract {ContractID=2042,Name="Contract 7",value=912,currency="EUR",StartDate=DateTime.Parse("03-05-2004"),EndDate=DateTime.Parse("09-05-2004"),Status=Convert.ToBoolean(1)},
            };

            contracts.ForEach(s => context.Contracts.Add(s));
            context.SaveChanges();

            var allocations = new List<Allocation>
            {
                new Allocation{ ProjectID=1,ContractID=1050},
                new Allocation{ ProjectID=1,ContractID=4022},
                new Allocation{ ProjectID=1,ContractID=4041},
                new Allocation{ ProjectID=2,ContractID=1045},
                new Allocation{ ProjectID=2,ContractID=3141},
                new Allocation{ ProjectID=2,ContractID=2021},
                new Allocation{ ProjectID=3,ContractID=1050},
                new Allocation{ ProjectID=4,ContractID=4022},
                new Allocation{ ProjectID=4,ContractID=4041},
                new Allocation{ ProjectID=4,ContractID=3141},
            };
            allocations.ForEach(s => context.Allocations.Add(s));
            context.SaveChanges();







        }
    }

}

