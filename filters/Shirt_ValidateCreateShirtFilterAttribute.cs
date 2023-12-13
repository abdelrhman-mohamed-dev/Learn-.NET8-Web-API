using learnApi.Models;
using learnApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace learnApi.filters
{
    public class Shirt_ValidateCreateShirtFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var shirt = context.ActionArguments["shirt"] as Shirt;

            if (shirt == null) 
            {
                context.ModelState.AddModelError("shirt", "please enter shirt information");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }else
            { 
                var existingShirt = ShirtRepository.GetShirtByProperties(shirt.Brand, shirt.Gender, shirt.Color, shirt.Size);
                if (existingShirt != null)
                {
                     context.ModelState.AddModelError("shirt", "This shirt is already existing !");
                     var problemDetails = new ValidationProblemDetails(context.ModelState)
                     {
                        Status = StatusCodes.Status400BadRequest
                     };
                     context.Result = new BadRequestObjectResult(problemDetails);
                }


            }

        }
    }
}
