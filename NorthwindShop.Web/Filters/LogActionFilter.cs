using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace NorthwindShop.Web.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public LogActionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Action {((ControllerActionDescriptor)context.ActionDescriptor).ActionName} in" +
                $" controller {((ControllerActionDescriptor)context.ActionDescriptor).ControllerName} has been started.");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Action {((ControllerActionDescriptor)context.ActionDescriptor).ActionName} in" +
                $" controller {((ControllerActionDescriptor)context.ActionDescriptor).ControllerName} has been ended.");
        }
    }
}
