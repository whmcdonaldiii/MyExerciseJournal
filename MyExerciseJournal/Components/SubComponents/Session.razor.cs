using Microsoft.AspNetCore.Components;
using MudBlazor;
using MyExerciseJournal.Models;
using MyExerciseJournal.Persistence;

namespace MyExerciseJournal.Components.SubComponents
{
    public partial class Session
    {
        [CascadingParameter] User CurrentUser { get; set; }

        [Parameter]
        public UserSession? UserSession { get; set; }

        [Inject]
        public ExerciseRepository Repo { get; set; }

        private List<SessionType> sessionTypes { get; set; }
        private SessionType? _selectedSessionType;
        private UserSession? _userSession { get; set; }
        private List<Exercise> Exercises = new();
        private Exercise? selectedExercise = new();
        private bool isDialogOpen;

        protected override void OnInitialized()
        {
            sessionTypes = UserSession.GetSessionTypes();
        }

        protected override async Task OnParametersSetAsync()
        {
            _userSession = UserSession;
            Exercises = _userSession.Exercises;
            _selectedSessionType = (SessionType)_userSession.SessionTypeId;
            await InvokeAsync(StateHasChanged);
        }

        private void SetSelected(DataGridRowClickEventArgs<Exercise> args)
        {
            if (selectedExercise == args.Item)
                selectedExercise = null;
            else
                selectedExercise = args.Item;
        }

        private bool IsRowSelected(Exercise exercise)
        {
            return selectedExercise == exercise;
        }

        private string GetRowClass(Exercise exercise, int rowNumber)
        {
            return exercise == selectedExercise ? "selected-row" : string.Empty;
        }

        private void UpdateSelectionTypeId()
        {
            _userSession.SessionTypeId = (int)_selectedSessionType;
            UserSession.SessionTypeId = (int)_selectedSessionType;
        }

        private void AddExercise()
        {
            selectedExercise = new Exercise();
            isDialogOpen = true;
        }

        private void EditExercise()
        {
            if (selectedExercise != null)
                isDialogOpen = true;
        }

        private void DeleteExercise()
        {
            if (selectedExercise != null)
            {
                Exercises.RemoveAll(x => x.Id == selectedExercise.Id);
                CurrentUser.Sessions.First(x => x.Id == _userSession.Id).Exercises = Exercises;
                Repo.UpdateUser(CurrentUser);
            }
        }

        private void UpdateNotes()
        {
            CurrentUser.Sessions.First(x => x.Id == _userSession.Id).Notes = _userSession.Notes;
            Repo.UpdateUser(CurrentUser);
        }

        private void HandleSave(Exercise exercise)
        {
            if (!Exercises.Contains(exercise))
            {
                Exercises.Add(exercise);
                CurrentUser.Sessions.First(x => x.Id == _userSession.Id).Exercises = Exercises;
                Repo.UpdateUser(CurrentUser);
            }
            else
            {
                Exercises.First(x => x.Id == exercise.Id).Name = exercise.Name;
                Exercises.First(x => x.Id == exercise.Id).Sets = exercise.Sets;
                Exercises.First(x => x.Id == exercise.Id).Reps = exercise.Reps;
                Exercises.First(x => x.Id == exercise.Id).Weight = exercise.Weight;
                Exercises.First(x => x.Id == exercise.Id).RestTime = exercise.RestTime;
                CurrentUser.Sessions.First(x => x.Id == _userSession.Id).Exercises = Exercises;
                Repo.UpdateUser(CurrentUser);
            }

            isDialogOpen = false;
        }
    }
}
