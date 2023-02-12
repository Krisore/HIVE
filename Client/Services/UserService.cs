using HIVE.Client.Authentication;
using HIVE.Shared.Model;
using HIVE.Shared.Request;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using HIVE.Client.Services.Interface;

namespace HIVE.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;
        private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;
        private readonly NavigationManager _navigation;

        public UserService(HttpClient client, CustomAuthenticationStateProvider customAuthenticationStateProvider, NavigationManager navigation)
        {
            _client = client;
            _customAuthenticationStateProvider = customAuthenticationStateProvider;
            _navigation = navigation;
        }

        public UserSession Session { get; set; } = new UserSession();
        public List<string?> Emails { get; set; } = new List<string?>();
        public User User { get; set; } = new User();
        public UserRegisterRequest UserRequest { get; set; } = new UserRegisterRequest();
        public List<UserRegisterRequest> Users { get; set; } = new List<UserRegisterRequest>();
        public List<User> UsersCount { get; set; } = new List<User>();
        public async Task<User> MyAccount(string email)
        {
            var accessToken = await _customAuthenticationStateProvider.GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.GetFromJsonAsync<User>($"api/User/Account/{email}");
            return response;
        }

        public async Task<List<User>> GetUserCount()
        {
            var accessToken = await _customAuthenticationStateProvider.GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.GetFromJsonAsync<List<User>>("api/User/count");
            if (response is not null)
            {
                UsersCount = response;
            }

            return UsersCount;
        }

        public async Task<HttpResponseMessage> UpdateUserAccount(int id, UserRegisterRequest user)
        {
            // Include the authorization header with the request
            var accessToken = await _customAuthenticationStateProvider.GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.PutAsJsonAsync($"api/User/update/{id}", user);
            return response;
        }
        public async Task<HttpResponseMessage> Login(LoginRequest request)
        {

            var response = await _client.PostAsJsonAsync("/api/User/login", request);
            return response;
        }

        public async Task<HttpResponseMessage> RegisterAsync(UserRegisterRequest request)
        {
            var response = await _client.PostAsJsonAsync("api/User/register", request);
            return response;
        }

        public async Task<HttpResponseMessage> RegisterAdminAsync(User request)
        {
            var response = await _client.PostAsJsonAsync("api/User/register/admin", request);
            return response;
        }
        public async Task<HttpResponseMessage> UpdateAdminAccount(int id, User request)
        {
            var response = await _client.PutAsJsonAsync($"api/User/update/admin/{id}", request);
            return response;
        }

        public async Task<HttpResponseMessage> VerifyAsync(string token)
        {
            var accessToken = await _customAuthenticationStateProvider.GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.PostAsJsonAsync($"api/User/verify/{token}", token);
            return response;
        }

        public async Task<HttpResponseMessage> ForgotPassword(string email)
        {
            var result = await _client.PostAsJsonAsync($"api/User/forgot-password/{email}", email);
            return result;
        }

        public async Task<HttpResponseMessage> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _client.PostAsJsonAsync($"api/User/reset-password", request);
            return result;
        }
        public async Task<List<string?>> UserEmails()
        {
            var response = await _client.GetFromJsonAsync<List<string>>("api/User/emails");
            if (response == null) return Emails;
            Emails = response;
            return response;
        }

        public async Task<HttpResponseMessage> DeleteUserAccount(int id)
        {
            var accessToken = await _customAuthenticationStateProvider.GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await _client.DeleteAsync($"api/User/delete/{id}");
            return result;
        }
    }
}
