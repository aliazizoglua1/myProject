using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MyProject.UI.ProjectManagementAdminPanel;
using MyProject.UI.ProjectManagementAdminPanel.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7294/") });

builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IUserService, UserService>();
// Add MudBlazor services
builder.Services.AddMudServices();
//builder.Services.AddScoped<IProjectService, ProjectService>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IOrganizationUserService, OrganizationUserService>();
//builder.Services.AddScoped<IResourceService, ResourceService>();
//builder.Services.AddScoped<ITaskService, TaskService>();
//builder.Services.AddScoped<IRiskService, RiskService>();
//builder.Services.AddScoped<IIssueService, IssueService>();
//builder.Services.AddScoped<IMilestoneService, MilestoneService>();
//builder.Services.AddScoped<IChangeRequestService, ChangeRequestService>();
//builder.Services.AddScoped<ITaskTimeLogService, TaskTimeLogService>();
//builder.Services.AddScoped<ITaskHistoryService, TaskHistoryService>();
//builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
await builder.Build().RunAsync();
