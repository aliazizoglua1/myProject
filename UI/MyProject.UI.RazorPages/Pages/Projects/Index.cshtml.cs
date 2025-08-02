using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IProjectService projectService, ILogger<IndexModel> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        public IEnumerable<ProjectDto> Projects { get; set; } = new List<ProjectDto>();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Projects = await _projectService.GetAllProjectsAsync();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving projects");
                TempData["ErrorMessage"] = "Unable to load projects. Please check if the API is running.";
                return Page();
            }
        }
    }
}