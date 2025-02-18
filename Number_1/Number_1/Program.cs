using System;
using System.Collections.Generic;


Dictionary<string, (int count, List<(int id, string name)> students)> courses = new();
int studentIdCounter = 1;

while (true)
{
    Console.WriteLine("1. Добавить курс");
    Console.WriteLine("2. Посмотреть студентов выбранного курса");
    Console.WriteLine("3. Удалить курс");
    Console.WriteLine("4. Записать студента на курс");
    Console.WriteLine("5. Показать студентов");
    Console.WriteLine("6. Удалить студента");
    Console.WriteLine("7. Выход");
    Console.Write("Выберите опцию: ");
    string choice = Console.ReadLine();
    void AddCourse()
    {
        Console.Write("Введите название курса: ");
        string name = Console.ReadLine();
        Console.Write("Количество студентов: ");
        int count = int.Parse(Console.ReadLine());
        courses[name] = (count, new List<(int id, string name)>());
        Console.WriteLine("Курс был добавлен.");
    }

    void SeeCourse()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        if (courses.TryGetValue(name, out var course))
            Console.WriteLine($"Курс: {name}, Кол-во студентов: {course.count}, Студентов: {course.students.Count}");
        else
            throw new Exception("Курс не найден.");
    }
    void KillCourse()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        if (courses.Remove(name))
            Console.WriteLine("Курс удален.");
        else
            throw new Exception("Курс не найден.");
    }

    void EnrollStudent()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        Console.Write("Имя студента: ");
        string studentName = Console.ReadLine();
        if (courses.TryGetValue(name, out var course) && course.students.Count < course.count)
        {
            course.students.Add((studentIdCounter++, studentName));
            Console.WriteLine("Студент записан.");
        }
        else
            throw new Exception("Курс не найден или нет мест.");
    }

    void SeeStudents()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        if (courses.TryGetValue(name, out var course))
        {
            Console.WriteLine($"Студенты на курсе {name}:");
            foreach (var student in course.students)
                Console.WriteLine($"ID: {student.id}, Имя: {student.name}");
        }
        else
            throw new Exception("Курс не найден.");
    }

    void KillStudent()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        Console.Write("ID студента: ");
        if (int.TryParse(Console.ReadLine(), out int studentId))
        {
            if (courses.TryGetValue(name, out var course))
            {
                var student = course.students.Find(s => s.id == studentId);
                if (student != default)
                {
                    course.students.Remove(student);
                    Console.WriteLine("Студент был удален.");
                }
                else
                {
                    throw new Exception("Студент не был найден.");
                }
            }
            else
            {
                throw new Exception("Курс не был найден.");
            }
        }
        else
        {
            throw new Exception("Неверный ID студента.");
        }
    }
    try
    {
        switch (choice)
        {
            case "1": 
                AddCourse(); 
                break;
            case "2": 
                SeeCourse();
                break;
            case "3": 
                KillCourse(); 
                break;
            case "4": 
                EnrollStudent(); 
                break;
            case "5":
                SeeStudents(); 
                break;
            case "6":
                KillStudent(); 
                break;
            case "7":
                return;
            default: 
                Console.WriteLine("Вы ввели неверное число."); 
                break;
        }
    }
    catch (Exception massage)
    {
        Console.WriteLine($"Ошибка: {massage.Message}");
    }
}



