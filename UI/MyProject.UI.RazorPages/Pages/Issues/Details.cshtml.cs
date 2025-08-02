using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Issues
{
    public class DetailsModel : PageModel
    {
        private readonly IIssueService _issueService;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(IIssueService issueService, ILogger<DetailsModel> logger)
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
                _logger.LogError(ex, "Error retrieving issue details, ID: {IssueId}", id);
                return NotFound();
            }
        }
    }
}