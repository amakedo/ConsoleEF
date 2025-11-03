using Microsoft.EntityFrameworkCore;
namespace ConsoleEF.DAL
{
    public class AppDbContext : DbContext
    {

        private string ConnectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Students;Integrated Security=True;Connect Timeout=30;";
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
