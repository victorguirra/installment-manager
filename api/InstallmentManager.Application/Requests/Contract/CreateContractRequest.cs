namespace InstallmentManager.Application.Requests.Contract
{
    public class CreateContractRequest
    {
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public int InstallmentAmounts { get; set; }
    }
}
