using AutoMapper;
using InstallmentManager.Application.Requests.User;
using InstallmentManager.Application.Responses.User;
using InstallmentManager.Application.Services.Interfaces;
using InstallmentManager.Domain.Entities;
using InstallmentManager.Domain.Exceptions;
using InstallmentManager.Domain.Interfaces;

namespace InstallmentManager.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task Create(CreateUserRequest createUserRequest)
        {
            User? existingUser = await _userRepository.Get(createUserRequest.Username);

            if (existingUser is not null)
                throw new UserAlreadyExistsException(createUserRequest.Username);

            User user = _mapper.Map<User>(createUserRequest);
            user.Password = _passwordHasher.Hash(createUserRequest.Password);

            await _userRepository.Create(user);
        }

        public async Task<UserLoginResponse> Login(string username, string password)
        {
            User? user = await _userRepository.Get(username);

            if (user is null)
                throw new UserNotFoundException(username);

            bool isCorrectPassword = _passwordHasher.Verify(password, user.Password);

            if (!isCorrectPassword)
                throw new InvalidPasswordException();

            return new UserLoginResponse()
            {
                AccessToken = _tokenService.Generate(user.Id, user.Name),
            };
        }
    }
}
