using MyProject.UI.RazorPages.Models;

namespace MyProject.UI.RazorPages.Services
{
    public interface IIssueService
    {
        Task<IEnumerable<IssueDto>> GetAllIssuesAsync();
        Task<IssueDto?> GetIssueAsync(Guid id);
        Task<IssueDto> CreateIssueAsync(CreateIssueDto issue);
        Task<IssueDto> UpdateIssueAsync(Guid id, UpdateIssueDto issue);
        Task<bool> DeleteIssueAsync(Guid id);
        Task<IEnumerable<IssueDto>> GetIssuesByProjectAsync(Guid projectId);
        Task<IEnumerable<IssueDto>> GetIssuesByTaskAsync(Guid taskId);
        Task<IEnumerable<IssueDto>> GetIssuesByStatusAsync(string status);
        Task<IEnumerable<IssueDto>> GetIssuesBySeverityAsync(string severity);
        Task<IEnumerable<IssueDto>> GetIssuesByPriorityAsync(string priority);
        Task<IEnumerable<IssueDto>> GetIssuesByAssignedUserAsync(Guid userId);
        Task<IEnumerable<IssueDto>> GetOpenIssuesAsync();
        Task<IEnumerable<IssueDto>> GetOverdueIssuesAsync();
    }
}