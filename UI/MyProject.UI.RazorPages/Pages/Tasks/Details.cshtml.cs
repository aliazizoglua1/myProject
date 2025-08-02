using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Tasks
{
    public class DetailsModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(ITaskService taskService, ILogger<DetailsModel> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        public TaskDto? Task { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                Task = await _taskService.GetTaskAsync(id);
                if (Task == null)
                {
                    return NotFound();
                }

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving task details, ID: {TaskId}", id);
                return NotFound();
            }
        }
    }
}