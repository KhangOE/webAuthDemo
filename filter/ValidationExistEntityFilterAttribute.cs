using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using web_authentication.Data;

namespace web_authentication.filter
{
    public class ValidationExistEntityFilterAttribute<T> : IActionFilter where T : class
    {
        private DataContext _dataContext;

        public ValidationExistEntityFilterAttribute(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (context.ActionArguments.ContainsKey("id"))
            {
                var id = context.ActionArguments["id"];
          
                var entity = _dataContext.Set<T>().Find(id) as T;
                if (entity != null)
                {
                    context.HttpContext.Items.Add("entity", entity);
                }
                else
                {
                    context.Result = new NotFoundResult();

                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
