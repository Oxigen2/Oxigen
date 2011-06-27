﻿using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Oxigen.Web.Assets
{
    [AttributeUsage(AttributeTargets.Class |
                    AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class FileCacheAttribute : ActionFilterAttribute
    {
        private int _maxAge = 1800;

        public int MaxAge { get { return _maxAge; } set { _maxAge = value; } } 

        //TODO: check file path does not contain ".."
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.RequestContext.HttpContext.Request;
            HttpResponseBase response = filterContext.RequestContext.HttpContext.Response;
            var result = filterContext.Result as FilePathResult;
            if (result == null)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            if (!File.Exists(result.FileName))
            {          
                response.Write(DateTime.Now);
                response.StatusCode = 404;
                response.StatusDescription = "Not Found";
                return;

            }
            
            if ((request.Headers["If-Modified-Since"] != null) &&  
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

            //Expensive to great eTags for large files and is not really necessary since we are using LastModified Date
            //response.Cache.SetETagFromFileDependencies();

            response.Cache.SetLastModifiedFromFileDependencies();

            response.Cache.SetCacheability(HttpCacheability.Public);
            if (_maxAge > -1)
                response.Cache.SetMaxAge(new TimeSpan(0, 0, _maxAge, 0));

            response.Cache.SetSlidingExpiration(true);
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



