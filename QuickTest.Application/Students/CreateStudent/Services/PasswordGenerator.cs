namespace QuickTest.Application.Students.CreateStudent.Services;

public class PasswordGenerator
{
    public int PasswordLength { get; set; }

    public PasswordGenerator(int passwordLength)
    {
        PasswordLength = passwordLength;
    }

    public string GenerateRandomPassword()
    {
        var random = new Random();

        var password = new List<char>();

        password.Add((char)random.Next(35, 48));
        password.Add((char)random.Next(48, 57));
        password.Add((char)random.Next(65, 90));
        password.Add((char)random.Next(97, 122));

        for (int i = 0; i < this.PasswordLength - 4; i++)
        {
            password.Add((char)random.Next(35, 125));
        }

        var pwrd = new string(password.OrderBy(x => random.Next()).ToArray());
        Console.WriteLine($" =============== PASSWORD =================== {pwrd}");
        return pwrd;
    }
}
