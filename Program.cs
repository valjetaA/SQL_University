using University.Models;
using University.Repositories;
using System;
using System.Collections.Generic;

namespace University
{
    class Program
    {
        private static string _connectionString = @"Data Source=DESKTOP-UUL0SEC\SQLEXPRESS;Database=University;Trusted_Connection=True";

        static void Main(string[] args)
        {
            IStudentRepository studentRepository = new StudentRawSqlRepository(_connectionString);
            IGroupRepository groupRepository = new GroupRawSqlRepository(_connectionString);
            IStudentInGroupRepository studentInGroupRepository = new StudentInGroupRawSqlRepository(_connectionString);

            Console.WriteLine("Доступные команды:");
            Console.WriteLine("add-student - добавить студента");
            Console.WriteLine("add-group - добавить группу");
            Console.WriteLine("add-student-in-group - добавить студента в группу");
            Console.WriteLine("print-students - вывести список студентов");
            Console.WriteLine("print-groups - вывести список групп");
            Console.WriteLine("show-student-by-group-id - вывести студентов по id группы");
            Console.WriteLine("exit - выход из приложения");

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "add-student")
                {
                    Console.WriteLine("Введите имя студента");
                    string name = Console.ReadLine();
                    if (name.Length == 0)
                    {
                        Console.WriteLine("Имя студента введено некорректно");
                        continue;
                    }

                    Console.WriteLine("Введите возраст студента");
                    if (!Int32.TryParse(Console.ReadLine(), out int age ))
                    {
                        Console.WriteLine("Возраст студента введен некорректно");
                        continue;
                    }

                    studentRepository.Add(new Student
                    {
                        Name = name,
                        Age = age
                    });
                    Console.WriteLine("Успешно добавлено.");
                }
                else if (command == "add-group")
                {
                    Console.WriteLine("Введите название группы.");
                    string name = Console.ReadLine();
                    if (name.Length == 0)
                    {
                        Console.WriteLine("Название группы не было введено.");
                        continue;
                    }

                    groupRepository.Add(new Group
                    {
                        Name = name
                    });
                    Console.WriteLine("Успешно добавлено");
                }
                else if (command == "add-student-in-group")
                {
                    int groupId;

                    Console.WriteLine("Введите id студента, которого хотите добавить в группу ");
                    if ( !int.TryParse( Console.ReadLine(), out int studentId ) )
                    {
                        Console.WriteLine( "Id введено некорректно." );
                        continue;
                    }
                    Console.WriteLine("Введите id группы, в которую хотите добавить студента");
                    groupId = Convert.ToInt32(Console.ReadLine());

                    studentInGroupRepository.Add(new StudentInGroup
                    {
                        StudentId = studentId,
                        GroupId = groupId
                    });
                    Console.WriteLine("Успешно добавлено");
                }
                else if (command == "print-students")
                {
                    List<Student> students = studentRepository.GetAll();
                    if (students.Count < 1)
                    {
                        Console.WriteLine("В группе нет ни одного студента.");
                        continue;
                    }
                    foreach (Student student in students)
                    {
                        Console.WriteLine($"Id: {student.Id}, Name: {student.Name}");
                    }
                }
                else if (command == "print-groups")
                {
                    List<Group> groups = groupRepository.GetAll();
                    if (groups.Count < 1)
                    {
                        Console.WriteLine("Группы не были созданы.");
                        continue;
                    }
                    foreach (Group group in groups)
                    {
                        Console.WriteLine($"Id: {group.Id}, Name: {group.Name}");
                    }
                }
                else if (command == "show-student-by-group-id")
                {
                    Console.WriteLine("Введите id группы");
                    int groupsId = int.Parse(Console.ReadLine());
                    List<StudentInGroup> studentsInGroup = studentInGroupRepository.GetByStudentAndGroupIds();
                    var student = new Student();
                    foreach (var studentInGroup in studentsInGroup)
                    {
                        student = studentRepository.GetById(studentInGroup.StudentId);
                        Console.WriteLine($"Id: {student.Id}, Name: {student.Name}");
                    }

                }
                else if (command == "exit")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Команда не найдена");
                }
            }
        }
    }
}