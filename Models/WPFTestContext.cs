using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WPFCRUDSample.Models
{
    public partial class WPFTestContext : DbContext
    {
        public WPFTestContext()
        {
        }

        public WPFTestContext(DbContextOptions<WPFTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString);
            }
        }

    }
}
