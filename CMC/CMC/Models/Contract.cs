using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;


namespace CMC.Models
{
    public class Contract
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int ContractID { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        public int value { get; set; }
        public string currency { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public Boolean Status { get; set; }


        public int JobID { get; set; }


        public virtual Job Job { get; set; }
        public virtual ICollection<Allocation> Allocations { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}