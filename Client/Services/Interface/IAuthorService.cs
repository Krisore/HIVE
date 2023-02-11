using HIVE.Shared.Model;

namespace HIVE.Client.Services.Interface
{
    public interface IAuthorService
    {
        List<Author> Authors { get; set; }
         Author Author { get; set; }
        Task AddAuthor(List<CreateAuthor> authors);
        Task<HttpResponseMessage> DeleteAuthor(int id);
        Task<List<Author>> GetAllAuthorAsync();
    }
}
