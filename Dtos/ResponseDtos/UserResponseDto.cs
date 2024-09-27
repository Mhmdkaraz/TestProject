using TestProject.Dtos.RequestDtos;

namespace TestProject.Dtos.ResponseDtos {
    public class UserResponseDto : GenericResponseDto {
        public UserDto User { get; set; }
    }
}
