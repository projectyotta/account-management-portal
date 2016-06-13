using CMC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace CMC.Models
{
    public class Employee
    {
        public int ID { get; set; }

        public int EmployeeID { get; set; }
        [Display(Name = "First Name"), StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name"), StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }


        [StringLength(320, MinimumLength = 4)]
        public string Email { get; set; }

        public Int64 PhoneNumber { get; set; }

        public Int32 OfficeNumber { get; set; }


        [RegularExpression("^(1|2)$", ErrorMessage = "Enter 1 for PM and 2 for TM ")]
        public int Type { get; set; }

        public Boolean Status { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
        



    }
}


