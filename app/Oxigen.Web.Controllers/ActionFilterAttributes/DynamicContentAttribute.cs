using System;
using System.Web;
using System.Web.Mvc;

namespace Oxigen.Web.Controllers.ActionFilterAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class DynamicContentAttribute : ActionFilterAttribute
    {
        private int _maxAgeInSeconds;
        public int MaxAgeInSeconds
        {
            get { return _maxAgeInSeconds; }
            set { _maxAgeInSeconds = value; }
        }

        public override void OnResultExecuting(ResultExecutingContext ctx)
        {
            HttpRequestBase request = ctx.RequestContext.HttpContext.Request;
            HttpResponseBase response = ctx.RequestContext.HttpContext.Response;

            if (ctx.Result == null || !(ctx.Result is FileContentResult))
            {
                base.OnResultExecuting(ctx);
                return;
            }

            var result = ctx.Result as FileContentResult;

            var eTag = Convert.ToBase64String(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(result.FileContents));

            response.ClearHeaders();
            response.Cache.SetSlidingExpiration(true);
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetETag(eTag);
            response.Cache.SetMaxAge(TimeSpan.FromSeconds(_maxAgeInSeconds));

            if (request.Headers["If-None-Match"] != null && request.Headers["If-None-Match"] == eTag)
            {
                response.StatusCode = 304;
                response.StatusDescription = "Not Modified";
                ctx.Cancel = true;
            }
            else
                base.OnResultExecuting(ctx);
        }
    }
}
