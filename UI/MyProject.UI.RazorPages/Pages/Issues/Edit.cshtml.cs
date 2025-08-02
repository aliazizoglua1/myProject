using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Issues
{
    public class EditModel : PageModel
    {
        private readonly IIssueService _issueService;
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;
        private readonly ILogger<EditModel> _logger;

        public EditModel(IIssueService issueService, IProjectService projectService, ITaskService taskService, ILogger<EditModel> logger)
        {
            _issueService = issueService;
            _projectService = projectService;
            _taskService = taskService;
            _logger = logger;
        }

        [BindProperty]
        public UpdateIssueDto Issue { get; set; } = new UpdateIssueDto();

        public Guid IssueId { get; set; }
        public SelectList Projects { get; set; } = new SelectList(new List<SelectListItem>());
        public SelectList Tasks { get; set; } = new SelectList(new List<SelectListItem>());

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var issue = await _issueService.GetIssueAsync(id);
                if (issue == null)
                {
                    return NotFound();
                }

                IssueId = id;
                
                // Map IssueDto to UpdateIssueDto
                Issue = new UpdateIssueDto
                {
                    TaskId = issue.TaskId,
                    IssueName = issue.IssueName,
                    IssueDescription = issue.IssueDescription,
                    IssueType = issue.IssueType,
                    RootCause = issue.RootCause,
                    ImpactOnScheduleDays = issue.ImpactOnScheduleDays,
                    ImpactOnBudgetUsd = issue.ImpactOnBudgetUsd,
                    ImpactOnScope = issue.ImpactOnScope,
                    ImpactOnQuality = issue.ImpactOnQuality,
                    ResolutionSteps = issue.ResolutionSteps,
                    Severity = issue.Severity,
                    Priority = issue.Priority,
                    AssignedToUserId = issue.AssignedToUserId,
                    Status = issue.Status,
                    OpenedDate = issue.OpenedDate,
                    ClosedDate = issue.ClosedDate
                };

                await LoadDropdownData();
                
                // Set the project in the dropdown for display purposes (it's disabled)
                var projects = await _projectService.GetAllProjectsAsync();
                var projectsList = projects.ToList();
                Projects = new SelectList(projectsList, "ProjectId", "ProjectName", issue.ProjectId);

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading issue {IssueId}", id);
                TempData["ErrorMessage"] = "Unable to load issue. Please check if the API is running.";
                return RedirectToPage("./Index");
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            IssueId = id;
            
            if (!ModelState.IsValid)
            {
                await LoadDropdownData();
                return Page();
            }

            try
            {
                var updatedIssue = await _issueService.UpdateIssueAsync(id, Issue);
                TempData["SuccessMessage"] = "Issue updated successfully!";
                return RedirectToPage("./Details", new { id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating issue {IssueId}", id);
                ModelState.AddModelError(string.Empty, "An error occurred while updating the issue. Please try again.");
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