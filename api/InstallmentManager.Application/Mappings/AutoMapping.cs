using AutoMapper;
using InstallmentManager.Application.Requests.User;
using InstallmentManager.Domain.Entities;

namespace InstallmentManager.Application.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<CreateUserRequest, User>();
        }
    }
}
