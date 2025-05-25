namespace InstallmentManager.Domain.Exceptions
{
    public class InstallmentDueDateTooCloseException : Exception
    {
        public InstallmentDueDateTooCloseException()
            : base("Only installments with a due date more than 30 days from today can be anticipated.")
        { }
    }
}
