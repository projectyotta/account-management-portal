using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMC.Models;

namespace CMC.ViewModels
{
    public class EmployeeIndexData
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Contract> Contracts { get; set; }
        public IEnumerable<Allocation> Allocations { get; set; }

    }
}