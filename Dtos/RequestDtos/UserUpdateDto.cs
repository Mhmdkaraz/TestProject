using System.ComponentModel.DataAnnotations;

namespace TestProject.Dtos.RequestDtos {
    public class UserUpdateDto {
        [Required(ErrorMessage = "User Id is required")]
        public Guid Id { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }

        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date of birth format.")]
        public DateTime? DOB { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a non-negative value.")]
        public decimal? Balance { get; set; }

        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string? Password { get; set; }
    }
}
