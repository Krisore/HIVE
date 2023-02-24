using HIVE.Shared.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace HIVE.Shared.Model;
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = new byte[32];
    public byte[] PasswordSalt { get; set; } = new byte[32];
    public string VerificationToken { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime? DateRegistered { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string PasswordResetToken { get; set; } = string.Empty;
    public DateTime? ResetTokenExpires { get; set; }
    [JsonIgnore]
    public bool ShowDocuments;
    [JsonIgnore]
    public string Password = string.Empty;
    [JsonIgnore]
    public string ConfirmPassword = string.Empty;
}
