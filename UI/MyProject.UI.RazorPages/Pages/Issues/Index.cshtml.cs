using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProject.UI.RazorPages.Models;
using MyProject.UI.RazorPages.Services;

namespace MyProject.UI.RazorPages.Pages.Issues
{
    public class IndexModel : PageModel
    {
        private readonly IIssueService _issueService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IIssueService issueService, ILogger<IndexModel> logger)
        {
            _issueService = issueService;
            _logger = logger;
        }

        public IEnumerable<IssueDto> Issues { get; set; } = new List<IssueDto>();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Issues = await _issueService.GetAllIssuesAsync();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving issues");
                TempData["ErrorMessage"] = "Unable to load issues. Please check if the API is running.";
                return Page();
            }
        }
    }
}