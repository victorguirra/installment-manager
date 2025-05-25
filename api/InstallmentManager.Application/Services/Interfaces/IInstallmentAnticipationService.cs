using InstallmentManager.Domain.Entities;

namespace InstallmentManager.Application.Services.Interfaces
{
    public interface IInstallmentAnticipationService
    {
        Task<List<InstallmentAnticipation>> Get(int userId);
        Task<InstallmentAnticipation?> Get(int userId, int id);
        Task<List<InstallmentAnticipation>> Anticipate(int userId, List<int> installmentIds);
        Task Approve(int userId, int installmentAnticipationId);
        Task Reject(int userId, int installmentAnticipationId);
    }
}
