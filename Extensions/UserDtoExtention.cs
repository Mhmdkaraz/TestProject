using TestProject.Dtos.RequestDtos;
using TestProject.Models;

namespace TestProject.Extensions
{
    public static class UserDtoExtention {
        public static User ToUser(this UserCreateDto userDto) {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));

            return new User {
                Email = userDto.Email,
                Name = userDto.Name,
                PhoneNumber = userDto.PhoneNumber,
                DOB = userDto.DOB,
                Balance = userDto.Balance
            };
        }
    }
}
