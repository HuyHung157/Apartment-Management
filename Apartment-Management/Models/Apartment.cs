using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apartment_Management.Models
{
    public class Apartment
    {
        public int ApartmentID { get; set; }
        public int BuildingID { get; set; }
        public string ApartmentName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual Building Building { get; set; }
    }
}