using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apartment_Management.Models
{
    public class Building
    {
        [Display(Name = "Mã tòa nhà")]
        public int BuildingID { get; set; }
        [Display(Name = "Mã chi nhánh")]
        public int BranchID { get; set; }
        [Display(Name = "Tên tòa nhà")]
        public string BuildingName { get; set; }
        [Display(Name = "Ghi chú")]
        public string Description { get; set; }
        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual List<Apartment> Apartments { get; set; }
        public virtual List<BuildingEmployees> BuildingEmployees { get; set; }
    }
}