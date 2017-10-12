using System.Net.Mail;
using System.Threading.Tasks;
using Utilities.ServicesHandler.Core;

namespace DataAccess.Services.Email
{
    public interface IEmailUtility
    {
        Task<ServiceResult> SendEmailAsync(MailMessage message);
    }
}
