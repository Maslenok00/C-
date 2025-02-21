using System;
using System.Collections.Generic;

namespace GradeStud
{
    enum Subject
    {
        Math,
        IT,
        Chemestry,
        History,
        PE
    }

    interface Interface
    {
        void AddGrade();
        void ViewGrades();
        void RemoveGrade();
    }

    class Grade
    {
        public Subject Subject { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }

        public Grade(Subject subject, int score)
        {
            Subject = subject;
            Score = score;
            Date = DateTime.Now;
        }
    }

    class BaseInfo
    {
        protected List<Grade> marks = new List<Grade>();
    }

    class MainProgramm : BaseInfo, Interface
    {
        public void AddGrade()
        {
            Console.WriteLine("Выберите предмет (0 - Math, 1 - IT, 2 - Chemestry, 3 - History, 4 - PE): ");
            if (int.TryParse(Console.ReadLine(), out int subjectIndex) && Enum.IsDefined(typeof(Subject), subjectIndex))
            {
                Subject subject = (Subject)subjectIndex;
                Console.Write("Введите оценку: ");
                if (int.TryParse(Console.ReadLine(), out int score) && score >= 0 && score <= 5)
                {
                    marks.Add(new Grade(subject, score));
                    Console.WriteLine("Оценка успешно добавлена");
                }
                else
                {
                    Console.WriteLine("Некорректно введена оценка");
                }
            }
            else
            {
                Console.WriteLine("Некорректно выбран предмет");
            }
        }

        public void ViewGrades()
        {
            if (marks.Count == 0)
            {
                Console.WriteLine("Нет оценок");
                return;
            }
            Console.WriteLine("Список оценок:");
            foreach (var grade in marks)
            {
                Console.WriteLine($"Предмет: {grade.Subject}, Оценка: {grade.Score}, Дата: {grade.Date}");
            }
        }

        public void RemoveGrade()
        {
            Console.Write("Введите номер оценки: ");
            if (int.TryParse(Console.ReadLine(), out int gradeIndex) && gradeIndex > 0 && gradeIndex <= marks.Count)
            {
                marks.RemoveAt(gradeIndex - 1);
                Console.WriteLine("Оценка успешно удалена");
            }
            else
            {
                Console.WriteLine("Некорректный указан номер оценки");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Interface gradeMenu = new MainProgramm();

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
                        gradeMenu.AddGrade();
                        break;
                    case "2":
                        gradeMenu.ViewGrades();
                        break;
                    case "3":
                        gradeMenu.RemoveGrade(); 
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Ошибка"); 
                        break;
                }
            }
        }
    }
}