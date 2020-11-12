using System;
using System.Threading.Tasks;
using DataLogic.Entities;
using DataLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.Requests.AppUsers;
using Shared.Requests.Common;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppUsersController : ControllerBase
    {
        private readonly IGenericRepository<AppUser> _userRepo;

        public AppUsersController(IGenericRepository<AppUser> userRepo)
        {
            _userRepo = userRepo;
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync([FromQuery] PagingOptions payload)
        {
            try
            {
                bool whereClause(AppUser m) => m.FullName.Contains(payload.SearchQuery) &&
                    !m.LockoutEnabled;

                var count = await _userRepo.CountAsync(whereClause);

                var data = await _userRepo.GetAsync(
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

        [Authorize(Roles = Roles.Administrator)]
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(
            Guid id,
            [FromBody] UpdateUser payload)
        {
            try
            {
                AppUser userToEdit = await _userRepo.GetAsync(id);

                if (userToEdit != null)
                {
                    return NotFound("User was not found.");
                }

                userToEdit.FirstName = payload.FirstName;
                userToEdit.LastName = payload.LastName;
                userToEdit.Email = payload.EmailAddress;
                userToEdit.PhoneNumber = payload.PhoneNumber;
                userToEdit.UserName = payload.UserName;

                AppUser updatedUser = await _userRepo.UpdateAsync(
                    id,
                    userToEdit);

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            try
            {
                AppUser userToEdit = await _userRepo.GetAsync(id);

                if (userToEdit != null)
                {
                    return NotFound("User was not found.");
                }

                userToEdit.LockoutEnabled = true;

                await _userRepo.UpdateAsync(id, userToEdit);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
