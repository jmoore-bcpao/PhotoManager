using System.Threading.Tasks;

namespace BCPAO.PhotoManager.Services
{
   public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
