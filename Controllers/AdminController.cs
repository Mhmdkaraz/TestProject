using Microsoft.AspNetCore.Mvc;
using TestProject.Attributes;
using TestProject.Dtos.RequestDtos;
using TestProject.Dtos.ResponseDtos;
using TestProject.Services.UserService;

namespace TestProject.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    [AdminAuthorize]
    public class AdminController : ControllerBase {
        private readonly IUserService _userService;

        public AdminController(IUserService userService) {
            _userService = userService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UserCreateDto request) {
            try {
                if (!ModelState.IsValid) {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                             .Select(e => e.ErrorMessage)
                             .ToList();

                    return BadRequest(new UserResponseDto {
                        Success = false,
                        Message = "Validation errors occurred",
                        Errors = errors
                    });
                }
                return Ok(await _userService.CreateUser(request));
            } catch (Exception ex) {
                return BadRequest(new UserResponseDto() { Success = false, Message = "Error while creating user" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers() {
            try {
                return Ok(await _userService.GetUsers());
            } catch (Exception ex) {
                return BadRequest(new UsersListResponseDto() { Success = false, Message = "Error while getting users" });
            }
        }

        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto request) {
            try {
                if (!ModelState.IsValid) {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();

                    return BadRequest(new UserResponseDto {
                        Success = false,
                        Message = "Validation errors occurred",
                        Errors = errors
                    });
                }
                return Ok(await _userService.UpdateUser(request));
            } catch (Exception ex) {
                return BadRequest(new UserResponseDto() { Success = false, Message = "Error while updating user" });
            }
        }

        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUser(UserDeleteDto userDeleteDto) {
            try {
                return Ok(await _userService.DeleteUser(userDeleteDto.Id));
            } catch (Exception ex) {
                return BadRequest(new GenericResponseDto() { Success = false, Message = "Error while deleting a user." });
            }
        }

        [HttpPost("add-balance")]
        public async Task<IActionResult> AddBalance(AddBalanceRequestDto addBalanceRequestDto) {
            try {
                return Ok(await _userService.AddBalance(addBalanceRequestDto.Id, addBalanceRequestDto.Amount));
            } catch (Exception ex) {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
