using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apartment_Management.Models
{
    public class Branch
    {

        [Display(Name = "Mã chi nhánh")]
        public int BranchID { get; set; }
        [Display(Name = "Tên chi nhánh")]
        public string BranchName { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Ghi chú")]
        public string Description { get; set; }
        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual List<Building> Buildings { get; set; }
    }
}