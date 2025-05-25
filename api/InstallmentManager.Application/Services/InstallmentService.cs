using InstallmentManager.Application.Services.Interfaces;
using InstallmentManager.Domain.Entities;
using InstallmentManager.Domain.Enums;
using InstallmentManager.Domain.Interfaces;

namespace InstallmentManager.Application.Services
{
    public class InstallmentService : IInstallmentService
    {
        private readonly IInstallmentRepository _installmentRepository;

        public InstallmentService(IInstallmentRepository installmentRepository)
        {
            _installmentRepository = installmentRepository;
        }

        public async Task<List<Installment>> Get(int userId, int contractId)
        {
            return await _installmentRepository.Get(userId, contractId);
        }

        public async Task<Installment?> GetById(int userId, int id)
        {
            return await _installmentRepository.GetById(userId, id);
        }

        public List<Installment> Generate(int installmentAmounts, decimal totalContractAmount)
        {
            List<Installment> installments = new List<Installment>();
            decimal installmentValue = Math.Round(totalContractAmount / installmentAmounts, 2);
            
            for (int i = 0; i < installmentAmounts; i++)
            {
                installments.Add(new Installment
                {
                    Code = $"#P{i + 1}",
                    DueDate = DefineInstallmentDueDate(i),
                    Amount = installmentValue,
                    Status = InstallmentStatus.Open,
                    Anticipated = false,
                });
            }

            return installments;
        }

        public async Task Anticipate(int userId, int id)
        {
            DateTime newDueDate = DefineInstallmentDueDate(0);
            await _installmentRepository.Anticipate(userId, id, newDueDate);
        }

        private DateTime DefineInstallmentDueDate(int installmentNumber)
        {
            DateTime dueDate = DateTime.UtcNow.Date.AddDays(15);

            if (installmentNumber > 0)
                dueDate = dueDate.AddDays(installmentNumber * 30);

            return dueDate;
        }
    }
}
