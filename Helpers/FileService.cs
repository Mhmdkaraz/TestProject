using System.Text.Json;
using TestProject.Models;

namespace TestProject.Helpers {
    public class FileService : IFileService {
        private readonly IConfiguration _config;
        public FileService(IConfiguration config) {
            _config = config;
        }
        public async Task AddUserToFile(User user) {
            var users = await ReadUsersFromFile();
            users.Add(user);
            await SaveUsersToFile(users);
        }

        public async Task<List<User>> ReadUsersFromFile() {
            if (!File.Exists(_config["FilePath"])) return new List<User>();

            var jsonData = await File.ReadAllTextAsync(_config["FilePath"]);
            return JsonSerializer.Deserialize<List<User>>(jsonData) ?? new List<User>();
        }

        public async Task SaveUsersToFile(List<User> users) {
            var jsonData = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_config["FilePath"], jsonData);
        }
    }
}
