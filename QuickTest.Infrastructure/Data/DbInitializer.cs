using Microsoft.AspNetCore.Identity;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Services;

namespace QuickTest.Infrastructure.Data;
public class DbInitializer
{
    public static async Task FillDatabaseRandomData(DataContext context, UserManager<User> userManager)
    {
        var userRole = context.UserRoles.FirstOrDefault(x => x.Name == "ADMINISTRATOR");
        var checkAdmins = context.Admins.Where(x => x.LastName == "Serdak" || x.LastName == "Okrzeja").ToList();
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
