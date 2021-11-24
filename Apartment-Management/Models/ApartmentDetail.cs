using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apartment_Management.Models
{
    public class ApartmentDetail
    {
        public int ApartmentDetailID { get; set; }
        public int ApartmentID { get; set; }
        public int UserID { get; set; }
        public bool IsHost { get; set; }
        public string Relationship { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual Apartment Apartment { get; set; }
        public virtual User User { get; set; }
    }
}