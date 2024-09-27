using TestProject.Models;

namespace TestProject.Helpers {
    public interface IFileService {
        Task AddUserToFile(User user);
        Task<List<User>> ReadUsersFromFile();
        Task SaveUsersToFile(List<User> users);
    }
}
