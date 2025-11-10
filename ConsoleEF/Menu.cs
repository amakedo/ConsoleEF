using ConsoleEF.DAL;

namespace ConsoleEF
{
    internal class Menu
    {
        private readonly StudentRepositoryDapper studentRepo;
        private readonly GroupRepositoryDapper groupRepo;

        public Menu()
        {
            studentRepo = new StudentRepositoryDapper();
            groupRepo = new GroupRepositoryDapper();
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
                Console.WriteLine("5. Manage Groups");
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
                    case "5":
                        ManageGroups();
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
            studentRepo.Add(newStudent);
            Console.WriteLine("Student added successfully.");
            Pause();
        }

        private void ListStudents()
        {
            Console.Clear();
            var students = studentRepo.GetAll();

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
                var student = studentRepo.GetById(editId);
                if (student != null)
                {
                    InputStudentData(student, isNew: false);
                    studentRepo.Update(student);
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
                var student = studentRepo.GetById(deleteId);
                if (student != null)
                {
                    Console.Write($"Are you sure you want to delete {student.Name}? (y/n): ");
                    var confirm = Console.ReadLine()?.ToLower();
                    if (confirm == "y")
                    {
                        studentRepo.Remove(student);
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

            var groupRepo = new GroupRepositoryDapper();
            var groups = groupRepo.GetAll();

            if (groups.Count == 0)
            {
                Console.WriteLine("No groups found. Please add a group first!");
                Pause();
                return;
            }

            Console.WriteLine("\nAvailable Groups:");
            foreach (var g in groups)
                Console.WriteLine($"{g.Id}. {g.Name} ({g.Department})");
            Console.Write("Enter Group Id: ");
            if (int.TryParse(Console.ReadLine(), out int groupId) && groups.Any(g => g.Id == groupId))
            {
                student.GroupId = groupId;
            }
            else
            {
                Console.WriteLine("Invalid Group Id. Student not added.");
                student.GroupId = 0;
            }
        }


        private void repo_AddStudentToGroup(Student student)
        {
            var groups = groupRepo.GetAll();
            if (groups.Count == 0)
            {
                Console.WriteLine("No groups found. Please add a group first!");
                Pause();
                return;
            }

            Console.WriteLine("\nAvailable Groups:");
            foreach (var g in groups)
                Console.WriteLine($"{g.Id}. {g.Name} ({g.Department})");

            Console.Write("Enter Group ID: ");
            if (int.TryParse(Console.ReadLine(), out int groupId))
                student.GroupId = groupId;
            else
                Console.WriteLine("Invalid input. Student will not be linked to a group.");
        }


        private void ManageGroups()
        {
            bool groupMenu = true;
            while (groupMenu)
            {
                Console.Clear();
                Console.WriteLine("====== GROUP MANAGEMENT ======");
                Console.WriteLine("1. Add Group");
                Console.WriteLine("2. List Groups");
                Console.WriteLine("3. Delete Group");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddGroup();
                        break;
                    case "2":
                        ListGroups();
                        break;
                    case "3":
                        DeleteGroup();
                        break;
                    case "0":
                        groupMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        private void AddGroup()
        {
            Console.Clear();
            Console.Write("Enter Group Name: ");
            var name = Console.ReadLine();

            Console.Write("Enter Department: ");
            var dept = Console.ReadLine();

            groupRepo.Add(new Group { Name = name, Department = dept });
            Console.WriteLine("Group added successfully.");
            Pause();
        }

        private void ListGroups()
        {
            Console.Clear();
            var groups = groupRepo.GetAll();

            if (groups.Count == 0)
                Console.WriteLine("No groups found.");
            else
            {
                Console.WriteLine("====== GROUPS ======");
                foreach (var g in groups)
                    Console.WriteLine($"Id: {g.Id}, Name: {g.Name}, Department: {g.Department}");
            }

            Pause();
        }

        private void DeleteGroup()
        {
            Console.Clear();
            Console.Write("Enter Group Id to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var group = groupRepo.GetById(id);
                if (group == null)
                {
                    Console.WriteLine("Group not found.");
                }
                else
                {
                    Console.Write($"Are you sure you want to delete {group.Name}? (y/n): ");
                    var confirm = Console.ReadLine()?.ToLower();
                    if (confirm == "y")
                    {
                        groupRepo.Remove(id);
                        Console.WriteLine("Group deleted successfully.");
                    }
                    else
                        Console.WriteLine("Deletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Id.");
            }
            Pause();
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }
    }
}
