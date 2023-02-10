using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HIVE.Shared.Request
{
    public class UserRegistration
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Role { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        public DateTime DateRegistered { get; set; } = DateTime.Now;
        [JsonIgnore]
        public bool ShowDocument = false;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
    }
}
