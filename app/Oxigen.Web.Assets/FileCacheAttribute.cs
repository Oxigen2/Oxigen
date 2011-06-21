using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Oxigen.Web.Assets
{
    [AttributeUsage(AttributeTargets.Class |
                    AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class FileCacheAttribute : ActionFilterAttribute

    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)

        {
            HttpRequestBase request = filterContext.RequestContext.HttpContext.Request;

            HttpResponseBase response = filterContext.RequestContext.HttpContext.Response;
            var result = filterContext.Result as FilePathResult;

            
            if ((request.Headers["If-Modified-Since"] != null) && (result != null) && 
                 new FileInfo(result.FileName).LastWriteTimeUtc >  DateTime.Parse(request.Headers["If-Modified-Since"]))

            {
                response.Write(DateTime.Now);

                response.StatusCode = 304;

                //response.Headers.Add("Content-Encoding", "gzip");

                response.StatusDescription = "Not Modified";
            }

            else

            {
                base.OnActionExecuting(filterContext);
            }
        }

        private void SetFileCaching(HttpResponseBase response, string fileName)

        {
            response.AddFileDependency(fileName);

            //response.Cache.SetETagFromFileDependencies();

            response.Cache.SetLastModifiedFromFileDependencies();

            response.Cache.SetCacheability(HttpCacheability.Public);

            //response.Cache.SetMaxAge(new TimeSpan(7, 0, 0, 0));

            //response.Cache.SetSlidingExpiration(true);
        }


        public override void OnActionExecuted(ActionExecutedContext filterContext)

        {
            var result = filterContext.Result as FilePathResult;

            if (result != null)

            {
                if (!string.IsNullOrEmpty(result.FileName) && (File.Exists(result.FileName)))

                    SetFileCaching(filterContext.HttpContext.Response, result.FileName);
            }

            base.OnActionExecuted(filterContext);
        }
    }
}



