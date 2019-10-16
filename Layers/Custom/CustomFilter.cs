using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
//using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Layers.Custom
{
    public class CustomFilter : FilterAttribute, IActionFilter
    {
        Stopwatch watch;
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            watch.Stop();
            filterContext.HttpContext.Response.Write("Time " + watch.ElapsedMilliseconds.ToString());
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            watch = Stopwatch.StartNew();
        }
    }
}