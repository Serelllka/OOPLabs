using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;

namespace Reports.Server.Database
{
    public class ReportsDatabaseContext : DbContext
    {
        public ReportsDatabaseContext(DbContextOptions<ReportsDatabaseContext> options) 
            : base(options)
        {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            LoadData();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Entry> Entries { get; set; }
        
        private void LoadData()
        {
            Employees.Include("_subordinates").Load();
            Tasks
                .Include("_changes")
                .Include(item => item.AssignedEmployee)
                .Load();
            Entries.Load();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasMany("_subordinates");
            modelBuilder.Entity<TaskModel>().HasMany("_changes");
            modelBuilder.Entity<TaskModel>().HasOne(item => item.AssignedEmployee);
            modelBuilder.Entity<Report>().HasOne("_resolvedTask");
            
            base.OnModelCreating(modelBuilder);
        }
    }
}