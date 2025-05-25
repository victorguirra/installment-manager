using InstallmentManager.Domain.Entities;
using InstallmentManager.Domain.Interfaces;
using InstallmentManager.Infraestructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InstallmentManager.Infraestructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> Get(string username)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task Create(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
