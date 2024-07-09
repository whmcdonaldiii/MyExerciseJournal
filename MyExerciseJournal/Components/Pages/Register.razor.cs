using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MyExerciseJournal.ViewModels;

namespace MyExerciseJournal.Components.Pages
{
    public partial class Register
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        RegisterViewModel model { get; set; }
        bool success;


        public class RegisterAccountForm
        {
            
        }

        private void OnValidSubmit(EditContext context)
        {
            success = true;
            StateHasChanged();
            model.RegisterNewUser();
            NavigationManager.NavigateTo("/login", true);
        }
    }
}
