using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyProject.UI.ProjectManagementConsole;
using MyProject.UI.ProjectManagementConsole.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrganizationUserService, OrganizationUserService>();
builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IRiskService, RiskService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<IMilestoneService, MilestoneService>();
builder.Services.AddScoped<IChangeRequestService, ChangeRequestService>();
builder.Services.AddScoped<ITaskTimeLogService, TaskTimeLogService>();

await builder.Build().RunAsync();
