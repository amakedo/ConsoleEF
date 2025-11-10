namespace ConsoleEF.DAL
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Adress { get; set; }

        public string Email { get; set; }

        public int Year { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
