using AdminLTE.Application.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Net.Web.AuthenticationAttr
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class UserAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    { 
        public UserAuthorizeAttribute()
        {
            base.AuthenticationSchemes = CookieService.AuthenticationScheme;
        }

        /// <summary>
        /// 执行验证
        /// </summary>
        /// <param name="filterContext"></param>
        public async virtual void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var authenticate = filterContext.HttpContext.AuthenticateAsync(CookieService.AuthenticationScheme);
            if (authenticate.Result == null || authenticate.Result.Succeeded || this.SkipUserAuthorize(filterContext.ActionDescriptor))
            {
                return;
            }
 
            if (filterContext.HttpContext.User.Identity.IsAuthenticated || this.SkipUserAuthorize(filterContext.ActionDescriptor))
            {
                return;
            }
            
            filterContext.Result = new RedirectResult("/Login"); ;
            return;
        }

        /// <summary>
        /// 是否跳过权限验证
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        protected virtual bool SkipUserAuthorize(ActionDescriptor actionDescriptor)
        {
            bool skipAuthorize = actionDescriptor.FilterDescriptors.Where(a => a.Filter is SkipUserAuthorizeAttribute).Any();
            if (skipAuthorize)
            {
                return true;
            }

            return false;
        }
    }
}
