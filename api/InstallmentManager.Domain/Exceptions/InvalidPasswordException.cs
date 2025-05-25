namespace InstallmentManager.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException()
            : base("The provided password is incorrect.")
        {
        }
    }
}
