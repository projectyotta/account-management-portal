using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMC.Models
{
    public class Job
    {
        public int JobID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }


        public int? EmployeeID { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }


        public virtual Employee Administrator { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }

    }
}   