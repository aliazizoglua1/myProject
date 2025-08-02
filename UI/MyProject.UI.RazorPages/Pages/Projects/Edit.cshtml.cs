using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly ILogger<EditModel> _logger;

        public EditModel(IProjectService projectService, ILogger<EditModel> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [BindProperty]
        public UpdateProjectDto Project { get; set; } = new UpdateProjectDto();

        public Guid ProjectId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var project = await _projectService.GetProjectAsync(id);
                if (project == null)
                {
                    return NotFound();
                }

                ProjectId = id;
                
                // Map ProjectDto to UpdateProjectDto
                Project = new UpdateProjectDto
                {
                    ProjectName = project.ProjectName,
                    ProjectType = project.ProjectType,
                    IndustryDomain = project.IndustryDomain,
                    PlannedStartDate = project.PlannedStartDate,
                    PlannedEndDate = project.PlannedEndDate,
                    ActualStartDate = project.ActualStartDate,
                    ActualEndDate = project.ActualEndDate,
                    PlannedBudget = project.PlannedBudget,
                    ActualBudget = project.ActualBudget,
                    ProjectManagerId = project.ProjectManagerId,
                    TeamSize = project.TeamSize,
                    ContractType = project.ContractType,
                    ProjectStatus = project.ProjectStatus,
                    ProjectComplexity = project.ProjectComplexity,
                    Description = project.Description,
                    Notes = project.Notes,
                    CurrentPhase = project.CurrentPhase,
                    OrganizationId = project.OrganizationId
                };

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving project for edit, ID: {ProjectId}", id);
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                ProjectId = id;
                return Page();
            }

            try
            {
                var updatedProject = await _projectService.UpdateProjectAsync(id, Project);
                TempData["SuccessMessage"] = "Project updated successfully!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating project, ID: {ProjectId}", id);
                ModelState.AddModelError(string.Empty, "An error occurred while updating the project. Please try again.");
                ProjectId = id;
                return Page();
            }
        }
    }
}