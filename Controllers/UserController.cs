using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProject.Dtos.RequestDtos;
using TestProject.Dtos.ResponseDtos;
using TestProject.Services.UserService;

namespace TestProject.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request) {
            try {
                if (!ModelState.IsValid) {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();

                    return BadRequest(new UserLoginResponseDto {
                        Success = false,
                        Message = "Validation errors occurred",
                        Errors = errors
                    });
                }
                return Ok(await _userService.LoginUser(request));
            } catch (Exception ex) {
                return BadRequest(new UserLoginResponseDto() { Success = false, Message = "Error while logging in" });
            }
        }

        [HttpPost("transfer")]
        [Authorize]
        public async Task<IActionResult> TransferFunds(TransferFundsDto request) {
            try {
                if (!ModelState.IsValid) {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();

                    return BadRequest(new TransferFundsResponseDto {
                        Success = false,
                        Message = "Validation errors occurred",
                        Errors = errors
                    });
                }
                return Ok(await _userService.TransferFunds(request));
            } catch (Exception ex) {
                //Error log: ex.Message;
                return BadRequest(new TransferFundsResponseDto { Success = false, Message = "Error while transfer funds" });
            }
        }
    }
}
