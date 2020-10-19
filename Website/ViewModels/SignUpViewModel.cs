using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.Requests.Common;
using Shared.Requests.Employee;

namespace Website.ViewModels
{
    public class SignUpViewModel : CreateEmployee
    {
        private readonly HttpClient _http;

        public SignUpViewModel(HttpClient http)
        {
            _http = http;

            Address = new Address
            {
                Line1 = "5710 Cornish St.",
                Line2 = "Unit #1",
                City = "Houston",
                State = "TX",
                ZipCode = "77007"
            };
            EmailAddress = "saachi.roye@gmail.com";
            FirstName = "Saachi";
            LastName = "Roye";
            Password = "Saachir1!";
            PhoneNumber = "7134802586";
            UserName = "sroye98";
        }

        public async Task<string> OnSignUpSubmitAsync()
        {
            string response = string.Empty;
            HttpResponseMessage responseMessage = await _http.PostAsJsonAsync(
                "/Employee",
                new
                {
                    FirstName,
                    LastName,
                    EmailAddress,
                    UserName,
                    Password,
                    PhoneNumber,
                    Address
                });

            if (!responseMessage.IsSuccessStatusCode)
            {
                response = await responseMessage.Content.ReadAsStringAsync();
            }

            return response;
        }
    }
}
