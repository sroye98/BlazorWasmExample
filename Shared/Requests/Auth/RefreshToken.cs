using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Auth
{
    public class RefreshToken
    {
        public RefreshToken()
        {
        }

        [Required]
        public string Token { get; set; }
    }
}
