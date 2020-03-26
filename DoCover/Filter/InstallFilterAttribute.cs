using Microsoft.AspNetCore.Mvc.Filters;

namespace DoCover.Filter
{
    public class InstallFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string controller = context.RouteData.Values["Controller"].ToString();
            string action = context.RouteData.Values["Action"].ToString();
            //if (controller != "Install")
            //    context.Result = new RedirectToActionResult("Index","Install",new {});
            base.OnActionExecuting(context);
        }
    }
}
