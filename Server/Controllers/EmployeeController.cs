using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataLogic.Entities;
using DataLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests.Common;
using Shared.Requests.Employee;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeSvc;
        private readonly IGenericRepository<Employee> _employeeRepo;

        public EmployeeController(
            IEmployeeService employeeSvc,
            IGenericRepository<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync([FromQuery] PagingOptions payload)
        {
            try
            {
                bool whereClause(Employee m) => m.AppUser.FullName.Contains(payload.SearchQuery);

                var count = await _employeeRepo.CountAsync(whereClause);

                var data = await _employeeRepo.GetAsync(
                    payload.SortColumn,
                    whereClause,
                    payload.DescendingOrder,
                    payload.PageSize,
                    payload.Projections,
                    payload.Skip);

                return Ok(
                    new
                    {
                        data,
                        count
                    });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
