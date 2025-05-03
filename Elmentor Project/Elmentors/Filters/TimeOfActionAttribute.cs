using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Elmentors.Filters
{
    public class TimeOfActionAttribute : Attribute, IActionFilter
    {
        private Stopwatch? _stopwatch;
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch?.Stop();
            long ExcutionTime = _stopwatch.ElapsedMilliseconds;
            if (ExcutionTime > 3000)
            {
                ContentResult contentResult = new ContentResult();
                contentResult.Content = $"Slow Excution Time {ExcutionTime}, check connection and try again";
                contentResult.StatusCode = 200;

                // if you didnt' type this line, user will not see ane reply to what happen(normal excution)
                context.Result = contentResult;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
        }
    }
}
