using System;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataLogic.Interfaces;
using DataLogic.Entities;

namespace BusinessLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IAuthService _authService;
        private readonly IGenericRepository<Employee> _employeeRepo;

        public EmployeeService(
            IAuthService authService,
            IGenericRepository<Employee> employeeRepo)
        {
            _authService = authService;
            _employeeRepo = employeeRepo;
        }

        public async Task<Employee> AddAsync(
            Employee entity,
            string password)
        {
            try
            {
                await _authService.RegisterAsync(
                    $"http://site.com/confirm?token=[[TOKEN]]&email={entity.AppUser.Email}",
                    "alerts@site.com",
                    "Confirm Your Email Address",
                    null,
                    "Please confirm your email address. <a href=\"[[CALLBACKURL]]\">[[CALLBACKURL]]</a>",
                    null,
                    "Please enter the following token [[TOKEN]] to validate your phone number",
                    null,
                    entity.AppUser.Email,
                    entity.AppUser.UserName,
                    password,
                    entity.AppUser.PhoneNumber,
                    string.Empty,
                    false,
                    false);

                Employee newEmployee = await _employeeRepo.AddAsync(entity);

                return newEmployee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
