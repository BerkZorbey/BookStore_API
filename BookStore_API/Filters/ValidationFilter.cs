using BookStore_API.Models.DTOs;
using BookStore_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore_API.Filters
{

    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _validationService = context.HttpContext.RequestServices.GetService<ValidationService>();

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestResult();
            }
            else
            {
                var user = context.ActionArguments["user"];
                var checkUserName = _validationService.IsUserNameValid((UserRegisterDTO)user);
                var checkEmail = _validationService.IsEmailValid((UserRegisterDTO)user);
                var checkPassword = _validationService.IsPasswordValid((UserRegisterDTO)user);
                var checkConditions = _validationService.IsConditionsValid(checkEmail, checkUserName, checkPassword);
                if (checkConditions != true)
                {
                    context.Result = new BadRequestResult();
                }

            }

        }

    }
}
