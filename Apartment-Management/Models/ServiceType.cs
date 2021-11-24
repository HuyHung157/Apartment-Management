using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apartment_Management.Models
{
    public class ServiceType
    {
        public int ServiceTypeID { get; set; }
        public string ServiceTypeName { get; set; }
        public double UnitPrice { get; set; }
        public string Unit { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual List<ServiceDetail> ServiceDetails { get; set; }
    }
}