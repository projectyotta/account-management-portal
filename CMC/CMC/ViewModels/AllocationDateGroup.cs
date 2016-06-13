using System;
using System.ComponentModel.DataAnnotations;

namespace CMC.ViewModels
{
    public class AllocationDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? AllocationDate { get; set; }

        public int ProjectCount { get; set; }
    }
}