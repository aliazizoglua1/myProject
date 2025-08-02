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
                _logger.LogError(ex, "Error retrieving issue for deletion, ID: {IssueId}", id);
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try
            {
                var deleted = await _issueService.DeleteIssueAsync(id);
                if (deleted)
                {
                    TempData["SuccessMessage"] = "Issue deleted successfully!";
                    return RedirectToPage("./Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Issue not found or could not be deleted.";
                    return RedirectToPage("./Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting issue, ID: {IssueId}", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the issue.";
                return RedirectToPage("./Index");
            }
        }
    }
}