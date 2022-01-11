
using System.ComponentModel.DataAnnotations;

namespace Apartment_Management.Models
{
    public class Apartment
    {
        [Display(Name = "Mã căn hộ")]
        public int ApartmentID { get; set; }
        [Display(Name = "Mã tòa nhà")]
        public int BuildingID { get; set; }
        [Display(Name = "Tên căn hộ")]
        public string ApartmentName { get; set; }
        [Display(Name = "Ghi chú")]
        public string Description { get; set; }
        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual Building Building { get; set; }
    }
}