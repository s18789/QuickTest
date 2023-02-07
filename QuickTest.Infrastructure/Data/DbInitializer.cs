using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Data;
public class DbInitializer
{
    public static void FillDatabaseRandomData(DataContext context)
    {
        /*var question1 = new Question()
        {
            QuestionContent = "kij i piłka kosztują razem 1 10 zł. piłka jest droższa od kija o 1 zł. ile kosztuje kij ?",
            Description = "opis",
            Title = "Zadanie z podstaw majmy",
            Points = 1,
            PredefinedAnswers = new List<PredefinedAnswer>()
            {
                new PredefinedAnswer()
                {
                    Content = "1 pln",
                    IsCorrect = false,
                },
                new PredefinedAnswer()
                {
                    Content = "1.10 pln",
                    IsCorrect = false,
                },
                new PredefinedAnswer()
                {
                    Content = "0.10 pln",
                    IsCorrect = true,
                },
                new PredefinedAnswer()
                {
                    Content = "5 groszy",
                    IsCorrect = false,
                },
            },
        };

        var question2 = new Question()
        {
            QuestionContent = "Który owoc zjadł Adam z Ewą nim zostali wypędzeni z raju ?",
            Description = "opis",
            Title = "Religioznactwo",
            Points = 1,
            PredefinedAnswers = new List<PredefinedAnswer>()
            {
                new PredefinedAnswer()
                {
                    Content = "jabłko",
                    IsCorrect = true,
                },
                new PredefinedAnswer()
                {
                    Content = "gruszkę",
                    IsCorrect = false,
                },
                new PredefinedAnswer()
                {
                    Content = "ananasa",
                    IsCorrect = false,
                },
                new PredefinedAnswer()
                {
                    Content = "pomidora",
                    IsCorrect = false,
                },
            },
        };

        var question3 = new Question()
        {
            QuestionContent = "Kto był pierwszym królem polski ?",
            Description = "opis",
            Title = "Podstawy histori",
            Points = 1,
            PredefinedAnswers = new List<PredefinedAnswer>()
            {
                new PredefinedAnswer()
                {
                    Content = "Mieszko I",
                    IsCorrect = false,
                },
                new PredefinedAnswer()
                {
                    Content = "Bolesław Chrobny",
                    IsCorrect = true,
                },
                new PredefinedAnswer()
                {
                    Content = "Mieszko II",
                    IsCorrect = false,
                },
                new PredefinedAnswer()
                {
                    Content = "Władysław Jagiello",
                    IsCorrect = false,
                },
            },
        };

        context.Add(question1);
        context.Add(question2);
        context.Add(question3);

        var exam1 = new Exam()
        {
            Title = "One question exam",
            Time = 1,
            CreationDate = DateTime.Now,
            Description = "Proste pytanko natury historyczno religijnej.",
            MaxPoints = 1,
            Questions = new List<Question>()
            {
                question2,
            }
        };

        var exam2 = new Exam()
        {
            Title = "All question exam",
            Time = 3,
            CreationDate = DateTime.Now,
            Description = "Prosty egzamin badający wiedzy z wielu dziedzin zycia",
            MaxPoints = 3,
            Questions = new List<Question>()
            {
                question1, question2, question3,
            }
        };

        var exam3 = new Exam()
        {
            Title = "Math & logic exam",
            Time = 1,
            CreationDate = DateTime.Now,
            Description = "Sprawdzimy czy umiesz liczyć",
            MaxPoints = 1,
            Questions = new List<Question>()
            {
                question1,
            }
        };

        var exam4 = new Exam()
        {
            Title = "Religious exam",
            Time = 2,
            CreationDate = DateTime.Now,
            Description = "Egzamin z religi",
            MaxPoints = 2,
            Questions = new List<Question>()
            {
                question2
            }
        };

        var exam5 = new Exam()
        {
            Title = "Polish history exam",
            Time = 20,
            CreationDate = DateTime.Now,
            Description = "Egzamin sprawdzajacy wiedze na temat history krolow polski",
            MaxPoints = 3,
            Questions = new List<Question>()
            {
                question3
            }
        };

        context.Add(exam1);
        context.Add(exam2);
        context.Add(exam3);
        context.Add(exam4);
        context.Add(exam5);

        var teacher = new User()
        {
            Index = "t22m",
            FirstName = "Michał",
            LastName = "Ser",
            Email = "Ser.jem@alboniejem.pl",
            PhoneNumber = "666777444",
            Description = "brak",
            Login = "michal.ser",
            Password = "nieJemSera",
        };

        var student = new User()
        {
            Index = "s18789",
            FirstName = "Antoni",
            LastName = "Materac",
            Email = "materac@wygodny.pl",
            PhoneNumber = "502095702",
            Login = "admin",
            Password = "admin",
            Description = "opis.",
        };

        context.Add(teacher);
        context.Add(student);
        context.SaveChanges();*/
    }

}
