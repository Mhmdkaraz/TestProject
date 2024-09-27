using System.ComponentModel.DataAnnotations;

namespace TestProject.Dtos.RequestDtos {
    public class UserDeleteDto {
        [Required(ErrorMessage = "User Id is required")]
        public Guid Id { get; set; }
    }
}
