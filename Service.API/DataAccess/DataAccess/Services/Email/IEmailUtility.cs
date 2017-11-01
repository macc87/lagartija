using System.Net.Mail;
using System.Threading.Tasks;
using Fantasy.API.Utilities.ServicesHandler.Core;

namespace Fantasy.API.DataAccess.Services.Email
{
    public interface IEmailUtility
    {
        Task<ServiceResult> SendEmailAsync(MailMessage message);
    }
}
