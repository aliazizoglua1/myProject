using Microsoft.EntityFrameworkCore;
using MyProject.Persistence.Configurations;
using MyProject.Persistence.Entities;
using Task = MyProject.Persistence.Entities.Task;
namespace MyProject.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Risk> Risks { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<ChangeRequest> ChangeRequests { get; set; }
        public DbSet<QualityAssurance> QualityAssurances { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        public DbSet<TaskTimeLog> TaskTimeLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new RiskConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceConfiguration());
            modelBuilder.ApplyConfiguration(new IssueConfiguration());
            modelBuilder.ApplyConfiguration(new ChangeRequestConfiguration());
            modelBuilder.ApplyConfiguration(new QualityAssuranceConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MilestoneConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationUserConfiguration());
            modelBuilder.ApplyConfiguration(new TaskTimeLogConfiguration());

            // Configure the update trigger behavior
            modelBuilder.Entity<Project>()
                .Property(p => p.UpdatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<Risk>()
                .Property(r => r.UpdatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<Task>()
                .Property(t => t.UpdatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<Resource>()
                .Property(r => r.UpdatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<Issue>()
                .Property(i => i.UpdatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<ChangeRequest>()
                .Property(cr => cr.UpdatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<QualityAssurance>()
                .Property(qa => qa.UpdatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<Organization>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<Milestone>()
                .Property(m => m.UpdatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<OrganizationUser>()
                .Property(ou => ou.UpdatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<TaskTimeLog>()
                .Property(t => t.UpdatedAt)
                .HasDefaultValueSql("now()");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // This is just for design-time configuration
                // In production, the connection string should be passed via constructor
                optionsBuilder.UseNpgsql("Host=localhost;Database=your_database;Username=postgres;Password=your_password");
            }
        }
    }
} 