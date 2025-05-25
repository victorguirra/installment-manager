using InstallmentManager.Domain.Entities;

namespace InstallmentManager.Domain.Interfaces
{
    public interface IContractRepository
    {
        Task<List<Contract>> Get(int userId);
        Task Create(Contract contract);
        Task Delete(int userId, int contractId);
    }
}
