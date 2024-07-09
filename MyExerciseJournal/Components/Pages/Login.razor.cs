using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MyExerciseJournal.Services;
using MyExerciseJournal.ViewModels;
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

        public LoginViewModel model { get; set; } = new();
        bool success;
        string errorMessage = "";

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
