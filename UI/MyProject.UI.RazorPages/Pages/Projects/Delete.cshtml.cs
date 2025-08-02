using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(IProjectService projectService, ILogger<DeleteModel> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        public ProjectDto? Project { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                Project = await _projectService.GetProjectAsync(id);
                if (Project == null)
                {
                    return NotFound();
                }

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving project for deletion, ID: {ProjectId}", id);
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try
            {
                var deleted = await _projectService.DeleteProjectAsync(id);
                if (deleted)
                {
                    TempData["SuccessMessage"] = "Project deleted successfully!";
                    return RedirectToPage("./Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Project not found or could not be deleted.";
                    return RedirectToPage("./Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting project, ID: {ProjectId}", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the project.";
                return RedirectToPage("./Index");
            }
        }
    }
}