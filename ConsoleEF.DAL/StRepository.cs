using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEF.DAL
{
    public class StRepository
    {
        private AppDbContext dbContext;
        public StRepository()
        {
            dbContext = new AppDbContext();
        }

        public void Add(Student student)
        {
            dbContext.Students.Add(student);
            dbContext.SaveChanges();
        }

        public List<Student> GetAll()
        {
            return dbContext.Students.ToList();
        }

        public Student GetById(int id)
        {
            return dbContext.Students.FirstOrDefault(s => s.Id == id);
        }

        public void Update(Student student)
        {
            dbContext.Students.Update(student);
            dbContext.SaveChanges();
        }

        public void Remove(Student student)
        {
            dbContext.Students.Remove(student);
            dbContext.SaveChanges();
        }
    }
}
