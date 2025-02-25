using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace GradeStud
{
    public enum Subject
    {
        Math,
        IT,
        Chemestry,
        History,
        PE
    }

    public interface Interface
    {
        void AddGrade();
        void ViewGrades();
        void RemoveGrade();
        void SaveReport(); 
    }

    public class Grade
    {
        public Subject Subject { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }

        public Grade() { }

        public Grade(Subject subject, int score)
        {
            Subject = subject;
            Score = score;
            Date = DateTime.Now;
        }
    }

    public class Student
    {
        public string Name { get; set; } 
        public List<Grade> Grades { get; set; } = new List<Grade>();
    }

    public class Menu : Interface
    {
        private List<Student> students = new List<Student>();
        public delegate void NotificationHandler(string message);
        public event NotificationHandler Notification;

        public Menu()
        {
            Notification += message => Console.WriteLine($"Уведомление: {message}");
        }

        public void AddGrade()
        {
            Console.Write("Введите имя студента: ");
            string name = Console.ReadLine();
            Student student = students.Find(s => s.Name == name) ?? new Student { Name = name };
            if (!students.Contains(student)) students.Add(student);

            Console.Write("Выберите предмет (0 - Math, 1 - IT, 2 - Chemestry, 3 - History, 4 - PE): ");
            if (int.TryParse(Console.ReadLine(), out int subjectIndex) && Enum.IsDefined(typeof(Subject), subjectIndex))
            {
                Console.Write("Введите оценку: ");
                if (int.TryParse(Console.ReadLine(), out int score) && score >= 0 && score <= 5)
                {
                    student.Grades.Add(new Grade((Subject)subjectIndex, score));
                    Notification?.Invoke($"Оценка добавлена студенту {student.Name}");
                }
                else
                {
                    Console.WriteLine("Некорректная оценка");
                }
            }
            else
            {
                Console.WriteLine("Некорректный предмет");
            }
        }

        public void ViewGrades()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Нет студентов с оценками");
                return;
            }
            foreach (var student in students)
            {
                Console.WriteLine($"Студент: {student.Name}");
                foreach (var grade in student.Grades)
                {
                    Console.WriteLine($"  Предмет: {grade.Subject}, Оценка: {grade.Score}, Дата: {grade.Date}");
                }
            }
        }

        public void RemoveGrade()
        {
            Console.Write("Введите имя студента: ");
            string name = Console.ReadLine();
            Student student = students.Find(stud => stud.Name == name);
            if (student == null || student.Grades.Count == 0)
            {
                Console.WriteLine("Нет оценок для удаления");
                return;
            }
            Console.Write("Введите индекс оценки: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < student.Grades.Count)
            {
                student.Grades.RemoveAt(index);
                Notification?.Invoke($"Оценка удалена у студента {student.Name}");
            }
            else
            {
                Console.WriteLine("Некорректный индекс");
            }
        }

        public void SaveReport()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));
            using (FileStream fileXML = new FileStream(@"D:\отчет.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fileXML, students);
            }
            Notification?.Invoke("Отчет сохранен в формате XML");
        }
    } 

    class Program
    {
        static void Main()
        {
            Menu gradeMenu = new Menu(); 

            while (true)
            {
                Console.WriteLine("1. Добавить оценку");
                Console.WriteLine("2. Показать оценки");
                Console.WriteLine("3. Удалить оценку");
                Console.WriteLine("4. Сохранить отчет в XML"); 
                Console.WriteLine("5. Выход");
                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        gradeMenu.AddGrade();
                        break;
                    case "2":
                        gradeMenu.ViewGrades();
                        break;
                    case "3":
                        gradeMenu.RemoveGrade();
                        break;
                    case "4":
                        gradeMenu.SaveReport();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Ошибка");
                        break;
                }
            }
        }
    }
}