using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ProxyClientBaseLib
{
/// <summary>
  /// Provides static methods to override or restore SSL certificate validation
  /// </summary>
  public static class SSLValidator
  {
    private static RemoteCertificateValidationCallback _orgCallback;

    private static bool OnValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
      return true;
    }

    /// <summary>
    /// Overrides validation of an SSL certificate
    /// </summary>
    public static void OverrideValidation()
    {
      _orgCallback = ServicePointManager.ServerCertificateValidationCallback;
      ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OnValidateCertificate);
      ServicePointManager.Expect100Continue = true;
    }

    /// <summary>
    /// Restores the validation of an SSL certificate
    /// </summary>
    public static void RestoreValidation()
    {
      ServicePointManager.ServerCertificateValidationCallback = _orgCallback;
    }
  }
}
