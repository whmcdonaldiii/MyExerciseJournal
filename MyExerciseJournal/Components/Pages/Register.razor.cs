using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MyExerciseJournal.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MyExerciseJournal.Components.Pages
{
    public partial class Register
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        RegisterViewModel ViewModel { get; set; }

        RegisterAccountForm model = new RegisterAccountForm();
        bool success;


        public class RegisterAccountForm
        {
            [Required]
            [StringLength(16, ErrorMessage = "Name length can't be more than 16.")]
            public string Username { get; set; } = "";
        }

        private void OnValidSubmit(EditContext context)
        {
            success = true;
            StateHasChanged();
            ViewModel.RegisterNewUser(model.Username);
            NavigationManager.NavigateTo("/login", true);
        }
    }
}
