using TestProject.Dtos.RequestDtos;
using TestProject.Models;

namespace TestProject.Extensions
{
    public static class UserExtension {
        public static UserDto ToUserDto(this User user) {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return new UserDto {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                DOB = user.DOB,
                Balance = user.Balance
            };
        }
    }
}
