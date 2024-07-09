using CommunityToolkit.Mvvm.ComponentModel;
using MyExerciseJournal.Models;
using MyExerciseJournal.Persistence;

namespace MyExerciseJournal.ViewModels
{
    public class RegisterViewModel : ObservableObject
    {
        private readonly ExerciseRepository _repo;

        public RegisterViewModel(ExerciseRepository repo)
        {
            _repo = repo;
        }

        public void RegisterNewUser(string name)
        {
            List<User> users = _repo.GetAllUsers().ToList();

            if(!users.Any(x=>x.Name.ToLower() == name.ToLower()))
            {
                _repo.InsertUser(new() { Name = name });
            }
        }
    }
}
