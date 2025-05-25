using InstallmentManager.Domain.Entities;

namespace InstallmentManager.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> Get(string username);
        Task Create(User user);
    }
}
