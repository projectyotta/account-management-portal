using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMC.ViewModels
{
    public class AssignedContractData
    {
        public int ContractID { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}