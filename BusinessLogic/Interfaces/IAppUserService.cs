using System;
using System.Threading.Tasks;
using DataLogic.Entities;

namespace BusinessLogic.Interfaces
{
    public interface IAppUserService
    {
        Task<AppUser> AddAsync(
            AppUser entity,
            string password);

        Task<AppUser> UpdateAsync(
            Guid id,
            AppUser entity,
            string password);
    }
}
