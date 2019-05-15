using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMDeveloper.Config
{
    public class ActivityAuth : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _activity;

        public ActivityAuth(string activity)
        {
            _activity = activity;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claim = context.HttpContext.User.Claims.LastOrDefault(x => x.Type == "Permissions");
            if (claim == null || !claim.Value.Split(',').Contains(_activity))
            {
                context.Result = new StatusCodeResult(401);
            }
        }
    }
}
