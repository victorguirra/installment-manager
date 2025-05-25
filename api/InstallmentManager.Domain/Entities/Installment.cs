using InstallmentManager.Domain.Common;
using InstallmentManager.Domain.Enums;
using System.Text.Json.Serialization;

namespace InstallmentManager.Domain.Entities
{
    public class Installment : BaseEntity
    {
        public string Code { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public InstallmentStatus Status { get; set; }
        public bool Anticipated { get; set; }
        public int ContractId { get; set; }

        [JsonIgnore]
        public Contract Contract { get; set; }
    }
}
