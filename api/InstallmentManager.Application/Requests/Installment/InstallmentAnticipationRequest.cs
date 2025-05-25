namespace InstallmentManager.Application.Requests.Installment
{
    public class InstallmentAnticipationRequest
    {
        public List<int> InstallmentIds { get; set; } = new List<int>();
    }
}
