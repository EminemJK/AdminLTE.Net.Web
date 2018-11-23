using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE.Domain.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminLTE.Net.Web.Pages
{
    [Authorize(AuthenticationSchemes = CookieService.AuthenticationScheme)]
    public class IndexModel : BasePageModel
    {
         
        public void OnGet()
        {
             
        }
    }
}
