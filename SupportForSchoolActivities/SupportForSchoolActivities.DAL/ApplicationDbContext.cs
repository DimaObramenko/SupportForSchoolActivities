using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Parent> Parent { get; set; }
        public DbSet<SchoolClass> SchoolClass { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Homework> Homework { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Remark> Remark { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasOne<Parent>(s => s.Parent)
                .WithMany(p => p.Students)
                .OnDelete(DeleteBehavior.NoAction);
                entity.Navigation(e => e.Parent).IsRequired();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasOne<SchoolClass>(s => s.SchoolClass)
                .WithMany(c => c.Students)
                .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
