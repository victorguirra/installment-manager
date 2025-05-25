using InstallmentManager.Application.Requests.Contract;
using InstallmentManager.Application.Services.Interfaces;
using InstallmentManager.Domain.Entities;
using InstallmentManager.Domain.Interfaces;

namespace InstallmentManager.Application.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IInstallmentService _installmentService;

        public ContractService(IContractRepository contractRepository, IInstallmentService installmentService)
        {
            _contractRepository = contractRepository;
            _installmentService = installmentService;
        }

        public async Task<List<Contract>> Get(int userId)
        {
            return await _contractRepository.Get(userId);
        }

        public async Task<Contract> Create(int userId, CreateContractRequest createContractRequest)
        {
            Contract contract = new Contract()
            {
                UserId = userId,
                Description = createContractRequest.Description,
                Installments = _installmentService.Generate(createContractRequest.InstallmentAmounts, createContractRequest.TotalAmount),
            };

            await _contractRepository.Create(contract);
            return contract;
        }

        public async Task Delete(int userId, int contractId)
        {
            await _contractRepository.Delete(userId, contractId);
        }
    }
}
