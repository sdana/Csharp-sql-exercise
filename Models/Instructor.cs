using Dapper;
using Microsoft.Data.Sqlite;

namespace nss.Data
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public string Specialty { get; set; }
        public Cohort Cohort { get; set; }

        public void AssignExercise(Exercise e, Student s)
        {
            SqliteConnection db = DatabaseInterface.Connection;

            db.Execute($@"INSERT INTO StudentExercise
                        (ExerciseId, StudentId, InstructorId)
                        VALUES
                        ({e.Id}, {s.Id}, {this.Id})");
        }
    }
}
