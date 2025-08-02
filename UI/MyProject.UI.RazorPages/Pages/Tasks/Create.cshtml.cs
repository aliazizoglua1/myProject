using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly IProjectService _projectService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ITaskService taskService, IProjectService projectService, ILogger<CreateModel> logger)
        {
            _taskService = taskService;
            _projectService = projectService;
            _logger = logger;
        }

        [BindProperty]
        public CreateTaskDto Task { get; set; } = new CreateTaskDto();

        public SelectList Projects { get; set; } = new SelectList(new List<SelectListItem>());
        public SelectList ParentTasks { get; set; } = new SelectList(new List<SelectListItem>());

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
                var createdTask = await _taskService.CreateTaskAsync(Task);
                TempData["SuccessMessage"] = "Task created successfully!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the task. Please try again.");
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
                ParentTasks = new SelectList(tasks, "TaskId", "TaskName");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dropdown data");
                Projects = new SelectList(new List<SelectListItem>());
                ParentTasks = new SelectList(new List<SelectListItem>());
            }
        }
    }
}