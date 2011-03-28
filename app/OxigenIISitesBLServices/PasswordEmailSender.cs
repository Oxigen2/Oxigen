using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using OxigenIISitesBLServices;

namespace OxigenIIAdvertising.Services
{
  public class PasswordEmailSender
  {
    private string _email;
    private string _password;

    public PasswordEmailSender(string email, string password)
    {
      _email = email;
      _password = password;
    }

    public void SendPasswordReminderEmail()
    {
      MailMessage mailMessage = new MailMessage();
      mailMessage.From = new MailAddress(Resource.PasswordRemindEmailFrom);
      mailMessage.To.Add(_email);

      mailMessage.Subject = Resource.PasswordRemindEmailSubject;
      mailMessage.Body = String.Format(Resource.PasswordRemindEmailMessage, Environment.NewLine, _password);

      mailMessage.IsBodyHtml = false;

      SmtpClient smtpClient = new SmtpClient(System.Configuration.ConfigurationSettings.AppSettings["SMTPserver"]);
      smtpClient.Send(mailMessage);
    }
  }
}
