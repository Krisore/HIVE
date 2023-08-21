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
        List<User> UsersCount { get; set; }
        Task<User> MyAccount(string email);
        Task<List<User>> GetUserCount();
        Task<HttpResponseMessage> UpdateUserAccount(int id, UserRegisterRequest user);
        Task<HttpResponseMessage> UpdateUserAccount(int id, User request);
        Task<HttpResponseMessage> Login(LoginRequest request);
        Task<HttpResponseMessage> RegisterAsync(UserRegisterRequest request);
        Task<HttpResponseMessage> RegisterAdminAsync(User request);
        Task<HttpResponseMessage> UpdateAdminAccount(int id, User request);
        Task<HttpResponseMessage> VerifyAsync(string token);
        Task<HttpResponseMessage> ForgotPassword(string email);
        Task<HttpResponseMessage> ResetPassword(ResetPasswordRequest request);
        Task<List<string?>> UserEmails();
        Task<HttpResponseMessage> DeleteUserAccount(int id);
        Task<bool> CheckEmail(string email);

    }
}
