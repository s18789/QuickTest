using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Users.CreateUser
{
    public class UserUtilities
    {
        public static string GenerateRandomPassword()
        {
            Random random = new Random();

            char randomUppercaseLetter = (char)random.Next(65, 91);

            char[] specialCharacters = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '_', '{', '}', '[', ']', '|', '\\', ':', ';', '<', '>', '?', '/', '.' };
            char randomSpecialCharacter = specialCharacters[random.Next(0, specialCharacters.Length)];

            string[] words = { "apple", "banana", "cherry", "date", "elderberry", "fig", "grape", "honeydew" };
            string randomWord = words[random.Next(0, words.Length)];

            int randomNumber = random.Next(0, 10);

            string password = $"{randomUppercaseLetter}{randomSpecialCharacter}{randomWord}{randomNumber}";

            return password;
        }
    }
}
