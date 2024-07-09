using System;

namespace MyExerciseJournal.Models
{
    public class UserSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int? SessionTypeId { get; set; }
        public string? Notes { get; set; }
        public List<Exercise> Exercises { get; set; } = new();

        public static List<SessionType> GetSessionTypes()
        {
            // Retrieve all enum values using Enum.GetValues
            var enumValues = Enum.GetValues(typeof(SessionType)).Cast<SessionType>().ToList();
            return enumValues;
        }
    }

    public enum SessionType
    {
        Legs = 1,
        Push = 2,
        Pull = 3
    }

   
}
