using System;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataLogic.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests.Employee;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeSvc;

        public EmployeeController(IEmployeeService employeeSvc)
        {
            _employeeSvc = employeeSvc;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployee payload)
        {
            try
            {
                Employee employee = new Employee
                {
                    AddressLine1 = payload.Address.Line1,
                    AddressLine2 = payload.Address.Line2,
                    AppUser = new AppUser
                    {
                        Email = payload.EmailAddress,
                        FirstName = payload.FirstName,
                        LastName = payload.LastName,
                        PhoneNumber = payload.PhoneNumber,
                        UserName = payload.UserName
                    },
                    City = payload.Address.City,
                    State = payload.Address.State,
                    Zip = payload.Address.ZipCode
                };

                Employee newEmployee = await _employeeSvc.AddAsync(
                    employee,
                    payload.Password);

                return Ok(newEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
