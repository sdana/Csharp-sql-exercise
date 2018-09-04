using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Collections;
using Dapper;

namespace nss.Data
{
    public class DatabaseInterface
    {
        public static SqliteConnection Connection
        {
            get
            {
                /*
                    Mac users: You can create an environment variable in your
                    .zshrc file.
                        export NSS_DB="/path/to/your/project/nss.db"

                    Windows users: You need to use a property window
                        http://www.forbeslindesay.co.uk/post/42833119552/permanently-set-environment-variables-on-windows
                 */
                string env = $"{Environment.GetEnvironmentVariable("NSS_DB")}";
                string _connectionString = $"Data Source={env}";
                return new SqliteConnection(_connectionString);
            }
        }


        public static void CheckCohortTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Cohort> cohorts = db.Query<Cohort>
                    ("SELECT Id FROM Cohort").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    db.Execute(@"CREATE TABLE Cohort (
                        `Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `Name`	TEXT NOT NULL UNIQUE
                    )");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Evening Cohort 1')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 10')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 11')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 12')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 13')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 21')");
                    
                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 26')");

                }
            }
        }

        public static void CheckInstructorsTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Instructor> toys = db.Query<Instructor>
                    ("SELECT Id FROM Instructor").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    db.Execute($@"CREATE TABLE Instructor (
                        `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `FirstName`	varchar(80) NOT NULL,
                        `LastName`	varchar(80) NOT NULL,
                        `SlackHandle`	varchar(80) NOT NULL,
                        `Specialty` varchar(80),
                        `CohortId`	integer NOT NULL,
                        FOREIGN KEY(`CohortId`) REFERENCES `Cohort`(`Id`)
                    )");

                    db.Execute($@"INSERT INTO Instructor
                        SELECT null,
                              'Steve',
                              'Brownlee',
                              '@coach',
                              'Dad jokes',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Evening Cohort 1'
                    ");

                    db.Execute($@"INSERT INTO Instructor
                        SELECT null,
                              'Joe',
                              'Shepherd',
                              '@joes',
                              'Analogies',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Day Cohort 13'
                    ");

                    db.Execute($@"INSERT INTO Instructor
                        SELECT null,
                              'Jisie',
                              'David',
                              '@jisie',
                              'Student success',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Day Cohort 21'
                    ");
                }
            }
        }

        public static void CheckExercisesTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Exercise> exercises = db.Query<Exercise>("SELECT Id FROM Exercise").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    db.Execute(@"
                        CREATE TABLE Exercise (
                            `Id` integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                            'Name' varchar(50) NOT NULL,
                            'Language' varchar(20) NOT NULL
                        )
                    ");
                    
                    db.Execute(@"
                        INSERT INTO Exercise
                        VALUES(null, 'Kill Nickelback', 'C#')
                    ");

                    db.Execute(@"
                        INSERT INTO Exercise
                        VALUES(null, 'Family Dictionary', 'C#')
                    ");

                    db.Execute(@"
                        INSERT INTO Exercise
                        VALUES(null, 'Dream Team', 'C#')
                    ");

                    db.Execute(@"
                        INSERT INTO Exercise
                        VALUES(null, 'Nutshell', 'JavaScript')
                    ");

                    db.Execute(@"
                        INSERT INTO Exercise
                        VALUES(null, 'Dynamic Cards', 'JavaScript')
                    ");

                    db.Execute(@"
                        INSERT INTO Exercise
                        VALUES(null, 'CSS Variables', 'CSS')
                    ");

                    db.Execute(@"
                        INSERT INTO Exercise
                        VALUES(null, 'Personality Webpage', 'HTML')
                    ");
                }
            }
        }

        public static void CheckStudentTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Student> students = db.Query<Student>
                    ("SELECT Id FROM Student").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    db.Execute($@"CREATE TABLE Student (
                        `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `FirstName`	varchar(80) NOT NULL,
                        `LastName`	varchar(80) NOT NULL,
                        `SlackHandle`	varchar(80) NOT NULL,
                        `CohortId`	integer NOT NULL,
                        FOREIGN KEY(`CohortId`) REFERENCES `Cohort`(`Id`)
                    )");

                    db.Execute(@"
                        INSERT INTO Student
                                SELECT null,
                                'Seth',
                                'Dana',
                                '@sdana',
                                c.Id
                                FROM Cohort c WHERE c.Name = 'Day Cohort 26'
                    ");

                    db.Execute(@"
                        INSERT INTO Student
                                SELECT null,
                                'Robert',
                                'Leedy',
                                '@rleedy',
                                c.Id
                                FROM Cohort c WHERE c.Name = 'Day Cohort 26'
                    ");

                    db.Execute(@"
                        INSERT INTO Student
                                SELECT null,
                                'Adelaide',
                                'Yoder',
                                '@coderYoder',
                                c.Id
                                FROM Cohort c WHERE c.Name = 'Day Cohort 26'
                    ");

                    db.Execute(@"
                        INSERT INTO Student
                                SELECT null,
                                'The',
                                'Dude',
                                '@WheresMyRug',
                                c.Id
                                FROM Cohort c WHERE c.Name = 'Day Cohort 13'
                    ");

                    db.Execute(@"
                        INSERT INTO Student
                                SELECT null,
                                'Jordan',
                                'Castello',
                                '@jcast',
                                c.Id
                                FROM Cohort c WHERE c.Name = 'Day Cohort 21'
                    ");
                }
            }
        }

        public static void CheckStudentExerciseTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<StudentExercise> studentExercise = db.Query<StudentExercise>
                    ("SELECT Id FROM StudentExercise").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    StudentExercise.Create(db);

                    db.Execute(@"
                        INSERT INTO StudentExercise
                        SELECT null,
                        1,
                        e.Id,
                        1
                        FROM Exercise e WHERE e.Name = 'Nutshell'
                    ");
                }
            }
        }
    }
}