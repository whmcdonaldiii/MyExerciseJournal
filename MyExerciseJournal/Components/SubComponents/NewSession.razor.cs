using Microsoft.AspNetCore.Components;
using MyExerciseJournal.Models;

namespace MyExerciseJournal.Components.SubComponents
{
    public partial class NewSession
    {
        [Parameter] public EventCallback<UserSession> OnSave { get; set; }

        private SessionType? _selectedSessionType;
        private List<SessionType> sessionTypes;

        protected override void OnInitialized()
        {
            sessionTypes = UserSession.GetSessionTypes();
            StateHasChanged();
        }

        private async Task SaveSession()
        {
            if (_selectedSessionType != null)
            {
                UserSession userSesh = new();
                userSesh.Date = DateTime.Now;
                userSesh.Exercises = new();
                userSesh.SessionTypeId = (int)_selectedSessionType;
                await OnSave.InvokeAsync(userSesh);
            }
        }
    }
}
