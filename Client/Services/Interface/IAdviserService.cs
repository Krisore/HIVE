using HIVE.Shared.Model;

namespace HIVE.Client.Services.Interface
{
    public interface IAdviserService
    {
        Adviser Adviser { get; set; }
        List<Adviser> Advisers { get; set; }
        Task<Adviser> GetAdviserAsync(int id);
        Task<IEnumerable<Adviser>> GetAdvisersBySearchAsync(string search);
        Task<List<Adviser>> GetAdvisersAsync();
        Task<HttpResponseMessage> AddAdviserAsync(Adviser adviser);
        Task<HttpResponseMessage> DeleteAdviserAsync(int adviserId);
        Task<HttpResponseMessage> UpdateAdviserAsync(Adviser adviser);

    }
}
