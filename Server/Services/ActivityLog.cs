using HIVE.Server.Data;
using HIVE.Server.Services.Interface;
using HIVE.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace HIVE.Server.Services
{
    public class ActivityLog : IActivityLog
    {
        private readonly DataContext _context;

        public ActivityLog(DataContext context)
        {
            _context = context;
        }
        public async Task<Activity> InsertLog(Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
            return activity;
        }

        public async Task<List<Activity>> GetLogs()
        {
            return await _context.Activities.ToListAsync();
        }
        public async Task ClearLogs(int id)
        {
            var response = await _context.Activities.FirstOrDefaultAsync(a => a.Id == id);
            if (response != null) _context.Activities.Remove(response);
            await _context.SaveChangesAsync();
        }
    }
}
