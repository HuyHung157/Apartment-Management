using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apartment_Management.Models
{
    public enum Role
    {
        ADMIN,
        MANAGER,
        STAFF
    }

    public enum ServiceStatus
    {
        COMPLETED,
        IN_COMPLETED,
    }
}