using InstallmentManager.Application.Services.Interfaces;
using InstallmentManager.Domain.Entities;
using InstallmentManager.Domain.Enums;
using InstallmentManager.Domain.Exceptions;
using InstallmentManager.Domain.Interfaces;

namespace InstallmentManager.Application.Services
{
    public class InstallmentAnticipationService : IInstallmentAnticipationService
    {
        private readonly IInstallmentAnticipationRepository _installmentAnticipationRepository;
        private readonly IInstallmentService _installmentService;

        public InstallmentAnticipationService(
            IInstallmentAnticipationRepository installmentAnticipationRepository,
            IInstallmentService installmentService
        )
        {
            _installmentAnticipationRepository = installmentAnticipationRepository;
            _installmentService = installmentService;
        }

        public Task<List<InstallmentAnticipation>> Get(int userId)
        {
            return _installmentAnticipationRepository.Get(userId);
        }

        public Task<InstallmentAnticipation?> Get(int userId, int id)
        {
            return _installmentAnticipationRepository.Get(userId, id);
        }

        public async Task<List<InstallmentAnticipation>> Anticipate(int userId, List<int> installmentIds)
        {
            List<InstallmentAnticipation> anticipations = await _installmentAnticipationRepository.Get(userId);
            bool userHasPendingAnticipation = anticipations.Any(x => x.Status == AnticipationStatus.Pending);

            if (userHasPendingAnticipation)
                throw new UserHasPendingAnticipationException();

            List<InstallmentAnticipation> installmentAnticipations = new List<InstallmentAnticipation>();

            foreach (int installmentId in installmentIds)
            {
                Installment? installment = await _installmentService.GetById(userId, installmentId);

                if (installment is null)
                    throw new InstallmentNotFoundException();

                if ((installment.DueDate - DateTime.UtcNow).TotalDays <= 30)
                    throw new InstallmentDueDateTooCloseException();

                InstallmentAnticipation installmentAnticipation = new InstallmentAnticipation()
                {
                    InstallmentId = installmentId,
                    Status = AnticipationStatus.Pending
                };

                installmentAnticipations.Add(installmentAnticipation);
            }

            foreach(InstallmentAnticipation installmentAnticipation in installmentAnticipations)
                await _installmentAnticipationRepository.Anticipate(installmentAnticipation);

            return installmentAnticipations;
        }

        public async Task Approve(int userId, int installmentAnticipationId)
        {
            InstallmentAnticipation? currentAncitipation = await _installmentAnticipationRepository.Get(userId, installmentAnticipationId);
            
            if(currentAncitipation is not null && currentAncitipation.Status == AnticipationStatus.Pending)
            {
                await _installmentAnticipationRepository.UpdateStatus(userId, installmentAnticipationId, AnticipationStatus.Approved);
                await _installmentService.Anticipate(userId, currentAncitipation.InstallmentId);
            }
        }

        public async Task Reject(int userId, int installmentAnticipationId)
        {
            InstallmentAnticipation? currentAncitipation = await _installmentAnticipationRepository.Get(userId, installmentAnticipationId);

            if (currentAncitipation is not null && currentAncitipation.Status == AnticipationStatus.Pending)
                await _installmentAnticipationRepository.UpdateStatus(userId, installmentAnticipationId, AnticipationStatus.Rejected);
        }
    }
}
