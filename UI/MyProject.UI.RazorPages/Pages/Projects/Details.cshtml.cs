using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(IProjectService projectService, ILogger<DetailsModel> logger)
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
                _logger.LogError(ex, "Error retrieving project details, ID: {ProjectId}", id);
                return NotFound();
            }
        }
    }
}