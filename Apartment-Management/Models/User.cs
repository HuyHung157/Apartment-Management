using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apartment_Management.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int ApartmentDetailID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public int PhoneNumber { get; set; }
        public int IdCard { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual List<ApartmentDetail> ApartmentDetail { get; set; }
    }
}