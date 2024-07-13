using Microsoft.AspNetCore.Components;
using MudBlazor;
using MyExerciseJournal.Models;
using MyExerciseJournal.Persistence;

namespace MyExerciseJournal.Components.SubComponents
{
    public partial class ExerciseSessions
    {
        [CascadingParameter] User CurrentUser { get; set; }

        [Inject]
        public ExerciseRepository Repo { get; set; }

        private List<UserSession> _sessions { get; set; }
        public UserSession? SelectedSession { get; set; }
        private SessionType? _sessionType;
        private bool IsNew;
        private bool isDialogOpen;

        protected override void OnInitialized()
        {
            if (CurrentUser != null)
            {
                _sessions = CurrentUser.Sessions ?? new();
            }
        }

        private void SetSelectedSession(DataGridRowClickEventArgs<UserSession> args)
        {
            if (SelectedSession == args.Item)
            {
                SelectedSession = null;
            }
            else
            {
                SelectedSession = args.Item;
            }

            //StateHasChanged();
        }
        private bool IsRowSelected(UserSession session)
        {
            return SelectedSession == session;
        }

        private string GetRowClass(UserSession session, int rowNumber)
        {
            return session == SelectedSession ? "selected-row" : string.Empty;
        }

        private void CreateNewSession()
        {
            SelectedSession = null;
            isDialogOpen = true;
            //StateHasChanged();
        }

        private void DeleteSession()
        {
            if (SelectedSession != null)
            {
                CurrentUser.Sessions.RemoveAll(s => s.Id == SelectedSession.Id);
                Repo.UpdateUser(CurrentUser);
            }
            SelectedSession = null;
        }

        private void HandleSave(UserSession session)
        {
            session.UserId = CurrentUser.Id;
            _sessions.Add(session);
            CurrentUser.Sessions = _sessions;
            Repo.UpdateUser(CurrentUser);
            isDialogOpen = false;
            SelectedSession = null;
        }
    }
}
