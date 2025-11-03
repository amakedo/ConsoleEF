using ConsoleEF.DAL;

namespace ConsoleEF
{
    internal class Menu
    {
        private readonly StRepository repo;

        public Menu()
        {
            repo = new StRepository();
        }

        public void Show()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("====== STUDENT MANAGEMENT ======");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. List Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ListStudents();
                        break;
                    case "3":
                        UpdateStudent();
                        break;
                    case "4":
                        DeleteStudent();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Pause();
                        break;
                }
            }
        }

        private void AddStudent()
        {
            Console.Clear();
            var newStudent = new Student();
            InputStudentData(newStudent, isNew: true);
            repo.Add(newStudent);
            Console.WriteLine("Student added successfully.");
            Pause();
        }

        private void ListStudents()
        {
            Console.Clear();
            var students = repo.GetAll();

            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                Console.WriteLine("====== STUDENTS ======");
                foreach (var s in students)
                {
                    Console.WriteLine($"Id: {s.Id}, Name: {s.Name}, Age: {s.Age}, Email: {s.Email}, Address: {s.Adress}");
                }
            }

            Pause();
        }

        private void UpdateStudent()
        {
            Console.Clear();
            Console.Write("Enter Student Id to update: ");

            if (int.TryParse(Console.ReadLine(), out int editId))
            {
                var student = repo.GetById(editId);
                if (student != null)
                {
                    InputStudentData(student, isNew: false);
                    repo.Update(student);
                    Console.WriteLine("Student updated successfully.");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Id.");
            }

            Pause();
        }

        private void DeleteStudent()
        {
            Console.Clear();
            Console.Write("Enter Student Id to delete: ");

            if (int.TryParse(Console.ReadLine(), out int deleteId))
            {
                var student = repo.GetById(deleteId);
                if (student != null)
                {
                    Console.Write($"Are you sure you want to delete {student.Name}? (y/n): ");
                    var confirm = Console.ReadLine()?.ToLower();
                    if (confirm == "y")
                    {
                        repo.Remove(student);
                        Console.WriteLine("Student deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Deletion cancelled.");
                    }
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Id.");
            }

            Pause();
        }

        private void InputStudentData(Student student, bool isNew)
        {
            Console.Write("Enter Name" + (isNew ? ": " : $" (current: {student.Name}): "));
            var name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name) || isNew)
                student.Name = name ?? student.Name;

            Console.Write("Enter Age" + (isNew ? ": " : $" (current: {student.Age}): "));
            var ageInput = Console.ReadLine();
            if (int.TryParse(ageInput, out int newAge) || (isNew && !string.IsNullOrWhiteSpace(ageInput)))
                student.Age = string.IsNullOrEmpty(ageInput) ? student.Age : newAge;

            Console.Write("Enter Email" + (isNew ? ": " : $" (current: {student.Email}): "));
            var email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email) || isNew)
                student.Email = email ?? student.Email;

            Console.Write("Enter Address" + (isNew ? ": " : $" (current: {student.Adress}): "));
            var address = Console.ReadLine();
            if (!string.IsNullOrEmpty(address) || isNew)
                student.Adress = address ?? student.Adress;
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }
    }
}
