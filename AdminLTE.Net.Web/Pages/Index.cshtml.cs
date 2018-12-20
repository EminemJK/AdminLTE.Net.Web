using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE.Application.Service;
using AdminLTE.Application.Service.UserSvr;
using AdminLTE.Application.Service.UserSvr.Dto;
using AdminLTE.Models.VModel;
using AdminLTE.Net.Web.AuthenticationAttr;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminLTE.Net.Web.Pages
{
    [UserAuthorize]
    public class IndexModel : BasePageModel
    {
        public IndexModel(IUserService service)
        {
            userService = service;
        }

        protected IUserService userService { get; set; }

        public List<UserInfoDto> UserList { get; set; }

        public void OnGet()
        {
            UserList = userService.GetIndexUsers();  
        }
    }
}
