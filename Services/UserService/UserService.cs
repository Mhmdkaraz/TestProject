using System.Security.Claims;
using TestProject.Dtos.RequestDtos;
using TestProject.Dtos.ResponseDtos;
using TestProject.Extensions;
using TestProject.Helpers;

namespace TestProject.Services.UserService {
    public class UserService : IUserService {
        private readonly IFileService _fileService;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IFileService fileService, IPasswordService passwordService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor) {
            _fileService = fileService;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> CreateUser(UserCreateDto userDto) {
            var user = userDto.ToUser();

            _passwordService.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user.Token = _tokenService.CreateToken(user);

            await _fileService.AddUserToFile(user);

            return user.ToUserDto();
        }

        public async Task<UsersListResponseDto> GetUsers() {
            var users = await _fileService.ReadUsersFromFile();
            if (users == null) return new UsersListResponseDto() { Success = false, Message = "No users found" };
            List<UserDto> usersDto = users.Select(u => u.ToUserDto()).ToList();
            return new UsersListResponseDto() { Users = usersDto, Success = true, Message = $"{usersDto.Count} users was found" };
        }

        public async Task<UserResponseDto> UpdateUser(UserUpdateDto updateDto) {
            var users = await _fileService.ReadUsersFromFile();
            var user = users.FirstOrDefault(u => u.Id == updateDto.Id);

            if (user == null) return new UserResponseDto() { Success = false, Message = "User not found" };


            if (!string.IsNullOrWhiteSpace(updateDto.Email)) {
                user.Email = updateDto.Email;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.Name)) {
                user.Name = updateDto.Name;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.PhoneNumber)) {
                user.PhoneNumber = updateDto.PhoneNumber;
            }

            if (updateDto.DOB.HasValue) {
                user.DOB = updateDto.DOB.Value;
            }

            if (updateDto.Balance.HasValue) {
                user.Balance = updateDto.Balance.Value;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.Password)) {
                _passwordService.CreatePasswordHash(updateDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            await _fileService.SaveUsersToFile(users);

            return new UserResponseDto() { User = user.ToUserDto(), Success = true, Message = "Successful" };
        }

        public async Task<GenericResponseDto> DeleteUser(Guid id) {
            var users = await _fileService.ReadUsersFromFile();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null) return new GenericResponseDto() { Success = false, Message = "User not found." };

            users.Remove(user);
            await _fileService.SaveUsersToFile(users);
            return new GenericResponseDto() { Success = true, Message = "User removed successfully." };
        }

        public async Task<UserResponseDto> AddBalance(Guid id, decimal amount) {
            var users = await _fileService.ReadUsersFromFile();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null) return new UserResponseDto() { Success = false, Message = "User was not found." };
            if (amount <= 0) return new UserResponseDto() { Success = false, Message = "Amount must be greater than 0." };

            user.Balance += amount;
            await _fileService.SaveUsersToFile(users);

            return new UserResponseDto() { User = user.ToUserDto(), Success = true, Message = "Balance was updated" };
        }

        public async Task<UserLoginResponseDto> LoginUser(UserLoginDto loginDto) {
            var users = await _fileService.ReadUsersFromFile();
            var user = users.FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null || !_passwordService.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt)) {
                return new UserLoginResponseDto() { Success = false, Message = "Incorrect email or password." }; ;
            }

            string token = _tokenService.CreateToken(user);
            return new UserLoginResponseDto() { Token = token, Success = true, Message = "Token created successfully." };
        }

        public async Task<TransferFundsResponseDto> TransferFunds(TransferFundsDto transferDto) {

            var senderIdFromToken = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


            if (senderIdFromToken == null) {
                return new TransferFundsResponseDto() { Success = false, Message = "Unathorized Access" };
            }

            var users = await _fileService.ReadUsersFromFile();
            var senderUser = users.FirstOrDefault(u => u.Id == Guid.Parse(senderIdFromToken));
            var receiverUser = users.FirstOrDefault(u => u.Id == transferDto.ReceiverId);

            if (senderUser == null || receiverUser == null) {
                return new TransferFundsResponseDto() { Success = false, Message = "Invalid sender or receiver." };
            }

            if (senderUser.Balance < transferDto.Amount) {
                return new TransferFundsResponseDto() { Success = false, Message = "Insufficient balance." };

            }

            senderUser.Balance -= transferDto.Amount;
            receiverUser.Balance += transferDto.Amount;

            await _fileService.SaveUsersToFile(users);
            return new TransferFundsResponseDto() { Success = true, Message = "Transfer funds successful" };
        }
    }
}
