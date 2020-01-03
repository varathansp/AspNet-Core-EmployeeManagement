using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        private ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodehaddler(int statusCode)
        {
            var statusCodeResult =
               HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage =
                            "Sorry, the resource you requested could not be found";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    break;

            }

            // LogWarning() method logs the message under
            // Warning category in the log
            _logger.LogWarning($"404 error occured. Path = " +
                $"{statusCodeResult.OriginalPath} and QueryString = " +
                $"{statusCodeResult.OriginalQueryString}");

            return View("NotFound");
        }
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            // Retrieve the exception Details
            var exceptionHandlerPathFeature =
                    HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            // LogError() method logs the exception under Error category in the log
            _logger.LogError($"The path {exceptionHandlerPathFeature.Path} " +
                $"threw an exception {exceptionHandlerPathFeature.Error}");


            return View("Error");
        }
    }
    
    
}
