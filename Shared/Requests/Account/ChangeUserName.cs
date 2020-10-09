using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Account
{
    public class ChangeUserName
    {
        public ChangeUserName()
        {
        }

        [Required, DataType(DataType.Text)]
        public string Username { get; set; }
    }
}
