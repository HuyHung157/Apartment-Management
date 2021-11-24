using Apartment_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Apartment_Management.Context
{
    public class AppContext : DbContext
    {
        public DbSet<Apartment> Apartment { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<ApartmentDetail> ApartmentDetail { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<BuildingEmployees> BuildingEmployees { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ServiceDetail> ServiceDetails { get; set; }

    }
}