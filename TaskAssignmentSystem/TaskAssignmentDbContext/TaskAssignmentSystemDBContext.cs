using Microsoft.EntityFrameworkCore;
using TaskAssignmentSystem.Models.Employees;
using TaskAssignmentSystem.Models.TaskAssignment;
using TaskAssignmentSystem.Models.Tasks;

namespace TaskAssignmentSystem.TaskAssignmentDbContext
{
    public class TaskAssignmentSystemDBContext : DbContext
    {
        public TaskAssignmentSystemDBContext(DbContextOptions<TaskAssignmentSystemDBContext> options) : base(options)
        {
        }

        #region Employees Region

        public DbSet<EmployeeModel> Employees { get; set; }

        #endregion

        #region Tasks Region

        public DbSet<TaskModel> Tasks { get; set; }

        #endregion

        #region Task Assignmet Region

        public DbSet<TaskAssignmentModel> TaskAssignments { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskAssignmentModel>()
                .HasKey(ta => new { ta.TaskId, ta.EmployeeId });

            modelBuilder.Entity<TaskAssignmentModel>()
                .HasOne(ta => ta.Task)
                .WithMany(t => t.TaskAssignments)
                .HasForeignKey(ta => ta.TaskId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete to delete related TaskAssignments

            modelBuilder.Entity<TaskAssignmentModel>()
                .HasOne(ta => ta.Employee)
                .WithMany(e => e.TaskAssignments)
                .HasForeignKey(ta => ta.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete to delete related TaskAssignments

        }

    }
}
