using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextoptions) : base(dbContextoptions)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            // между отделом и родительским
            modelBuilder.Entity<Department>()
                .HasOne(pd => pd.ParentDepartment)
                .WithMany(sd => sd.SubDepartments)
                .HasForeignKey(pdi => pdi.ParentDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // между отделами и должностями
            modelBuilder.Entity<Position>()
                .HasOne(d => d.Department)
                .WithMany(p => p.Positions)
                .HasForeignKey(di => di.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // между сотрудниками и должностями
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.Position)
                .WithMany(e => e.Employees)
                .HasForeignKey(pi => pi.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            // между работниками и отделами
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

