using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HIVE.Server.Services.Interface;
using HIVE.Shared.Request;

namespace HIVE.Server.Authentication
{
    public class JwtAuthenticationManager
    {
        private readonly IUserService _userService;
        public const string JwtSecurityKey = "yPkCqn4kSWLtaJwXvN2jGzpQRyT23gdXkt7FeBJP";
        private const int JwtTokenValidityTicks = 20;

        public JwtAuthenticationManager(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<UserSession?> GenerateJwtToken(string? email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            //TODO: Validating the user credentials || DONE ✔️
            var userAccount = await _userService.GetUserAccountByEmail(email);
            if (!VerifyPasswordHash(password, userAccount.PasswordHash, userAccount.PasswordSalt) || userAccount.VerifiedAt == null)
            {
                return null;
            }
            //TODO: Generating a JWT Token || DONE ✔️
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JwtTokenValidityTicks);
            var tokenKey = Encoding.ASCII.GetBytes(JwtSecurityKey);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userAccount.Email),
                new Claim(ClaimTypes.Role, userAccount.Role!)
            });
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            //TODO: Returning the user session Object || DONE ✔️
            var userSession = new UserSession()
            {
                Email = userAccount.Email,
                Role = userAccount.Role,
                Token = token,
                ExpireIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };
            return userSession;

        }
        public async Task<UserSession> GenerateJwtTokenByEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }
            //TODO: Validating the user credentials || DONE ✔️
            var userAccount = await _userService.GetUserAccountByEmail(email);
            //TODO: Generating a JWT Token || DONE ✔️
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JwtTokenValidityTicks);
            var tokenKey = Encoding.ASCII.GetBytes(JwtSecurityKey);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userAccount.Email),
                new Claim(ClaimTypes.Role, userAccount.Role!)
            });
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            //TODO: Returning the user session Object || DONE ✔️
            var userSession = new UserSession()
            {
                Email = userAccount.Email,
                Role = userAccount.Role,
                Token = token,
                ExpireIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };
            return userSession;

        }
        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
