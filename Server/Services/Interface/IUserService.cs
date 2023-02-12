using HIVE.Shared.Model;
using HIVE.Shared.Request;

namespace HIVE.Server.Services.Interface
{
    public interface IUserService
    {
        Task<User?> GetUserAccountByEmail(string? email);
        Task<string> RegisterUser(UserRegisterRequest request);
        Task RegisterAdmin(User request);
        Task UpdateAdminAccount(int id, User request);
        Task<string?> UpdateUser(int id, UserRegisterRequest request);
        Task<string> Verify(string token);
        Task<List<User>> GetUsers();
        Task<string> ForgotPasswordAsync(string email);
        Task<string> ResetPassword(ResetPasswordRequest request);
        Task<List<string>> UsersEmail();
        Task DeleteUser(int id);
    }
}
