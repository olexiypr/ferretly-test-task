using System.Reflection;
using Ferretly.TestTask.TimeTrackingApi.Entities;

namespace Ferretly.TestTask.TimeTrackingApi.DbContext;
using Microsoft.EntityFrameworkCore;

public class TimeTrackerDbContext(DbContextOptions<TimeTrackerDbContext> options) : DbContext(options)
{
    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityType> ActivityTypes { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(opt =>
        {
            opt.HasKey(r => r.Id);
            
            //Can be implemented using reflection
            opt.Property(r => r.DateCreated).ValueGeneratedOnAdd();
            opt.Property(r => r.DateModified).ValueGeneratedOnAddOrUpdate();
        });

        modelBuilder.Entity<ActivityType>(opt =>
        {
            opt.HasKey(a => a.Id);
            opt.Property(a => a.DateCreated).ValueGeneratedOnAdd();
            opt.Property(a => a.DateModified).ValueGeneratedOnAddOrUpdate();
        });

        modelBuilder.Entity<Project>(opt =>
        {
            opt.HasKey(a => a.Id);
            opt.Property(a => a.DateCreated).ValueGeneratedOnAdd();
            opt.Property(a => a.DateModified).ValueGeneratedOnAddOrUpdate();
        });

        modelBuilder.Entity<Employee>(opt =>
        {
            opt.HasKey(a => a.Id);
            opt.Property(a => a.DateCreated).ValueGeneratedOnAdd();
            opt.Property(a => a.DateModified).ValueGeneratedOnAddOrUpdate();
        });
        
        modelBuilder.Entity<Activity>(opt =>
        {
            opt.HasKey(x => x.Id);
            
            opt.HasOne(a => a.Project)
                .WithMany(project => project.Activities)
                .HasForeignKey(a => a.ProjectId);
            
            opt.HasOne(a => a.Employee)
                .WithMany(employee => employee.Activities)
                .HasForeignKey(a => a.EmployeeId);
            
            opt.HasOne(a => a.Role)
                .WithMany(role => role.Activities)
                .HasForeignKey(a => a.RoleId);
            
            opt.Property(a => a.DateCreated).ValueGeneratedOnAddOrUpdate();
            opt.Property(a => a.DateModified).ValueGeneratedOnAddOrUpdate();
        });
    }
}