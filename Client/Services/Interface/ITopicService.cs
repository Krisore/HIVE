using HIVE.Shared.Model;

namespace HIVE.Client.Services.Interface
{
    public interface ITopicService
    {
        Task<List<Topic>> GetTopics();
    }
}
