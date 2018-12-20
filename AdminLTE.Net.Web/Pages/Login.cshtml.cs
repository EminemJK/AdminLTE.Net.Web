using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminLTE.Application.Service.UserSvr;
using AdminLTE.Application.Service;
using AdminLTE.Models.VModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AdminLTE.Net.Web.Pages
{
    public class LoginModel : PageModel
    {
        public LoginModel(IUserService service)
        {
            userService = service;
        }
        public void OnGet()
        { 
        }

        protected IUserService userService { get; set; }

        [BindProperty]
        public VLoginInput Login { get; set; }

        [TempData]
        public string Message { get; set; }

        [HttpPost]
        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = ModelState.Root.Errors[0].ErrorMessage;
            }
            else
            {
                var user = userService.Login(Login.UserName, Login.Password);
                if (user != null)
                {
                    VUserCookieModel model = new VUserCookieModel()
                    {
                        Id = user.Id,
                        UserName = user.Name,
                        LoginTime = DateTime.Now,
                        HeaderImg = user.HeaderImg
                    }; 
                    var identity = new ClaimsIdentity(CookieService.AuthenticationScheme); 
                    identity.AddClaim(new Claim(ClaimTypes.Sid, CookieService.GetDesEncrypt(model)));

                    await HttpContext.SignInAsync(CookieService.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties()
                    {
                        //记住我
                        IsPersistent = true, 
                        //过期时间
                        ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromMinutes(30))
                    }); 
                    return RedirectToPage("./Index"); 
                }
                Message = "登录失败，用户名密码不正确。";
            }
            return Page();
        }
    }
}