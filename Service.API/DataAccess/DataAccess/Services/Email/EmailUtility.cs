using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using Utilities.ServicesHandler;
using Utilities.ServicesHandler.Core;

namespace DataAccess.Services.Email
{
    public class EmailUtility : BaseService, IEmailUtility
    {
        private readonly string _environmentVariable = ConfigurationManager.AppSettings.Get("Environment");

        public async Task<ServiceResult> SendEmailAsync(MailMessage message)
        {
            try
            {
                ServiceResult result;
                switch (_environmentVariable.ToLower())
                {
                    case "local":
                        using (SmtpClient client = new SmtpClient("mysmtphost"))
                        {
                            try
                            {
                                var localAddress = "C:" + Path.DirectorySeparatorChar + "PortalEmailFolder";
                                if (!Directory.Exists(localAddress))
                                {
                                    Directory.CreateDirectory(localAddress);
                                }
                                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                                client.PickupDirectoryLocation = localAddress;
                                client.Send(message);
                                result = await ServiceOkAsync();
                            }
                            catch (Exception exception)
                            {
                                result = ExceptionHandler(exception);
                            }
                            finally
                            {
                                message.Dispose(); //force message dispose here
                            }
                            return result;
                        }
                    case "test":
                    case "modl":
                    case "train":
                    case "prod":
                        using (SmtpClient client = new SmtpClient("mysmtphost"))
                        {
                            try
                            {
                                client.Send(message);
                                result = await ServiceOkAsync();

                            }
                            catch (Exception exception)
                            {
                                result = ExceptionHandler(exception);
                            }
                            finally
                            {
                                message.Dispose(); //force message dispose here
                            }
                            return result;
                        }
                }
                return await ServiceOkAsync();
            }
            catch (Exception exception)
            {
                return ExceptionHandler(exception);
            }
        }
    }
}

