using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public interface IChangeRequestRepository
    {
        Task<IEnumerable<ChangeRequest>> GetAllAsync();
        Task<ChangeRequest?> GetByIdAsync(Guid id);
        Task<IEnumerable<ChangeRequest>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<ChangeRequest>> GetByStatusAsync(string status);
        Task<IEnumerable<ChangeRequest>> GetByRequestedByUserIdAsync(Guid userId);
        Task<IEnumerable<ChangeRequest>> GetByApprovedByUserIdAsync(Guid userId);
        Task<IEnumerable<ChangeRequest>> GetPendingRequestsAsync();
        Task<IEnumerable<ChangeRequest>> GetApprovedRequestsAsync();
        Task<IEnumerable<ChangeRequest>> GetRejectedRequestsAsync();
        Task<IEnumerable<ChangeRequest>> GetByRequestDateRangeAsync(DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<ChangeRequest>> GetByVersionAffectedAsync(string version);
        Task<ChangeRequest> AddAsync(ChangeRequest changeRequest);
        Task<ChangeRequest> UpdateAsync(ChangeRequest changeRequest);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<ChangeRequest>> SearchByTitleAsync(string searchTerm);
    }
} 