using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace OxigenIISitesBLServices
{
  public class ContactEmailSender
  {
    private string _email;
    private string _subject;
    private string _name;
    private string _message;

    public ContactEmailSender(string email, string subject, string name, string message)
    {
      _email = email;
      _subject = subject;
      _name = name;
      _message = message;
    }

    public void Send()
    {
      MailMessage mailMessage = new MailMessage();
      mailMessage.From = new MailAddress(_email);
      mailMessage.To.Add(System.Configuration.ConfigurationSettings.AppSettings["ContactEmailAddress"]);

      mailMessage.Subject = _subject;
      mailMessage.Body = _message;

      mailMessage.IsBodyHtml = false;

      SmtpClient smtpClient = new SmtpClient(System.Configuration.ConfigurationSettings.AppSettings["SMTPServer"]);
      smtpClient.Send(mailMessage);
    }
  }
}
