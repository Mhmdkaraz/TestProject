namespace TestProject.Dtos.RequestDtos {
    public class UserDto {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public decimal Balance { get; set; }
    }
}
