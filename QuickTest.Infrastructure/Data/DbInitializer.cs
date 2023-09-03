using Microsoft.AspNetCore.Identity;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Services;

namespace QuickTest.Infrastructure.Data;
public class DbInitializer
{
    public static async Task FillDatabaseRandomData(DataContext context, UserManager<User> userManager)
    {
        var userRole = context.UserRoles.FirstOrDefault(x => x.Name == "ADMINISTRATOR");
        var studentRole = context.UserRoles.FirstOrDefault(x => x.Name == "STUDENT");
        var teacherRole = context.UserRoles.FirstOrDefault(x => x.Name == "TEACHER");
        var checkAdmins = context.Admins.Where(x => x.LastName == "Serdak" || x.LastName == "Okrzeja").ToList();
        var groups = context.Groups.ToList();
        var school = context.Schools.Any();
        School newSchool = new School();   
        if (!school)
        {
            newSchool = new School
            {
                Name = "Random High School",
                Address = new Address
                {
                    PostalCode = "12345",
                    Street = "Random Street",
                    BuildingNumber = "42",
                    PropertyNumber = "43",
                    City = "Random City",
                    State = "Random State",
                    Country = "Random Country",
                    SchoolId = 1
                }
                
            };
            context.Add(newSchool);
            context.SaveChanges();
        }
        else
        {
            newSchool = context.Schools.FirstOrDefault();
        }
        if (checkAdmins.Count != 2)
        {
            var adminMs = new Admin()
            {
                FirstName = "Michal",
                LastName = "Serdak",
                Email = "testsm@gg.pl",
                PhoneNumber = "666777444",
                UserName = "testsm@gg.pl".Split('@')[0],
                UserRole = userRole,
                AdministeredSchool = newSchool
            };
            var adminRo = new Admin()
            {
                FirstName = "Rafal",
                LastName = "Okrzeja",
                Email = "testro@gg.pl",
                PhoneNumber = "111222333",
                UserName = "testro@gg.pl".Split('@')[0],
                UserRole = userRole,
                AdministeredSchool = newSchool
            };

            
           

            var resultMs = await userManager.CreateAsync(adminMs, "Admin123!");
            var resultRo = await userManager.CreateAsync(adminRo, "Admin123!");

            if (resultMs.Succeeded && resultRo.Succeeded)
            {
                context.SaveChanges();
                context.Add(adminMs);
                context.Add(adminRo);
            }

        }
        var students = context.Students.Any();
        if(!students)
        {
            var group1 = groups.FirstOrDefault(x=> x.Id == 1);
            var group2 = groups.FirstOrDefault(x => x.Id == 2);
            var stud_1 = new Student()
            {
                Email = "john.doe@example.com",
                UserName = "john.doe@example.com".Split('@')[0],
                FirstName = "John",
                LastName = "Doe",
                UserRole = studentRole,
                Group = group1
            };
            var stud_2 = new Student()
            {
                Email = "jane.smith@example.com",
                UserName = "jane.smith@example.com".Split('@')[0],
                FirstName = "Jane",
                LastName = "Smith",
                UserRole = studentRole,
                Group = group1
            };

            var stud_3 = new Student()
            {
                Email = "michael.jordan@example.com",
                UserName = "michael.jordan@example.com".Split('@')[0],
                FirstName = "Michael",
                LastName = "Jordan",
                UserRole = studentRole,
                Group = group1
            };

            var stud_4 = new Student()
            {
                Email = "lucy.heartfilia@example.com",
                FirstName = "Lucy",
                LastName ="Heartfilia",
                UserName = "lucy.heartfilia@example.com".Split('@')[0],
                UserRole = studentRole,
                Group = group2
            };

            var stud_5 = new Student()
            {
                Email = "tony.stark@example.comtony.stark@example.com",
                UserName = "tony.stark@example.com".Split('@')[0],
                FirstName = "Tony",
                LastName = "Stark",
                UserRole = studentRole,
                Group = group2
            };
            var result_1 = await userManager.CreateAsync(stud_1, "As12345!");
            var result_2 = await userManager.CreateAsync(stud_2, "As12345!");
            var result_3 = await userManager.CreateAsync(stud_3, "As12345!");
            var result_4 = await userManager.CreateAsync(stud_4, "As12345!");
            var result_5 = await userManager.CreateAsync(stud_5, "As12345!");
            context.AddRange(new List<Student>(){ stud_1,stud_2, stud_3, stud_4, stud_5});
        }
        var teachers = context.Teachers.Any();
        if(!teachers) 
        {
            var teach_1 = new Teacher()
            {
                Email = "bruce.wayne@example.com",
                UserName = "bruce.wayne@example.com".Split('@')[0],
                FirstName = "Bruce",
                LastName = "Wayne",
                UserRole = teacherRole
            };
            var teach_2 = new Teacher()
            {
                Email = "clark.kent@example.com",
                FirstName = "Clark",
                LastName ="Kent",
                UserName = "clark.kent@example.com".Split('@')[0],
                UserRole = teacherRole
            };

            var teach_3 = new Teacher()
            {
                Email = "diana.prince@example.com",
                FirstName= "Diana",
                LastName ="Prince",
                UserName = "diana.prince@example.com".Split('@')[0],
                UserRole = teacherRole
            };
            var result_1 = await userManager.CreateAsync(teach_1, "As12345!");
            var result_2 = await userManager.CreateAsync(teach_2, "As12345!");
            var result_3 = await userManager.CreateAsync(teach_3, "As12345!");
            context.AddRange(new List<Teacher>(){ teach_1,teach_2,teach_3});
        }


        ///testy emaila
        //Random random = new Random();

        //char randomUppercaseLetter = (char)random.Next(65, 91);

        //char[] specialCharacters = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '_', '{', '}', '[', ']', '|', '\\', ':', ';', '<', '>', '?', '/', '.' };
        //char randomSpecialCharacter = specialCharacters[random.Next(0, specialCharacters.Length)];

        //string[] words = { "apple", "banana", "cherry", "date", "elderberry", "fig", "grape", "honeydew" };
        //string randomWord = words[random.Next(0, words.Length)];

        //int randomNumber = random.Next(0, 10);

        //string password = $"{randomUppercaseLetter}{randomSpecialCharacter}{randomWord}{randomNumber}";
        //var service = new EmailService();

        //await service.SendEmailAsync("serghio666@gmail.com", "Your Account Password", $"Your generated password is: {password}. Please change it upon first login.");


    }

}
