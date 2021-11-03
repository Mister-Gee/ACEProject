using ACE.Domain.Entities.ControlledEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Gender> Genders { get; set; }
        public DbSet<MaritalStatus> MaritalStatus { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<StudentCategory> StudentCategories { get; set; }
        public DbSet<Level> Levels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gender>().ToTable(nameof(Genders), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Programme>().ToTable(nameof(Programmes), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Religion>().ToTable(nameof(Religions), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Department>().ToTable(nameof(Departments), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<StudentCategory>().ToTable(nameof(StudentCategories), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Level>().ToTable(nameof(Levels), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<School>().ToTable(nameof(Schools), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<MaritalStatus>().ToTable(nameof(MaritalStatus), t => t.ExcludeFromMigrations());


        }


    }
}
