using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Apartment_Management.Models
{
    public class ServiceDetail
    {
        public int ServiceDetailID { get; set; }
        [DisplayName("Căn hộ")]
        public int ApartmentID { get; set; }
        [DisplayName("Loại dịch vụ")]
        public int ServiceTypeID { get; set; }
        [DisplayName("Trạng thái")]
        public string Status { get; set; }
        [DisplayName("Số lượng")]
        public double Quantity { get; set; }
        [DisplayName("Thành tiền")]
        public decimal Amount { get; set; }
        [DisplayName("Năm")]
        public int Year { get; set; }
        [DisplayName("Tháng")]
        public int Month { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual Apartment Apartment { get; set; }
        public virtual ServiceType ServiceType { get; set; }
    }
}