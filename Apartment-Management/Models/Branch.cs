using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apartment_Management.Models
{
    public class Branch
    {
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual List<Building> Buildings { get; set; }
    }
}