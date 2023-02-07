using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication3.ViewModels;

namespace WebApplication3.Model
{



    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        public AuthDbContext(IConfiguration configuration, DbContextOptions<AuthDbContext> options)
        : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("AuthConnectionString"); 
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().Property(e => e.Full_Name);
            modelBuilder.Entity<ApplicationUser>().Property(e => e.Credit_Card);
            modelBuilder.Entity<ApplicationUser>().Property(e => e.Gender);
            modelBuilder.Entity<ApplicationUser>().Property(e => e.Mobile_No);
            modelBuilder.Entity<ApplicationUser>().Property(e => e.Delivery_Address);
            modelBuilder.Entity<ApplicationUser>().Property(e => e.Image).HasMaxLength(50);
            modelBuilder.Entity<ApplicationUser>().Property(e => e.AboutMe);
        }
    }




}
