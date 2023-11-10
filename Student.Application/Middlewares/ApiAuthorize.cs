using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Student.Application.Middlewares
{
    public class ApiAuthorize : AuthorizationHandler<AuthorizeRequirement>
    {
        public ApiAuthorize()
        {

        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizeRequirement requirement)
        {
            //if (!context.User.Claims.Any())
            //{
            //    context.Fail();
            //}
            //else
            //{
            //    if (context.User.FindFirst(ClaimTypes.NameIdentifier).Value == null)
            //        context.Fail();
            //    else
            //    {
            //        var identifier = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //        //var user = await _userService.GetPersonByPhone(identifier);
            //        var staff = await _staffService.GetPersonByUserName(identifier);

            //        object userSession = null;
            //        if (/*user == null ||*/ staff == null)
            //        {
            //            int.TryParse(identifier, out int id);
            //            userSession = await _userSessionService.GetUserSessionByIdAsync(id);
            //        }

            //        if (/*user == null && */staff == null && userSession == null)
            //            context.Fail();
            //        else
            //        {
            //            //if (user != null && user.IsBlocked)
            //            //    context.Fail();
            //            //else
            //            context.Succeed(requirement);
            //        }
            context.Succeed(requirement);
            //}
        //}
        }
    }
}
