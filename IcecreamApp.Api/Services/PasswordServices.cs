using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace IcecreamApp.Api.Services
{
    public class PasswordServices
    {
        private const int SaltSize = 10;
        public (string salt, string hashedPassword) GenerateSaltAndHash(string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                throw new ArgumentNullException(nameof(plainPassword));
            var buffer = RandomNumberGenerator.GetBytes(8);
            var salt = Convert.ToBase64String(buffer);
            var bytes = Encoding.UTF8.GetBytes(plainPassword + salt);
            var hash = SHA256.HashData(bytes);
            var hashedPassword = Convert.ToBase64String(hash);
            return (salt,hashedPassword);
        }
        public bool Compare(string plainPassword, string hash, string salt)
        {

        }
    }
}