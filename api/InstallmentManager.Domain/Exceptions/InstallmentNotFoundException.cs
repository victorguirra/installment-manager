namespace InstallmentManager.Domain.Exceptions
{
    public class InstallmentNotFoundException : Exception
    {
        public InstallmentNotFoundException()
            : base("The specified installment was not found.")
        { }
    }
}
