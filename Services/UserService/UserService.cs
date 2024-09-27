using TestProject.Dtos.RequestDtos;
using TestProject.Dtos.ResponseDtos;

namespace TestProject.Services.UserService {
    public class UserService : IUserService {
        public Task<UserResponseDto> AddBalance(Guid id, decimal amount) {
            throw new NotImplementedException();
        }

        public Task<UserDto> CreateUser(UserCreateDto userDto) {
            throw new NotImplementedException();
        }

        public Task<GenericResponseDto> DeleteUser(Guid id) {
            throw new NotImplementedException();
        }

        public Task<UsersListResponseDto> GetUsers() {
            throw new NotImplementedException();
        }

        public Task<UserLoginResponseDto> LoginUser(UserLoginDto loginDto) {
            throw new NotImplementedException();
        }

        public Task<TransferFundsResponseDto> TransferFunds(TransferFundsDto transferDto) {
            throw new NotImplementedException();
        }

        public Task<UserResponseDto> UpdateUser(UserUpdateDto updateDto) {
            throw new NotImplementedException();
        }
    }
}
