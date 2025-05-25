using InstallmentManager.Domain.Common;
using InstallmentManager.Domain.Enums;

namespace InstallmentManager.Domain.Entities
{
    public class InstallmentAnticipation : BaseEntity
    {
        public int Id { get; set; }
        public int InstallmentId { get; set; }
        public Installment Installment { get; set; }
        public AnticipationStatus Status { get; set; }
    }
}
