using InstallmentManager.Domain.Entities;

namespace InstallmentManager.Domain.Interfaces
{
    public interface IInstallmentRepository
    {
        Task<List<Installment>> Get(int userId, int contractId);
        Task<Installment?> GetById(int userId, int id);
        Task Anticipate(int userId, int id, DateTime newDueDate);
    }
}
