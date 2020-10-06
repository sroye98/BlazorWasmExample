using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataLogic.Entities;

namespace BusinessLogic.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponse> GenerateTokens(
            AppUser user,
            string ipAddress);
    }
}
