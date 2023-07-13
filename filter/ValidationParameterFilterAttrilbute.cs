using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace web_authentication.filter
{
    public class ValidationParameterFilterAttrilbute : IActionFilter
    {
       

        public void OnActionExecuting(ActionExecutingContext context)
        {
         
            var checkParam = context.ActionArguments.Values.Any( x => x is null );

            if (checkParam)
            {
                context.Result = new BadRequestObjectResult("having param null");
                return;
            }

            foreach (var param in context.ActionArguments.Values)
            {

                if (param is ICollection<int>)
                {
                    var p = param as ICollection<int>;
                    if (p.Count() == 0)
                    {
                        context.Result = new BadRequestObjectResult("having emty list parameter");
                        return;
                    }
                }
               
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
