using InstallmentManager.Domain.Entities;
using InstallmentManager.Domain.Enums;

namespace InstallmentManager.Domain.Interfaces
{
    public interface IInstallmentAnticipationRepository
    {
        Task<List<InstallmentAnticipation>> Get(int userId);
        Task<InstallmentAnticipation?> Get(int userId, int id);
        Task Anticipate(InstallmentAnticipation installmentAnticipation);
        Task UpdateStatus(int userId, int installmentAntecipationId, AnticipationStatus status);
    }
}
