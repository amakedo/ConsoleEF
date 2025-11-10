using System.Data.SqlClient;
using Dapper;

namespace ConsoleEF.DAL
{
    public class StudentRepositoryDapper
    {
        private string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Students;Integrated Security=True;Connect Timeout=30;";

        public List<Student> GetAll()
            {
            using (var db = new SqlConnection(connStr))
            {
                string sql = @"
                    SELECT s.*, g.Id, g.Name, g.Department
                    FROM Students s
                    INNER JOIN Groups g ON s.GroupId = g.Id";

                var students = db.Query<Student, Group, Student>(
                    sql,
                    (student, group) =>
                    {
                        student.Group = group;
                        return student;
                    },
                    splitOn: "Id"
                ).ToList();

                return students;
            }
        }

        public Student GetById(int id)
        {
            using (var db = new SqlConnection(connStr))
            {
                string sql = @"
                    SELECT s.*, g.Id, g.Name, g.Department
                    FROM Students s
                    INNER JOIN Groups g ON s.GroupId = g.Id
                    WHERE s.Id = @Id";

                return db.Query<Student, Group, Student>(
                    sql,
                    (student, group) =>
                    {
                        student.Group = group;
                        return student;
                    },
                    new { Id = id },
                    splitOn: "Id"
                ).FirstOrDefault();
            }
        }

        public void Add(Student student)
        {
            using (var db = new SqlConnection(connStr))
            {
                string sql = @"
                    INSERT INTO Students (Name, Age, Email, Adress, Year, GroupId)
                    VALUES (@Name, @Age, @Email, @Adress, @Year, @GroupId)";
                db.Execute(sql, student);
            }
        }

        public void Update(Student student)
        {
            using (var db = new SqlConnection(connStr))
            {
                string sql = @"
                    UPDATE Students 
                    SET Name = @Name, 
                        Age = @Age, 
                        Email = @Email, 
                        Adress = @Adress, 
                        Year = @Year,
                        GroupId = @GroupId
                    WHERE Id = @Id";
                db.Execute(sql, student);
            }
        }

        public void Remove(Student student)
        {
            using (var db = new SqlConnection(connStr))
            {
                string sql = "DELETE FROM Students WHERE Id = @Id";
                db.Execute(sql, new { student.Id });
            }
        }
    }
}
