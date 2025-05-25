using InstallmentManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InstallmentManager.Infraestructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Installment> Installment { get; set; }
        public virtual DbSet<InstallmentAnticipation> InstallmentAnticipation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
