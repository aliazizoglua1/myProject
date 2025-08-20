using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyProject.Persistence.Configurations;
using MyProject.Persistence.Entities;
using Task = MyProject.Persistence.Entities.Task;
namespace MyProject.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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
        public DbSet<TaskHistory> TaskHistories { get; set; }

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
            modelBuilder.ApplyConfiguration(new TaskHistoryConfiguration());

            // Configure timestamp behavior for PostgreSQL
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // This is the global ValueConverter for all DateTime and DateTime? properties
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue ? (v.Value.Kind == DateTimeKind.Utc ? v : v.Value.ToUniversalTime()) : v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

            // Apply the converter to all DateTime and DateTime? properties in your model
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(dateTimeConverter);
                    }
                    else if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(nullableDateTimeConverter);
                    }
                }
            }
        }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // This is just for design-time configuration
                // In production, the connection string should be passed via constructor
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=ProjectAI;Username = myproject; Password = myproject;Search Path=ai;");
            }
        }
    }
} 