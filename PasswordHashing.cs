
namespace GymProgress.Mobile
{
    public class PasswordHashing
    {
        public static string HashPassword(string startPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(startPassword);
        }

        public static bool ConfirmPassword(string startPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(startPassword, hashedPassword);
        }
    }
}
