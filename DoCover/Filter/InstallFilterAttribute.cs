using DoCover.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace DoCover.Filter
{
    public class InstallFilterAttribute: ActionFilterAttribute
    {
        private readonly DoOptions _options;

        public InstallFilterAttribute(IOptionsSnapshot<DoOptions> options)
        {
            _options = options.Value;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string controller = context.RouteData.Values["Controller"].ToString();
            string action = context.RouteData.Values["Action"].ToString();

            if (!_options.Installed && controller != "Install")
            {
                context.Result = new RedirectToActionResult("Index", "Install", new { });
            }
            else if (_options.Installed && controller == "Install")
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { });
            }


            base.OnActionExecuting(context);
        }
    }
}
