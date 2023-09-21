using Domain.Model.Entity;
using iRentApi.Context.Convention;
using iRentApi.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class IRentContext : DbContext
    {

        public IRentContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var cascadeDeleteConvention = new CascadeDeleteConvention();
            cascadeDeleteConvention.Apply(modelBuilder.Model);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WarehouseComment> Comments { get; set; }
        public DbSet<ContractModel> Contracts { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<RentedWarehouseInfo> RentedWarehouseInfos { get; set; }
        public DbSet<WarehouseComment> WarehouseComments { get; set; }
        public DbSet<WarehouseCommentLike> WarehouseCommentLikes { get; set; }
        public DbSet<WarehouseImage> WarehouseImages { get; set; }
    }
}
