using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IProjectService projectService, ILogger<CreateModel> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [BindProperty]
        public CreateProjectDto Project { get; set; } = new CreateProjectDto();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var createdProject = await _projectService.CreateProjectAsync(Project);
                TempData["SuccessMessage"] = "Project created successfully!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating project");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the project. Please try again.");
                return Page();
            }
        }
    }
}