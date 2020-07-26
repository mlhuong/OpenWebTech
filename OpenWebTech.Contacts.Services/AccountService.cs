using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OpenWebTech.Contacts.Services.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OpenWebTech.Contacts.Services
{
    public class AccountService
    {
        private static string secretKey = "OpEnwEbTecH@cOnTaCT__secret";
        public static SymmetricSecurityKey SigningKey { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<(string token, string errors)> Register(RegisterDto register)
        {
            var user = new IdentityUser { UserName = register.Username, Email = register.Email };
            var result = await userManager.CreateAsync(user, register.Password).ConfigureAwait(false);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);
                return (await this.GenerateToken(user), null);
            }
            var msg = string.Join("\n", result.Errors.Select(x => $"{x.Code} - {x.Description}"));
            return (null, msg);
        }

        public async Task<string> GetToken(string username, string password)
        {
            SignInResult result = await this.signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                IdentityUser user = await this.userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return null;
                }

                return await this.GenerateToken(user);
            }
            return null;
        }

        private async Task<string> GenerateToken(IdentityUser user)
        {
            var now = DateTime.UtcNow;
            var userClaims = await this.userManager.GetClaimsAsync(user);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64),
            };

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: "OpenWebTech_Issuer",
                audience: "OpenWebTech_Audience",
                claims: claims.Concat(userClaims),
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(30)),
                signingCredentials: new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
