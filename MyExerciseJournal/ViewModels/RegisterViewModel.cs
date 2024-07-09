using CommunityToolkit.Mvvm.ComponentModel;
using MyExerciseJournal.Models;
using MyExerciseJournal.Persistence;
using System.ComponentModel.DataAnnotations;

namespace MyExerciseJournal.ViewModels
{
    public class RegisterViewModel : ObservableObject
    {
        private readonly ExerciseRepository _repo;

        public RegisterViewModel(ExerciseRepository repo)
        {
            _repo = repo;
        }

        [Required]
        [StringLength(16, ErrorMessage = "Name length can't be more than 16.")]
        public string Username { get; set; } = "";

        public void RegisterNewUser()
        {
            List<User> users = _repo.GetAllUsers().ToList();

            if(!users.Any(x=>x.Name.ToLower() == Username.ToLower()))
            {
                _repo.InsertUser(new() { Name = Username });
            }
        }
    }
}
