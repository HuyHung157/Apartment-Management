using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apartment_Management.Models
{
    public class BuildingEmployees
    {
        public int BuildingEmployeesID { get; set; }
        public int BuildingID { get; set; }
        public int EmployeeID { get; set; }
        public string RoleInBuilding { get; set; }
        public string OfficeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual Building Building { get; set; }
    }
}