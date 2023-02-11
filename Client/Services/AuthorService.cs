using System.Net.Http.Json;
using HIVE.Client.Services.Interface;
using HIVE.Shared.Model;

namespace HIVE.Client.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly HttpClient _client;

        public AuthorService(HttpClient client)
        {
            _client = client;
        }
        public List<Author> Authors { get; set; } = new List<Author>();
        public Author Author { get; set; }

        public async Task AddAuthor(List<CreateAuthor> authors)
        {
            foreach (var author in authors)
            {
                 await _client.PostAsJsonAsync("api/Author", author);
            }
        }
        public async Task<HttpResponseMessage> DeleteAuthor(int id)
        {

          var result =  await _client.DeleteAsync($"api/Author/{id}");
          return result;

        }
        public async Task<List<Author>> GetAllAuthorAsync()
        {
            var response = await _client.GetFromJsonAsync<List<Author>>("api/Author");
            if (response is not null)
            {
                Authors = response;
            }
            return Authors;
        }
    }
}
