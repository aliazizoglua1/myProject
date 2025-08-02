using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Tasks
{
    public class DeleteModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ITaskService taskService, ILogger<DeleteModel> logger)
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
                _logger.LogError(ex, "Error retrieving task for deletion, ID: {TaskId}", id);
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try
            {
                var deleted = await _taskService.DeleteTaskAsync(id);
                if (deleted)
                {
                    TempData["SuccessMessage"] = "Task deleted successfully!";
                    return RedirectToPage("./Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Task not found or could not be deleted.";
                    return RedirectToPage("./Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task, ID: {TaskId}", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the task.";
                return RedirectToPage("./Index");
            }
        }
    }
}