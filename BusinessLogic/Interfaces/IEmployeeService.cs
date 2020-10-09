using System;
using System.Threading.Tasks;
using DataLogic.Entities;

namespace BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> AddAsync(
            Employee entity,
            string password);
    }
}
