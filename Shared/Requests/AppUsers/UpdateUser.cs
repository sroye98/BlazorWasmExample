using System;
namespace Shared.Requests.AppUsers
{
    public class UpdateUser
    {
        public UpdateUser()
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }
    }
}
