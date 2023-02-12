using HIVE.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace HIVE.Server.Services.Interface
{
    public interface IAdviserService
    {
        Task<IEnumerable<Adviser>> GetAdviserBySearchAsync(string value);
        Task<Adviser> GetAdviserAsync(int id);
        Task<List<Adviser>> GetAllAdviserAsync();
        Task<Adviser> AddAdviserAsync(Adviser adviser);
        Task EditAdviserAsync(int id, Adviser adviser);
        Task DeleteAdviserAsync(int adviserId);
     
    }
}
