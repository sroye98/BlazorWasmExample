using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
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
