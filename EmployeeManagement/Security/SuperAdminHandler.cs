using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Security
{
    public class SuperAdminHandler :AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ManageAdminRolesAndClaimsRequirement requirement)
        {
            if (context.User.IsInRole("Super Admin"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
//public void ConfigureServices(IServiceCollection services)
//{
//    services.AddAuthorization(options =>
//    {
//        options.AddPolicy("EditRolePolicy", policy =>
//            policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));
//    });

//    // Register the first handler
//    services.AddSingleton<IAuthorizationHandler,
//        CanEditOnlyOtherAdminRolesAndClaimsHandler>();
//    // Register the second handler
//    services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();
//}