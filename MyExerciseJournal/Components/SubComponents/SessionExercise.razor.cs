using Microsoft.AspNetCore.Components;
using MyExerciseJournal.Models;

namespace MyExerciseJournal.Components.SubComponents
{
    public partial class SessionExercise
    {
        [Parameter] public Exercise? Exercise { get; set; }
        [Parameter] public SessionType? SelectedSessionType { get; set; }
        [Parameter] public EventCallback<Exercise> OnSave { get; set; }

        protected override void OnParametersSet()
        {
            SetExerciseList();
        }

        private string SelectedExercise
        {
            get => Exercise.Name;
            set => Exercise.Name = value;
        }

        private int Sets
        {
            get => Exercise.Sets;
            set => Exercise.Sets = value;
        }

        private int Reps
        {
            get => Exercise.Reps;
            set => Exercise.Reps = value;
        }

        private decimal Weight
        {
            get => Exercise.Weight;
            set => Exercise.Weight = value;
        }

        private int RestTime
        {
            get => Exercise.RestTime;
            set => Exercise.RestTime = value;
        }

        private List<string> Exercises;

        private void SetExerciseList()
        {
            switch (SelectedSessionType)
            {
                case SessionType.Legs:
                    Exercises = new() { "Squat", "Deadlift" };
                    break;
                case SessionType.Push:
                    Exercises = new() { "Bench Press", "Overhead Press" };
                    break;
                case SessionType.Pull:
                    Exercises = new() { "Pull Up", "Barbell Rows" };
                    break;
                default:
                    Exercises = new() { "Squat", "Deadlift" };
                    break;
            }
        }

        private async Task SaveExercise()
        {
            Exercise.Name = SelectedExercise;
            await OnSave.InvokeAsync(Exercise);
        }
    }
}
