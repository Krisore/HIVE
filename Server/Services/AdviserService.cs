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

        public async Task<Adviser> AddAdviserAsync(Adviser adviser)
        {
            var newAdviser = new Adviser
            {
                Name = adviser.Name,
                Email = adviser.Email,
            };
            _context.Advisers.Add(newAdviser);
            await _context.SaveChangesAsync();
            return newAdviser;
        }

        public async  Task EditAdviserAsync(int id, Adviser adviser)
        {
            var result = await _context.Advisers.Include(a => a.Documents).FirstOrDefaultAsync(a => a.Id == id);
            if (result != null)
            {
                result.Name = adviser.Name;
                result.Email = adviser.Email;
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAdviserAsync(int adviserId)
        {
            var result = await _context.Advisers.FirstOrDefaultAsync(a => a.Id == adviserId);
            if (result != null) _context.Advisers.Remove(result);
            await _context.SaveChangesAsync();
           
        }
    }
}
