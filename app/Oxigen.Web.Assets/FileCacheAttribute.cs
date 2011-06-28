using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace Oxigen.Web.Assets
{
    [AttributeUsage(AttributeTargets.Class |
                    AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class OxigenFileCacheAttribute : ActionFilterAttribute
    {
        private int _maxAge = 1800;

        public int MaxAge { get { return _maxAge; } set { _maxAge = value; } } 

        //TODO: check file path does not contain ".."
        public override void OnResultExecuting(ResultExecutingContext ctx)
        {
            HttpRequestBase request = ctx.RequestContext.HttpContext.Request;
            HttpResponseBase response = ctx.RequestContext.HttpContext.Response;
            var result = ctx.Result as FilePathResult;
            if (result == null)
            {
                base.OnResultExecuting(ctx);
                return;
            }

            if (!File.Exists(result.FileName))
            {
                response.StatusCode = 404;
                response.StatusDescription = "Not Found";
                ctx.Cancel = true;
                return;
            }

            response.AddFileDependency(result.FileName);

            //Expensive to great eTags for large files and is not really necessary since we are using LastModified Date
            //response.Cache.SetETagFromFileDependencies();

            response.Cache.SetLastModifiedFromFileDependencies();

            response.Cache.SetCacheability(HttpCacheability.Public);

            if (_maxAge > -1)
                response.Cache.SetMaxAge(new TimeSpan(0, 0, 0, _maxAge));

            response.Cache.SetSlidingExpiration(true);
            
            if ((request.Headers["If-Modified-Since"] != null) &&
                 new FileInfo(result.FileName).LastWriteTime  <= DateTime.Parse(request.Headers["If-Modified-Since"]).AddSeconds(1))
            {
                response.StatusCode = 304;
                //response.Headers.Add("Content-Encoding", "gzip");
                response.StatusDescription = "Not Modified";
                ctx.Cancel = true;
            }
            else
                base.OnResultExecuting(ctx);
        }
    }
}



