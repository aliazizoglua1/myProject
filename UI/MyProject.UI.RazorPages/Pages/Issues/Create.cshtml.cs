using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Issues
{
    public class CreateModel : PageModel
    {
        private readonly IIssueService _issueService;
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IIssueService issueService, IProjectService projectService, ITaskService taskService, ILogger<CreateModel> logger)
        {
            _issueService = issueService;
            _projectService = projectService;
            _taskService = taskService;
            _logger = logger;
        }

        [BindProperty]
        public CreateIssueDto Issue { get; set; } = new CreateIssueDto();

        public SelectList Projects { get; set; } = new SelectList(new List<SelectListItem>());
        public SelectList Tasks { get; set; } = new SelectList(new List<SelectListItem>());

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadDropdownData();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownData();
                return Page();
            }

            try
            {
                var createdIssue = await _issueService.CreateIssueAsync(Issue);
                TempData["SuccessMessage"] = "Issue created successfully!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating issue");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the issue. Please try again.");
                await LoadDropdownData();
                return Page();
            }
        }

        private async Task LoadDropdownData()
        {
            try
            {
                var projects = await _projectService.GetAllProjectsAsync();
                Projects = new SelectList(projects, "ProjectId", "ProjectName");

                var tasks = await _taskService.GetAllTasksAsync();
                Tasks = new SelectList(tasks, "TaskId", "TaskName");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dropdown data");
                Projects = new SelectList(new List<SelectListItem>());
                Tasks = new SelectList(new List<SelectListItem>());
            }
        }
    }
}