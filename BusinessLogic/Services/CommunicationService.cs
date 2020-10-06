using System;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;

namespace BusinessLogic.Services
{
    public class CommunicationService : ICommunicationService
    {
        public CommunicationService()
        {
        }

        public Task SendEmail(
            string toAddress,
            string fromAddress,
            string subject,
            string body)
        {
            //TODO: Choose Email Provider & Code for Email
            throw new NotImplementedException();
        }

        public Task SendSMS(
            string toPhoneNumber,
            string message)
        {
            //TODO: Choose SMS Provider & Code for SMS
            throw new NotImplementedException();
        }
    }
}
