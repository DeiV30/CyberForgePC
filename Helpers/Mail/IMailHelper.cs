namespace  cyberforgepc.Helpers.Mail
{
    using cyberforgepc.Helpers.Mail.Model;

    public interface IMailHelper
    {
        void Send(string template, string to, string subject, object model);
        void Send(string template, MailSetup header, object model);
    }
}
