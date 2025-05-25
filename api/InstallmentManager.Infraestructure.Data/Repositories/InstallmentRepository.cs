using InstallmentManager.Domain.Entities;
using InstallmentManager.Domain.Interfaces;
using InstallmentManager.Infraestructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InstallmentManager.Infraestructure.Data.Repositories
{
    public class InstallmentRepository : IInstallmentRepository
    {
        private readonly AppDbContext _context;

        public InstallmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Installment>> Get(int userId, int contractId)
        {
            return await _context.Installment
                .Include(x => x.Contract)
                .Where(x => x.Contract.UserId == userId && x.ContractId == contractId)
                .ToListAsync();
        }

        public async Task<Installment?> GetById(int userId, int id)
        {
            return await _context.Installment
                .Include(x => x.Contract)
                .Where(x => x.Contract.UserId == userId && x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Anticipate(int userId, int id, DateTime newDueDate)
        {
            Installment? installment = await _context
                .Installment
                .Include(c => c.Contract)
                .Where(x => x.Contract.UserId == userId && x.Id == id)
                .FirstOrDefaultAsync();

            if (installment is not null)
            {
                _context.Entry(installment).Property(x => x.Anticipated).CurrentValue = true;
                _context.Entry(installment).Property(x => x.DueDate).CurrentValue = newDueDate;
                _context.Entry(installment).Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }
    }
}
