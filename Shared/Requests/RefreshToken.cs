using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
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
