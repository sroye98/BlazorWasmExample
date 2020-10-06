using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DataLogic.Entities;
using DataLogic.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using BusinessLogic.Settings;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly SecuritySettings _securitySettings;

        public TokenService(
            UserManager<AppUser> userManager,
            AppDbContext context,
            SecuritySettings securitySettings)
        {
            _userManager = userManager;
            _context = context;
            _securitySettings = securitySettings;
        }

        public async Task<TokenResponse> GenerateTokens(
            AppUser user,
            string ipAddress)
        {
            try
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                IList<string> roles = await _userManager.GetRolesAsync(user);
                if (roles == null)
                {
                    roles = new List<string>();
                }

                foreach (string role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_securitySettings.Key));
                SigningCredentials credentials = new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256Signature);
                JwtHeader header = new JwtHeader(credentials);
                JwtSecurityToken jwtToken = new JwtSecurityToken(
                    _securitySettings.Issuer,
                    _securitySettings.Issuer,
                    claims,
                    expires: DateTime.Now.AddMinutes(_securitySettings.ExpirationMinutes),
                    signingCredentials: credentials);
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                string token = handler.WriteToken(jwtToken);

                RefreshToken refreshToken = generateRefreshToken(ipAddress);
                refreshToken.User = user;

                _context.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync();

                return new TokenResponse
                {
                    JwtToken = token,
                    RefreshToken = refreshToken.Token
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }
    }
}
