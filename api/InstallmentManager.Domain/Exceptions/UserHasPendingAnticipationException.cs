namespace InstallmentManager.Domain.Exceptions
{
    public class UserHasPendingAnticipationException : Exception
    {
        public UserHasPendingAnticipationException()
            : base("The user already has a pending anticipation request.")
        {
        }
    }
}
