using InstallmentManager.Domain.Entities;

namespace InstallmentManager.Application.Services.Interfaces
{
    public interface IInstallmentService
    {
        Task<List<Installment>> Get(int userId, int contractId);
        Task<Installment?> GetById(int userId, int id);
        List<Installment> Generate(int installmentAmounts, decimal totalContractAmount);
        Task Anticipate(int userId, int id);
    }
}
