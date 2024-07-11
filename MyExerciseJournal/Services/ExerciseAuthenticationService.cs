using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MyExerciseJournal.Models;
using MyExerciseJournal.Persistence;
using System.Text.Json;

namespace MyExerciseJournal.Services
{
    public class ExerciseAuthenticationService
    {
        private readonly ExerciseRepository _repo;
        private readonly ProtectedSessionStorage _protectedSessionStorage;
        public ExerciseAuthenticationService(ProtectedSessionStorage protectedSessionStorage, ExerciseRepository repo)
        {
            _protectedSessionStorage = protectedSessionStorage;
            _repo = repo;
        }

        public User CurrentUser { get; private set; }

        public async Task<User?> GetCurrentUserAsync()
        {
            var json = await _protectedSessionStorage.GetAsync<string>("isLoggedIn");
            if (!string.IsNullOrWhiteSpace(json.Value))
            {
                LoginCredentials credentials = JsonSerializer.Deserialize<LoginCredentials>(json.Value);
                string userName = credentials.UserName;

                List<User> users = _repo.GetAllUsers().ToList();
                CurrentUser = users.First(u => u.Name == userName);
                return CurrentUser;
            } 
            return null;
        }

        public bool AuthenticateUser(string userName)
        {
            List<User> users = _repo.GetAllUsers().ToList();
            if (users.Any(x => x.Name == userName))
            {
                CurrentUser = users.First(u=>u.Name == userName);
                return true;
            }
            return false;
        }
    }
}
