using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE.Domain.Service;
using AdminLTE.Models.VModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace AdminLTE.Net.Web.Pages
{
    public class BasePageModel : PageModel
    {
        public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
           
            if (!CheckToken(context.HttpContext))
            {
                context.Result = RedirectToPage("/Login");
            }
           

            base.OnPageHandlerExecuted(context);
        }

        protected bool CheckToken(HttpContext context)
        {
            bool b = false;
            if (context.User != null && context.User.Claims.Count() > 0)
            {
                CurrentUser = CookieService.GetDesDecrypt<VUserModel>(context.User.Claims.FirstOrDefault().Value);
                b = true;
            }
            return b;
        }
        public VUserModel CurrentUser { get; private set; }
    }
}
