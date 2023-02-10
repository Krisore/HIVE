namespace HIVE.Client.Services.Interface
{
    public interface IActivityLog
    {
        Task<HttpResponseMessage> WriteLog(string actor, string detail, string type, string role);
    }
}
