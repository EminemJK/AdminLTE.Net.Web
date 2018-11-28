using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE.Domain.Service;
using AdminLTE.Models.VModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminLTE.Net.Web.Pages.Account
{
    [Authorize(AuthenticationSchemes = CookieService.AuthenticationScheme)]
    public class UserListModel : BasePageModel
    {
        public UserListModel(UserService service)
        {
            userService = service;
        }
        public void OnGet()
        {
            
        }

        public IActionResult OnGetUserPage(int pageNum = 1, int pageSize = 8,int draw = 1)
        {
            int pageCount;
            var data = userService.GetUserList(pageNum, pageSize, out pageCount);
            return new JsonResult(new VModelTableOutput<VUserListModel>(data, draw, pageCount));
        }


        protected UserService userService { get; set; }

        public List<VUserListModel> UserList { get; set; }


    }
}