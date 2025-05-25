using InstallmentManager.Domain.Common;

namespace InstallmentManager.Domain.Entities
{
    public class Contract : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public List<Installment> Installments { get; set; }
    }
}
