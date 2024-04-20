using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors=context.ModelState.Where(x => x.Value.Errors.Any()).ToDictionary(e => e.Value.Errors.Select(e => e.ErrorMessage)).ToArray();
                context.Result = new BadRequestObjectResult(errors);
                return;
            }
            await next();
        }
    }
}
