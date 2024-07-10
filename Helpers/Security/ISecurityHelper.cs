namespace  cyberforgepc.Helpers.Security
{
    using cyberforgepc.Helpers.Security.Model;

    public interface ISecurityHelper
    {
        SecurityData HashPassword(string password, byte[] salt);
        SecurityData HashPassword(string password, byte[] salt, int iterations);
        SecurityData HashPassword(string password);
        string Sha256(string rawData);
    }
}
