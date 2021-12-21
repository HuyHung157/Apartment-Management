using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apartment_Management.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public Role Role { get; set; }
        [DisplayName("User name")]
        [Required(ErrorMessage = "This field is required")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public int PhoneNumber { get; set; }
        public int IdCard { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual List<BuildingEmployees> BuildingEmployees { get; set; }
    }
}