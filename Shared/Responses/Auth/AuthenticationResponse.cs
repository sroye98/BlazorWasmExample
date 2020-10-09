namespace Shared.Responses.Auth
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse()
        {
        }

        public string EmailAddress { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
