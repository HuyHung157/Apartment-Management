using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Apartment_Management.Models
{
    public class ServiceType
    {
        public int ServiceTypeID { get; set; }
        [DisplayName("Loại dịch vụ")]
        public string ServiceTypeName { get; set; }
        [DisplayName("Đơn giá")]
        public double UnitPrice { get; set; }
        [DisplayName("Đơn vị tính")]
        public string Unit { get; set; }
        [DisplayName("Năm")]
        public int Year { get; set; }
        [DisplayName("Tháng")]
        public int Month { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public virtual List<ServiceDetail> ServiceDetails { get; set; }
    }
}