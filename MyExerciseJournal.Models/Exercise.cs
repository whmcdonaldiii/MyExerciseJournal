namespace MyExerciseJournal.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int ExerciseTypeId { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public int RestTime { get; set; }
    }
}
