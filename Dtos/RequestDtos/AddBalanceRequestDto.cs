using System.ComponentModel.DataAnnotations;

namespace TestProject.Dtos.RequestDtos {
    public class AddBalanceRequestDto {
        [Required(ErrorMessage = "User Id is required")]
        public Guid Id { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a non-negative value.")]
        public decimal Amount { get; set; }
    }
}
