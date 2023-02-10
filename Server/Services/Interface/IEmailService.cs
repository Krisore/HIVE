using HIVE.Shared.Request;

namespace HIVE.Server.Services.Interface
{
    public interface IEmailService
    {
        void SendEmail(SendMail mail);
    }
}
