using HIVE.Shared.Model;

namespace HIVE.Server.Services.Interface
{
    public interface IAdviserService
    {
        Task<IEnumerable<Adviser>> GetAdviserBySearchAsync(string value);
        Task<Adviser> GetAdviserAsync(int id);
        Task<List<Adviser>> GetAllAdviserAsync();
    }
}
