using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Settings;
using DataLogic.DataAccess;
using DataLogic.Entities;
using DataLogic.Interfaces;
using DataLogic.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Server.Filters;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                if (Environment.IsDevelopment())
                {
                    options.UseInMemoryDatabase("testdb");
                }
                else
                {
                    options.UseInternalServiceProvider(sp);
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                }
            });

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options => { });

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidatorActionFilter));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            SecuritySettings securitySettings = new SecuritySettings();
            Configuration.Bind("SecuritySettings", securitySettings);
            services.AddSingleton(securitySettings);

            var key = Encoding.ASCII.GetBytes(securitySettings.Key);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WEBAPI", Version = "v1" });
                options.ResolveConflictingActions(a => a.First());
                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme()
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter into field the work 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                options.OperationFilter<Filters.SecurityRequirementsOperationFilter>();
            });

            services.AddHttpContextAccessor();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ICommunicationService, CommunicationService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IServiceProvider svc)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WEBAPI V1");
                options.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            CreateRoleAsync(
                svc,
                Shared.Constants.Roles.Administrator).GetAwaiter().GetResult();
            CreateUserAsync(
                svc,
                "Saachi",
                "Roye",
                "saachi.roye@gmail.com",
                "sroye98",
                "7134802586",
                "Saachir1!",
                Shared.Constants.Roles.Administrator).GetAwaiter().GetResult();
        }

        private async Task<bool> CreateRoleAsync(
            IServiceProvider serviceProvider,
            string role)
        {
            RoleManager<AppRole> RoleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

            var existingRole = await RoleManager.RoleExistsAsync(role);
            if (existingRole)
            {
                return true;
            }

            IdentityResult roleResult = await RoleManager.CreateAsync(
                new AppRole
                {
                    Name = role
                });

            return true;
        }

        private async Task<bool> CreateUserAsync(
            IServiceProvider serviceProvider,
            string firstName,
            string lastName,
            string email,
            string username,
            string phone,
            string password,
            string role)
        {
            UserManager<AppUser> UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            var existingUser = await UserManager.FindByEmailAsync(email) ??
                await UserManager.FindByNameAsync(username);

            if (existingUser != null)
            {
                return true;
            }

            AppUser newUser = new AppUser
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = username,
                PhoneNumber = phone
            };

            IdentityResult userResult = await UserManager.CreateAsync(
                newUser,
                password);

            if (!userResult.Succeeded)
            {
                return false;
            }

            userResult = await UserManager.AddToRoleAsync(
                newUser,
                role);

            return userResult.Succeeded;
        }
    }
}
