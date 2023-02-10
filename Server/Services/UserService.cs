﻿using HIVE.Shared.Model;
using HIVE.Shared.Request;
using System.Security.Cryptography;
using HIVE.Server.Data;
using HIVE.Server.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HIVE.Server.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IEmailService _emailService;
        public UserService(DataContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public async Task<User?> GetUserAccountByEmail(string? email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
            return user;
        }

        public async Task<string> RegisterUser(UserRegisterRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            try
            {
                if (_context.Users.Any(user => user.Email == request.Email))
                {
                    return string.Empty;
                }
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                if (request is { UserName: { }, Email: { } })
                {
                    var user = new User()
                    {
                        UserName = request.UserName,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        Role = request.Role,
                        DateRegistered = DateTime.Now,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        VerificationToken = CreateRandomToken()
                    };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    var sendVerification = new SendMail
                    {
                        To = user.Email,
                        Subject = "PUP ARCHIVE : Email Verification Code",
                        Body = $"<strong> Verification code: </strong> <code>{user.VerificationToken}</code>"
                    };
                    _emailService.SendEmail(sendVerification);
                }

                var result = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName || u.Email == request.Email);
                return result != null ? "User Successfully Created" : string.Empty;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<string?> UpdateUser(int id, UserRegisterRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (!user.Email.Equals(request.Email))
            {
                user.VerificationToken = CreateRandomToken();
                var sendVerification = new SendMail
                {
                    To = user.Email,
                    Subject = "PUP ARCHIVE : Email Verification Code",
                    Body = $"<strong> Verification code: </strong> <code>{user.VerificationToken}</code>"
                };
                _emailService.SendEmail(sendVerification);
            }
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.DateRegistered = request.DateRegistered;
            user.PasswordHash = request.PasswordHash;
            user.PasswordSalt = request.PasswordSalt;
            user.Role = request.Role;
            await _context.SaveChangesAsync();
            return user.Email;
        }

        public async Task<string> Verify(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken.Equals(token));
            if (user == null)
            {
                return string.Empty;
            }
            user.VerifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return "User Verified!";
        }

        public async Task<string> ForgotPasswordAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return string.Empty;
            }
            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();
            var sendVerification = new SendMail
            {
                To = user.Email,
                Subject = " PUP ARCHIVE : Reset password Code",
                Body = $"<strong> Reset code: </strong> <code>{user.PasswordResetToken}</code> \n <b> Expiration: {user.ResetTokenExpires.Value.ToLongDateString()} at {user.ResetTokenExpires.Value.ToLongTimeString()} </b>"
            };
            _emailService.SendEmail(sendVerification);
            return "You may now reset your password";
        }
        public async Task<string> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return string.Empty;
            }

            if (request.Password != null)
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            user.PasswordResetToken = string.Empty;
            user.ResetTokenExpires = null;
            await _context.SaveChangesAsync();
            return "PasswordSuccessfully reset";
        }

        public async Task<List<string>> UsersEmail()
        {
            var users = await _context.Users.ToListAsync();
            var emails = users.Select(user => user.Email).ToList();
            return emails.ToList();
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(24));
        }
    }
}