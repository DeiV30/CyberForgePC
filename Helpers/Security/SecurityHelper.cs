namespace cyberforgepc.Helpers.Security
{
    using cyberforgepc.Helpers.Security.Model;
    using System.Security.Cryptography;
    using System.Text;

    public class SecurityHelper : ISecurityHelper
    {
        const int IterationsCount = 100000;

        public SecurityData HashPassword(string password) => HashPassword(password, CreateSalt(), IterationsCount);

        public SecurityData HashPassword(string password, byte[] salt) => HashPassword(password, salt, IterationsCount);

        public string Sha256(string rawData) => ComputeSha256(rawData);

        public SecurityData HashPassword(string password, byte[] salt, int iterations)
        {
            const int ByteLength = 32; // 256-bit key

            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var hash = rfc2898DeriveBytes.GetBytes(ByteLength);

            return new SecurityData
            {
                PasswordHash = hash,
                PasswordSalt = salt
            };
        }

        static byte[] CreateSalt()
        {
            const int Strength = 16; // 128-bit salt

            var salt = new byte[Strength];
            RandomNumberGenerator.Fill(salt);
            return salt;
        }

        static string ComputeSha256(string rawData)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
