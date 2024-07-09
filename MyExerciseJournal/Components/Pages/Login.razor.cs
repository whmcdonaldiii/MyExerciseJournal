using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MyExerciseJournal.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MyExerciseJournal.Components.Pages
{
    public partial class Login
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        ExerciseAuthenticationService AuthenticationService { get; set; }

        public LoginForm model { get; set; } = new();

        bool success;

        string errorMessage = "";

        public class LoginForm
        {
            [Required]
            [StringLength(16, ErrorMessage = "Name length can't be more than 16.")]
            public string Username { get; set; } = "";
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private async Task OnValidSubmit(EditContext context)
        {
            success = AuthenticationService.AuthenticateUser(model.Username);

            if (success)
            {
                LoginCredentials credentials = new() { IsLoggedIn = true, UserName = model.Username };
                string json = JsonSerializer.Serialize<LoginCredentials>(credentials);
                await LocalStorage.SetItemAsync<string>("isLoggedIn", json);
                NavigationManager.NavigateTo("/", true);
            }
            else
            {
                errorMessage = "* Unknown user. Please register";
                NavigationManager.NavigateTo("/login");
            }

        }
    }
}
