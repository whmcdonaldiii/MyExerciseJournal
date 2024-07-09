using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MyExerciseJournal.Models;
using MyExerciseJournal.Persistence;
using System.Text.Json;

namespace MyExerciseJournal.Services
{
    public class ExerciseAuthenticationService
    {
        private readonly ExerciseRepository _repo;
        private readonly ILocalStorageService _localStorage;
        public ExerciseAuthenticationService(ILocalStorageService localStorage, ExerciseRepository repo)
        {
            _localStorage = localStorage;
            _repo = repo;
        }

        public User CurrentUser { get; private set; }

        public async Task<User?> GetCurrentUserAsync()
        {
            string json = await _localStorage.GetItemAsync<string>("isLoggedIn");
            if (!string.IsNullOrWhiteSpace(json))
            {
                LoginCredentials credentials = JsonSerializer.Deserialize<LoginCredentials>(json);
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
