namespace BusinessLogic.Models
{
    public class LoginResponse
    {
        public LoginResponse()
        {
        }

        public string EmailAddress { get; set; }

        public string UserName { get; set; }

        public TokenResponse Tokens { get; set; }
    }
}
