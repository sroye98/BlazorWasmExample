namespace BusinessLogic.Models
{
    public class TokenResponse
    {
        public TokenResponse()
        {
        }

        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
