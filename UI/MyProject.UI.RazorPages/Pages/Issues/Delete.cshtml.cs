using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Issues
{
    public class DeleteModel : PageModel
    {
        private readonly IIssueService _issueService;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(IIssueService issueService, ILogger<DeleteModel> logger)
        {
            _issueService = issueService;
            _logger = logger;
        }

        [BindProperty]
        public IssueDto? Issue { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                Issue = await _issueService.GetIssueAsync(id);
                if (Issue == null)
                {
                    return NotFound();
                }
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving issue {IssueId}", id);
                TempData["ErrorMessage"] = "Unable to load issue details. Please check if the API is running.";
                return RedirectToPage("./Index");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Issue?.IssueId == null)
            {
                return NotFound();
            }

            try
            {
                var deleted = await _issueService.DeleteIssueAsync(Issue.IssueId);
                if (deleted)
                {
                    TempData["SuccessMessage"] = "Issue deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete the issue.";
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting issue {IssueId}", Issue.IssueId);
                TempData["ErrorMessage"] = "An error occurred while deleting the issue. Please try again.";
                return RedirectToPage("./Index");
            }
        }
    }
}