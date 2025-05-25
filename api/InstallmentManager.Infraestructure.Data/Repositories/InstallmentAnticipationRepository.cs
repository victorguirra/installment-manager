using InstallmentManager.Domain.Entities;
using InstallmentManager.Domain.Enums;
using InstallmentManager.Domain.Interfaces;
using InstallmentManager.Infraestructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InstallmentManager.Infraestructure.Data.Repositories
{
    public class InstallmentAnticipationRepository : IInstallmentAnticipationRepository
    {
        private readonly AppDbContext _context;

        public InstallmentAnticipationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<InstallmentAnticipation>> Get(int userId)
        {
            return await _context.InstallmentAnticipation
                .Include(c => c.Installment)
                .Include(c => c.Installment.Contract)
                .Where(x => x.Installment.Contract.UserId == userId)
                .ToListAsync();;
        }

        public async Task<InstallmentAnticipation?> Get(int userId, int id)
        {
            return await _context.InstallmentAnticipation
                .Include(c => c.Installment)
                .Include(c => c.Installment.Contract)
                .Where(x => x.Installment.Contract.UserId == userId && x.Id == id)
                .FirstOrDefaultAsync(); ;
        }

        public async Task Anticipate(InstallmentAnticipation installmentAnticipation)
        {
            await _context.InstallmentAnticipation.AddAsync(installmentAnticipation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatus(int userId, int installmentAntecipationId, AnticipationStatus status)
        {
            InstallmentAnticipation? installment = await _context
                .InstallmentAnticipation
                .Include(c => c.Installment)
                .Include(c => c.Installment.Contract)
                .Where(x => x.Installment.Contract.UserId == userId && x.Id == installmentAntecipationId)
                .FirstOrDefaultAsync();

            if(installment is not null)
            {
                _context.Entry(installment).Property(x => x.Status).CurrentValue = status;
                _context.Entry(installment).Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }
    }
}
