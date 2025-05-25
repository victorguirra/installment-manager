using InstallmentManager.Domain.Entities;
using InstallmentManager.Domain.Interfaces;
using InstallmentManager.Infraestructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InstallmentManager.Infraestructure.Data.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly AppDbContext _context;

        public ContractRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contract>> Get(int userId)
        {
            return await _context.Contract
                .Where(x => x.UserId == userId)
                .Include(e => e.Installments)
                .ToListAsync();
        }

        public async Task Create(Contract contract)
        {
            await _context.Contract.AddAsync(contract);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int userId, int contractId)
        {
            Contract? contract = _context.Contract.FirstOrDefault(x => x.UserId == userId && x.Id == contractId);

            if(contract is not null)
            {
                _context.Contract.Remove(contract);
                await _context.SaveChangesAsync();
            }
        }
    }
}
