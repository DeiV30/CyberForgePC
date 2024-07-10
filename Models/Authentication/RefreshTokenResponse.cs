namespace  cyberforgepc.Models.Authentication
{
    public class RefreshTokenResponse
    {
        public string NewAccessToken { get; set; }
        public string NewRefreshToken { get; set; }
    }
}
