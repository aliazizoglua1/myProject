using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ITaskService taskService, ILogger<IndexModel> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        public IEnumerable<TaskDto> Tasks { get; set; } = new List<TaskDto>();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Tasks = await _taskService.GetAllTasksAsync();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tasks");
                TempData["ErrorMessage"] = "Unable to load tasks. Please check if the API is running.";
                return Page();
            }
        }
    }
}