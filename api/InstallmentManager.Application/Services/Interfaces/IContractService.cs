using InstallmentManager.Application.Requests.Contract;
using InstallmentManager.Domain.Entities;

namespace InstallmentManager.Application.Services.Interfaces
{
    public interface IContractService
    {
        Task<List<Contract>> Get(int userId);
        Task<Contract> Create(int userId, CreateContractRequest createContractRequest);
        Task Delete(int userId, int contractId);
    }
}
