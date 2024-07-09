using System.ComponentModel.DataAnnotations;

namespace MyExerciseJournal.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(16, ErrorMessage = "Name length can't be more than 16.")]
        public string Username { get; set; } = "";
    }
}
