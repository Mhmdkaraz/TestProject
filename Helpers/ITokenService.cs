using TestProject.Models;

namespace TestProject.Helpers {
    public interface ITokenService {
        string CreateToken(User user);
        User ValidateToken(string token);
    }
}
