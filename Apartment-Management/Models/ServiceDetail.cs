using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apartment_Management.Models
{
    public class ServiceDetail
    {
        public int ServiceDetailID { get; set; }
        public int ApartmentID { get; set; }
        public int ServiceTypeID { get; set; }
        public string Status { get; set; }
        public double Quantity { get; set; }
        public decimal Amount { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual Apartment Apartment { get; set; }
        public virtual ServiceType ServiceType { get; set; }
    }
}