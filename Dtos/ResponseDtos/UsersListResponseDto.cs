using TestProject.Dtos.RequestDtos;

namespace TestProject.Dtos.ResponseDtos {
    public class UsersListResponseDto : GenericResponseDto {
        public List<UserDto>? Users { get; set; }
    }
}
