using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TestProject.Dtos.ResponseDtos;

namespace TestProject.Attributes {
    public class AdminAuthorizeAttribute : Attribute, IAuthorizationFilter {
        public void OnAuthorization(AuthorizationFilterContext context) {
            var configuration = (IConfiguration)context.HttpContext.RequestServices.GetService(typeof(IConfiguration));
            string adminToken = configuration.GetValue<string>("AdminToken");

            // Retrieve the token from the request headers
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Check if the token matches the admin token
            if (string.IsNullOrEmpty(token) || token != adminToken) {
                // If the token is invalid or not found, return unauthorized
                context.Result = new JsonResult(new GenericResponseDto() { Success = false, Message = "Unauthorized access" }) {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
