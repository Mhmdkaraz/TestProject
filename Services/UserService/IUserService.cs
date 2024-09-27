using TestProject.Dtos.RequestDtos;
using TestProject.Dtos.ResponseDtos;

namespace TestProject.Services.UserService {
    public interface IUserService {
        Task<UserDto> CreateUser(UserCreateDto userDto);
        Task<UsersListResponseDto> GetUsers();
        Task<UserResponseDto> UpdateUser(UserUpdateDto updateDto);
        Task<GenericResponseDto> DeleteUser(Guid id);
        Task<UserResponseDto> AddBalance(Guid id, decimal amount);
        Task<UserLoginResponseDto> LoginUser(UserLoginDto loginDto);
        Task<TransferFundsResponseDto> TransferFunds(TransferFundsDto transferDto);
    }
}
