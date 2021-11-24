using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apartment_Management.Models
{
    public class Building
    {
        public int BuildingID { get; set; }
        public int BranchID { get; set; }
        public string BuildingName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual List<Apartment> Apartments { get; set; }
        public virtual List<BuildingEmployees> BuildingEmployees { get; set; }
    }
}