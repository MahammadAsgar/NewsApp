namespace NewsMedia.Infrastructure.Services.Mail
{
    public interface IMailService
    {
        Task SendMessage(string to, string message);
    }
}
