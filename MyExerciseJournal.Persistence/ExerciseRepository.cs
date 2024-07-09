using LiteDB;
using MyExerciseJournal.Models;

namespace MyExerciseJournal.Persistence
{
    public class ExerciseRepository
    {
        private readonly string _databasePath = @"C:\Temp\ExerciseData.db";
        private readonly LiteDatabase _database;
        public ExerciseRepository()
        {
            var directory = Path.GetDirectoryName(_databasePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            _database = new LiteDatabase(_databasePath);
        }

        public IEnumerable<User> GetAllUsers()
        {
            ILiteCollection<User> users = _database.GetCollection<User>("users");
            return users.FindAll();
        }

        public void InsertUser(User user)
        {
            ILiteCollection<User> users = _database.GetCollection<User>("users");
            users.Insert(user);
        }

        public void UpdateUser(User user)
        {
            var exercises = _database.GetCollection<Exercise>("exercises");
            var sessionsCollection = _database.GetCollection<UserSession>("sessions");
            var collection = _database.GetCollection<User>("users");
            foreach (UserSession session in user.Sessions)
            {
                foreach (Exercise exercise in session.Exercises)
                {
                    if (exercise.Id == 0)
                        exercises.Insert(exercise);
                    else
                        exercises.Update(exercise);

                }

                if (session.Id == 0)
                    sessionsCollection.Insert(session);
                else
                    sessionsCollection.Update(session);

            }

            collection.Update(user);

        }

        public void DeleteExercise(Exercise exercise)
        {
            var exercises = _database.GetCollection<Exercise>("exercises");

            exercises.Delete(exercise.Id);
        }

        public void DeleteSession(UserSession session)
        {
            var sessions = _database.GetCollection<UserSession>("sessions");
            sessions.Delete(session.Id);
        }

        public IEnumerable<UserSession> GetAllSessionsByUserId(int userId)
        {
            ILiteCollection<UserSession> sessions = _database.GetCollection<UserSession>("sessions");

            return sessions.Find(x => x.UserId == userId);
        }

    }
}
