namespace ConsoleEF.DAL
{
    public class StudentWithGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Email { get; set; } = "";
        public string Adress { get; set; } = "";
        public int Year { get; set; }
        public string GroupName { get; set; } = "— no group —";
    }
}
