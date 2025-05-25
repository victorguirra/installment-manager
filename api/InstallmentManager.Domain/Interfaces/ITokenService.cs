namespace InstallmentManager.Domain.Interfaces
{
    public interface ITokenService
    {
        string Generate(int userId, string username);
    }
}
