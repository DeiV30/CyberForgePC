namespace  cyberforgepc.Helpers.Security.Model
{
    public class SecurityData
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
