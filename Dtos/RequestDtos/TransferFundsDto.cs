using System.ComponentModel.DataAnnotations;

namespace TestProject.Dtos.RequestDtos {
    public class TransferFundsDto {
        [Required(ErrorMessage = "Receiver Id is required")]
        public Guid ReceiverId { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a non-negative value.")]
        public decimal Amount { get; set; }
    }
}
