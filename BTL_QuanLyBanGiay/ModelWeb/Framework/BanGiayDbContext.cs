using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ModelWeb.Framework
{
    public partial class BanGiayDbContext : DbContext
    {
        public BanGiayDbContext()
            : base("name=BanGiayDbContext")
        {
        }

        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
