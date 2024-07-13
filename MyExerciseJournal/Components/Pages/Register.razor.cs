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

        private void OnValidSubmit(EditContext context)
        {
            success = true;
            model.RegisterNewUser();
            NavigationManager.NavigateTo("/login", true);
        }
    }
}
