using InstallmentManager.Application.Requests.User;
using InstallmentManager.Application.Responses.User;

namespace InstallmentManager.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task Create(CreateUserRequest request);
        Task<UserLoginResponse> Login(string username, string password);
    }
}
