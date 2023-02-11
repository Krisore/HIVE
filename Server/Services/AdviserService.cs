using HIVE.Server.Data;
using HIVE.Server.Services.Interface;
using HIVE.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HIVE.Server.Services
{
    public class AdviserService : IAdviserService
    {
        private readonly DataContext _context;

        public AdviserService(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Adviser>> GetAdviserBySearchAsync(string value)
        {
            var result = await _context.Advisers.Where(a => a.Name.ToLower().Contains(value.ToLower())).ToListAsync();
            return result;
        }
        public async Task<Adviser> GetAdviserAsync(int id)
        {
            var result = await _context.Advisers.Where(a => a.Id == id).FirstOrDefaultAsync();
            return result;
        }
      
        public async Task<List<Adviser>> GetAllAdviserAsync()
        {
            var advisers = await _context.Advisers.ToListAsync();
            return advisers;
        }
    }
}
