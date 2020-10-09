using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Auth
{
    public class RevokeToken
    {
        public RevokeToken()
        {
        }

        [Required]
        public string Token { get; set; }
    }
}
