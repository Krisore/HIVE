using HIVE.Shared.Model;

namespace HIVE.Server.Services.Interface
{
    public interface IActivityLog
    {
        Task<Activity> InsertLog(Activity activity);
        Task<List<Activity>> GetLogs();
        Task ClearLogs(int id);
    }
}
