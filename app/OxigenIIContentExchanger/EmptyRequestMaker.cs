using System.Net;

namespace OxigenIIAdvertising.ContentExchanger
{
    public class EmptyRequestMaker
    {
        private const int REQUEST_TIMEOUT_MILLISECONDS = 3000;

        public void MakeRequest(string url, HttpMethod httpMethod)
        {
            WebRequest request = WebRequest.Create(url);
            
            request.Timeout = REQUEST_TIMEOUT_MILLISECONDS;
            request.Method = httpMethod.ToString();
            request.ContentLength = 0;

            HttpWebResponse response = null;

            try
            {
                request.Proxy = WebProxy.GetDefaultProxy();
                if (request.Proxy != null)
                    request.Proxy.Credentials = CredentialCache.DefaultCredentials;

                response = (HttpWebResponse) request.GetResponse();
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
    }

    public enum HttpMethod
    {
        POST,
        GET
    }
}