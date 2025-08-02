using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyProject.Persistence.Repositories;

namespace MyProject.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IRiskRepository, RiskRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<IChangeRequestRepository, ChangeRequestRepository>();
            services.AddScoped<IQualityAssuranceRepository, QualityAssuranceRepository>();

            return services;
        }
    }
} 