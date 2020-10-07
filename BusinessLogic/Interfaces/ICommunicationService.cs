using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICommunicationService
    {
        Task SendEmail(
            string toAddress,
            string fromAddress,
            string subject,
            string body);

        Task SendSMS(
            string toPhoneNumber,
            string message);
    }
}
