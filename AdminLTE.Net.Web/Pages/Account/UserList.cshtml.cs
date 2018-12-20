using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE.Application.Service;
using AdminLTE.Application.Service.UserSvr;
using AdminLTE.Application.Service.UserSvr.Dto;
using AdminLTE.Models.VModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminLTE.Net.Web.Pages.Account
{
    [Authorize(AuthenticationSchemes = CookieService.AuthenticationScheme)]
    public class UserListModel : BasePageModel
    {
        public UserListModel(IUserService service)
        {
            userService = service;
        }
        public void OnGet()
        {
            
        }

        public IActionResult OnGetUserPage(VUserListConditionInput input)
        {
            int pageCount;
            var data = userService.GetUserList(input, out pageCount);
            return new JsonResult(new VModelTableOutput<UserInfoDto>(data, input.draw, pageCount));
        }


        protected IUserService userService { get; set; }

        [HttpPost]
        public IActionResult OnPostSaveAsync(UserInfoDto inputUserInfo)
        {
            if (!ModelState.IsValid)
            {
                foreach (var c in ModelState.Root.Children)
                {
                    if (c.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    {
                        return new JsonResult(c.Errors[0].ErrorMessage);
                    }
                }
            }
            if (userService.Save(inputUserInfo) > 0)
            {
                return new JsonResult("ok");
            }
            return new JsonResult("±£¥Ê ß∞‹¿≤");
        }
    }
}