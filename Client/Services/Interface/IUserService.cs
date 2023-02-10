using HIVE.Shared.Model;
using HIVE.Shared.Request;

namespace HIVE.Client.Services.Interface
{
    public interface IUserService
    {
        UserSession Session { get; set; }
        List<string?> Emails { get; set; }
        UserRegisterRequest UserRequest { get; set; }
        List<UserRegisterRequest> Users { get; set; }
        User User { get; set; }
        Task<User> MyAccount(string email);
        Task<HttpResponseMessage> UpdateUserAccount(int id, UserRegisterRequest user);
        Task<HttpResponseMessage> Login(LoginRequest request);
        Task<HttpResponseMessage> RegisterAsync(UserRegisterRequest request);
        Task<HttpResponseMessage> VerifyAsync(string token);
        Task<HttpResponseMessage> ForgotPassword(string email);
        Task<HttpResponseMessage> ResetPassword(ResetPasswordRequest request);
        Task<List<string?>> UserEmails();
    }
}
