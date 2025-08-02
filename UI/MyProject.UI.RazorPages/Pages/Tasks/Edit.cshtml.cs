using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly IProjectService _projectService;
        private readonly ILogger<EditModel> _logger;

        public EditModel(ITaskService taskService, IProjectService projectService, ILogger<EditModel> logger)
        {
            _taskService = taskService;
            _projectService = projectService;
            _logger = logger;
        }

        [BindProperty]
        public UpdateTaskDto Task { get; set; } = new UpdateTaskDto();

        public Guid TaskId { get; set; }
        public SelectList Projects { get; set; } = new SelectList(new List<SelectListItem>());
        public SelectList ParentTasks { get; set; } = new SelectList(new List<SelectListItem>());

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var task = await _taskService.GetTaskAsync(id);
                if (task == null)
                {
                    return NotFound();
                }

                TaskId = id;
                
                // Map TaskDto to UpdateTaskDto
                Task = new UpdateTaskDto
                {
                    TaskName = task.TaskName,
                    TaskType = task.TaskType,
                    AssignedToUserId = task.AssignedToUserId,
                    ParentTaskId = task.ParentTaskId,
                    PlannedEffortHours = task.PlannedEffortHours,
                    ActualEffortHours = task.ActualEffortHours,
                    PlannedStartDate = task.PlannedStartDate,
                    PlannedEndDate = task.PlannedEndDate,
                    ActualStartDate = task.ActualStartDate,
                    ActualEndDate = task.ActualEndDate,
                    TaskStatus = task.TaskStatus,
                    Priority = task.Priority,
                    IsMilestone = task.IsMilestone,
                    Description = task.Description,
                    Comments = task.Comments
                };

                await LoadDropdownData(id);
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving task for edit, ID: {TaskId}", id);
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                TaskId = id;
                await LoadDropdownData(id);
                return Page();
            }

            try
            {
                var updatedTask = await _taskService.UpdateTaskAsync(id, Task);
                TempData["SuccessMessage"] = "Task updated successfully!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task, ID: {TaskId}", id);
                ModelState.AddModelError(string.Empty, "An error occurred while updating the task. Please try again.");
                TaskId = id;
                await LoadDropdownData(id);
                return Page();
            }
        }

        private async Task LoadDropdownData(Guid excludeTaskId)
        {
            try
            {
                var projects = await _projectService.GetAllProjectsAsync();
                Projects = new SelectList(projects, "ProjectId", "ProjectName");

                var tasks = await _taskService.GetAllTasksAsync();
                // Filter out the current task and its subtasks to prevent circular references
                var availableParentTasks = tasks.Where(t => t.TaskId != excludeTaskId).ToList();
                ParentTasks = new SelectList(availableParentTasks, "TaskId", "TaskName");
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