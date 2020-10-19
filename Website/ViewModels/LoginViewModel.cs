using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.Requests.Auth;
using Shared.Responses.Auth;

namespace Website.ViewModels
{
    public class LoginViewModel : Login
    {
        private readonly HttpClient _http;

        public LoginViewModel(HttpClient http)
        {
            _http = http;
        }

        public async Task<AuthenticationResponse> OnLoginSubmit()
        {
            HttpResponseMessage responseMessage = await _http.PostAsJsonAsync(
                "/Auth/Login",
                new
                {
                    Identifier,
                    Password
                });

            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            AuthenticationResponse authenticationResponse = await responseMessage.Content.ReadFromJsonAsync<AuthenticationResponse>();
            return authenticationResponse;
        }
    }
}
