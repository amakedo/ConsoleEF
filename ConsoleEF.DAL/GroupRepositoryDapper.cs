using System.Data.SqlClient;
using Dapper;

namespace ConsoleEF.DAL
{
    public class GroupRepositoryDapper
    {
        private string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Students;Integrated Security=True;Connect Timeout=30;";

        public List<Group> GetAll()
        {
            using (var db = new SqlConnection(connStr))
            {
                return db.Query<Group>("SELECT * FROM Groups").ToList();
            }
        }

        public Group GetById(int id)
        {
            using (var db = new SqlConnection(connStr))
            {
                return db.QueryFirstOrDefault<Group>("SELECT * FROM Groups WHERE Id = @Id", new { Id = id });
            }
        }

        public void Add(Group group)
        {
            using (var db = new SqlConnection(connStr))
            {
                string sql = @"INSERT INTO Groups (Name, Department)
                               VALUES (@Name, @Department)";
                db.Execute(sql, group);
            }
        }

        public void Update(Group group)
        {
            using (var db = new SqlConnection(connStr))
            {
                string sql = "UPDATE Groups SET Name = @Name, Department = @Department WHERE Id = @Id";
                db.Execute(sql, group);
            }
        }

        public void Remove(int id)
        {
            using (var db = new SqlConnection(connStr))
            {
                db.Execute("DELETE FROM Groups WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
