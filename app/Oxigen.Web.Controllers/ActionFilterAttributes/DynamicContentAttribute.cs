using System;
using System.Web;
using System.Web.Mvc;

namespace Oxigen.Web.Controllers.ActionFilterAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class DynamicContentAttribute : ActionFilterAttribute
    {
        private int _maxAgeInSeconds;
        private string _eTag;
        public int MaxAgeInSeconds
        {
            get { return _maxAgeInSeconds; }
            set { _maxAgeInSeconds = value; }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.RequestContext.HttpContext.Request;
            HttpResponseBase response = filterContext.RequestContext.HttpContext.Response;

            if (filterContext.Result == null || !(filterContext.Result is FileContentResult))
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            var result = filterContext.Result as FileContentResult;

            _eTag = Convert.ToBase64String(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(result.FileContents));

            if (request.Headers["If-None-Match"] != null && request.Headers["If-None-Match"] == _eTag)
            {
                response.Write(DateTime.Now);
                response.StatusCode = 304;
                response.StatusDescription = "Not Modified";
            }
            else
                base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result != null && filterContext.Result is FileContentResult)
                SetCaching(filterContext.HttpContext.Response);

            base.OnActionExecuted(filterContext);
        }

        private void SetCaching(HttpResponseBase response)
        {
            response.ClearHeaders();
            response.Cache.SetSlidingExpiration(true);
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetMaxAge(TimeSpan.FromSeconds(_maxAgeInSeconds));
        }
    }
}
