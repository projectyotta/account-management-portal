using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace CMC.Models
{
    public class Allocation
    {
        public int AllocationID { get; set; }
        public int ContractID { get; set; }
        public int ProjectID { get; set; }

        public virtual Contract Contract { get; set; }
        public virtual Project Project { get; set; }
    }
}