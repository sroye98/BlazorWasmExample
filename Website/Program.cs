using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Website.ViewModels;

namespace Website
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services
                .AddScoped(sp => new HttpClient
                {
                    //BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                    BaseAddress = new Uri("https://localhost:51852")
                }).AddBlazoredLocalStorage()
                .AddAuthorizationCore();

            builder.Services.AddSingleton<StateContainer>();
            builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>();

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}
