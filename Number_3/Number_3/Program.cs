using System;
using System.Collections.Generic;

enum Subject
{
    Math,
    IT,
    Chemestry,
    History,
    PE
}

struct Grade
{
    public Subject Subject;
    public int Score;
    public DateTime Date;
}

class Program
{
    static List<Grade> grades = new List<Grade>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Добавить оценку");
            Console.WriteLine("2. Показать оценки");
            Console.WriteLine("3. Удалить оценку");
            Console.WriteLine("4. Выход");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": 
                    AddGrade(); 
                    break;
                case "2": 
                    ViewGrades(); 
                    break;
                case "3": 
                    RemoveGrade(); 
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Неправильно введено число!"); 
                    break;
            }
        }
    }

    static void AddGrade()
    {
        Console.WriteLine("Выберите предмет (0 - Math, 1 - IT, 2 - Chemestry, 3 - History, 4 - PE): ");
        string mark = Console.ReadLine();
        if (int.TryParse(mark, out int subjectIndex) && Enum.IsDefined(typeof(Subject), subjectIndex))
        {
            Subject subject = (Subject)subjectIndex;
            Console.Write("Введите оценку: ");
            string mark2 = Console.ReadLine();
            if (int.TryParse(mark2, out int score) && score >= 0 && score <= 5)
            {
                grades.Add(new Grade { Subject = subject, Score = score, Date = DateTime.Now });
                Console.WriteLine("Оценка добавлена");
            }
            else
            {
                Console.WriteLine("Некорректная оценка");
            }
        }
        else
        {
            Console.WriteLine("Некоррекно выбран предмет");
        }
    }

    static void ViewGrades()
    {
        if (grades.Count == 0)
        {
            Console.WriteLine("Нет оценок");
            return;
        }
        Console.WriteLine("Список оценок:");
        for (int i = 0; i < grades.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Предмет: {grades[i].Subject}, Оценка: {grades[i].Score}, Дата: {grades[i].Date.ToString()}");
        }
    }

    static void RemoveGrade()
    {
        Console.Write("Введите номер оценки: ");
        if (int.TryParse(Console.ReadLine(), out int gradeIndex) && gradeIndex > 0 && gradeIndex <= grades.Count)
        {
            grades.RemoveAt(gradeIndex - 1);
            Console.WriteLine("Оценка удалена");
        }
        else
        {
            Console.WriteLine("Некорректный номер оценки");
        }
    }
}
