using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using OxigenIISitesBLServices;

namespace OxigenIIAdvertising.Services
{
  public class PasswordRequestSender
  {
    private string _senderEmailAddress;
    private string _channelCreatorEmailAddress;
    private string _channelCreatorName;
    private string _senderName;
    private string _message;
    private string _channelName;

    public PasswordRequestSender(string senderEmailAddress, 
      string channelCreatorName,
      string channelCreatorEmailAddress, 
      string senderName, 
      string message,
      string channelName)
    {
      _senderEmailAddress = senderEmailAddress;
      _channelCreatorName = channelCreatorName;
      _channelCreatorEmailAddress = channelCreatorEmailAddress;
      _senderName = senderName;
      _message = message;
      _channelName = channelName;
    }

    public void SendEmail()
    {
      MailMessage mailMessage = new MailMessage();
      mailMessage.From = new MailAddress(Resource.PasswordRequestEmailFrom);
      mailMessage.To.Add(_channelCreatorEmailAddress);

      mailMessage.Subject = Resource.PasswordRequestEmailSubject;
      mailMessage.Body = String.Format(Resource.PasswordRequestEmailMessage, new string[] { "\n",  _channelCreatorName, _channelName, _senderName, _senderEmailAddress, _message });

      mailMessage.IsBodyHtml = false;

      SmtpClient smtpClient = new SmtpClient(System.Configuration.ConfigurationSettings.AppSettings["SMTPserver"]);
      smtpClient.Send(mailMessage);
    }
  }
}
